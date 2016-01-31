using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Services
{
    internal class PostService : BaseService<Post>, IPostService
    {
        public PostService(GamepadContext context) : base(context)
        {
        }

        public IPostService Clone()
        {
            return new PostService(new GamepadContext());
        }

        public Post FindByTitle(string title)
        {
            return Get(x => x.Title == title);
        }

        public Post FindByName(string name)
        {
            name = name.ToLower();
            return Get(x => x.Name == name);
        }

        public override OperationResult Insert(Post item)
        {
            var post = FindByName(item.Name);
            if (post != null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }
            item.EditDate = null;
            item.View = 0;
            item.IsHidden = false;
            return base.Insert(item);
        }

        public override OperationResult Update(Post item)
        {
            var post = FindByName(item.Name);
            if (post == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (post.Id != item.Id)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }

            item.EditDate = DateTime.Now;
            return base.Update(item);
        }

        public OperationResult Store(Post item)
        {
            var post = FindById(item.Id);
            return post == null ? Insert(item) : Update(item);
        }

        public OperationResult Store(PostUpdateModel model)
        {
            if (!ValidateModel(model))
            {
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);
            }
            var post = FindByName(model.Name);
            if (post != null && post.Id != model.Id)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }

            if (!model.Id.HasValue)
            {
                post = new Post
                {
                    Title = model.Title,
                    AuthorId = model.AuthorId.Value,
                    PostType = model.PostType.Value,
                    Description = model.Description,
                    PostContent = new PostContent { Content = model.Content },
                    PublishDate = model.PublishDate,
                    Articles = new List<Article>(),
                    Attachments = new List<File>(),
                    ImageGallery = new List<File>(),
                    Tags = new List<Tag>(),
                };
            }
            else
            {
                if (post == null)
                {
                    post = FindById(model.Id.Value);
                    if (post == null)
                    {
                        return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
                    }
                }
                post.Title = model.Title;
                post.AuthorId = model.AuthorId.Value;
                post.PostType = model.PostType.Value;
                post.Description = model.Description;
                post.EditDate = DateTime.Now;
                post.PostContent.Content = model.Content;
                post.PublishDate = model.PublishDate;
            }

            if (model.MainImageId != post.MainImageId)
            {
                if (model.MainImageId.HasValue)
                {
                    var file = GpServices.File.FindById(model.MainImageId.Value);
                    if (file == null || file.FileType != FileType.Image)
                    {
                        model.MainImageId = null;
                    }
                }
                post.MainImageId = model.MainImageId;
            }

            if (model.VideoId != post.VideoId)
            {
                if (model.VideoId.HasValue)
                {
                    var file = GpServices.File.FindById(model.VideoId.Value);
                    if (file == null || file.FileType != FileType.Video)
                    {
                        model.VideoId = null;
                    }
                }
                post.VideoId = model.VideoId;
            }

            //sync post tags
            if (model.PostTags == null)
            {
                model.PostTags = new List<string>();
            }
            var newTags =
                model.PostTags.Where(x => post.Tags.All(p => p.Name.ToLower() != x.ToLower())).ToList();
            foreach (var n in newTags)
            {
                var tag = Context.Tags.FirstOrDefault(x => x.Name.ToLower() == n.ToLower());
                post.Tags.Add(tag ?? new Tag { Name = n });
            }
            var deletedTags =
                post.Tags.Where(x => model.PostTags.All(p => p.ToLower() != x.Name.ToLower())).ToList();
            foreach (var d in deletedTags)
            {
                post.Tags.Remove(d);
            }

            // sync articles
            if (model.Articles == null)
            {
                model.Articles = new List<Guid>();
            }
            var newArticles =
                model.Articles.Where(x => post.Articles.All(p => p.Id != x)).ToList();
            foreach (var n in newArticles)
            {
                var article = GpServices.Article.FindById(n);
                if (article != null)
                {
                    post.Articles.Add(article);
                }
            }
            var deletedArticles =
                post.Articles.Where(x => model.Articles.All(p => p != x.Id)).ToList();
            foreach (var d in deletedArticles)
            {
                post.Articles.Remove(d);
            }

            // sync attachments
            if (model.Attachments == null)
            {
                model.Attachments = new List<Guid>();
            }
            var newAttachments =
                model.Attachments.Where(x => post.Attachments.All(p => p.Id != x)).ToList();
            foreach (var n in newAttachments)
            {
                var file = GpServices.File.FindById(n);
                if (file != null)
                {
                    post.Attachments.Add(file);
                }
            }
            var deletedAttachments =
                post.Attachments.Where(x => model.Attachments.All(p => p != x.Id)).ToList();
            foreach (var d in deletedAttachments)
            {
                post.Attachments.Remove(d);
            }

            // sync images
            if (model.Images == null)
            {
                model.Images = new List<Guid>();
            }
            var newImages =
                model.Images.Where(x => post.ImageGallery.All(p => p.Id != x)).ToList();
            foreach (var n in newImages)
            {
                var file = GpServices.File.FindById(n);
                if (file != null && file.FileType == FileType.Image)
                {
                    post.ImageGallery.Add(file);
                }
            }
            var deletedImages =
                post.ImageGallery.Where(x => model.Images.All(p => p != x.Id)).ToList();
            foreach (var d in deletedImages)
            {
                post.ImageGallery.Remove(d);
            }

            return model.Id.HasValue ? base.Update(post) : base.Insert(post);
        }

        public OperationResult Publish(Guid postId, DateTime? publishDate = null)
        {
            var post = FindById(postId);
            if (post == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            post.PublishDate = publishDate ?? DateTime.Now;
            post.EditDate = DateTime.Now;
            return base.Update(post); ;
        }

        public OperationResult ChangeHide(Guid postId, bool isHidden)
        {
            var post = FindById(postId);
            if (post == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            post.IsHidden = isHidden;
            post.EditDate = DateTime.Now;
            return base.Update(post);
        }

        public OperationResult SyncPostReviews(Guid postId, ICollection<PostReview> reviews)
        {
            var post = FindById(postId);
            if (post == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }

            var newReviews =
                reviews.Where(x => post.ReviewScores.All(b => x.Title.ToLower() != b.Title.ToLower())).ToList();
            foreach (var n in newReviews)
            {
                post.ReviewScores.Add(n);
            }
            var deletedReviews =
                post.ReviewScores.Where(x => reviews.All(b => x.Title.ToLower() != b.Title.ToLower())).ToList();
            foreach (var d in deletedReviews)
            {
                Context.Entry(d).State = EntityState.Deleted;
            }
            var updatedReviews =
                reviews.Where(x => post.ReviewScores.Any(b => x.Title.ToLower() == b.Title.ToLower())).ToList();
            foreach (var u in updatedReviews)
            {
                var buffer = post.ReviewScores.FirstOrDefault(x => x.Title.ToLower() == u.Title.ToLower());
                if (buffer == null) continue;
                buffer.Max = u.Max;
                buffer.Score = u.Score;
                buffer.Title = u.Title;
            }

            post.EditDate = DateTime.Now;
            return base.Update(post);
        }

        public OperationResult Viewed(Guid postId)
        {
            var post = FindById(postId);
            if (post == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            post.View = post.View + 1;
            return base.Update(post);
        }
    }
}