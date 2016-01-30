using System;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
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
            if (post != null && post.Id != item.Id)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }
            item.EditDate = DateTime.Now;
            return base.Update(item);
        }

        public OperationResult Save(Post item)
        {
            var post = FindById(item.Id);
            return post == null ? Insert(item) : Update(item);
        }

        public OperationResult Publish(Guid postId, DateTime? publishDate = null)
        {
            var post = FindById(postId);
            if (post == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            post.PublishDate = publishDate ?? DateTime.Now;
            return Update(post);
        }

        public OperationResult ChangeHide(Guid postId, bool isHidden)
        {
            var post = FindById(postId);
            if (post == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            post.IsHidden = isHidden;
            return Update(post);
        }
    }
}