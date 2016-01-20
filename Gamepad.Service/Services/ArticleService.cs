using System;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Services
{
    internal class ArticleService : BaseService<Article>, IArticleService
    {
        public ArticleService(GamepadContext context) : base(context)
        {
        }

        public IArticleService Clone()
        {
            return new ArticleService(new GamepadContext());
        }


        public Article FindByTitle(string title)
        {
            return Get(x => x.Title == title);
        }

        public Article FindByName(string name)
        {
            name = name.ToLower();
            return Get(x => x.Name == name);
        }

        public override OperationResult Insert(Article item)
        {
            //item = _normalizeArticleName(item);
            if (FindByName(item.Name) != null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }
            item.EditDate = null;
            item.UserScoresAverage = null;
            return base.Insert(item);
        }

        public override OperationResult Update(Article item)
        {
            var article = FindByName(item.Name);
            if (article.Id != item.Id)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }

            item.EditDate = DateTime.Now;
            return base.Update(item);
        }

        public Cluster<Article> Search<TOrderingKey>(ArticleSearchModel model, Ordering<Article, TOrderingKey> ordering)
        {
            Expression<Func<Article, bool>> expression;
            if (model == null)
            {
                expression = u => true;
            }
            else
            {
                expression = u =>
                    (string.IsNullOrEmpty(model.Name) || u.Name.ToLower().Contains(model.Name.ToLower())) &&
                    (string.IsNullOrEmpty(model.Title) || u.Title.ToLower().Contains(model.Title.ToLower())) &&
                    (!model.Type.HasValue || u.Type == model.Type) &&
                    (!model.Platform.HasValue || u.Platforms.Any(x => x.GamePlatform == model.Platform)) &&
                    (!model.ReleaseDateFrom.HasValue || u.ReleaseDate >= model.ReleaseDateFrom) &&
                    (!model.ReleaseDateTo.HasValue || u.ReleaseDate <= model.ReleaseDateTo) &&
                    (!model.SiteScoreFrom.HasValue || u.SiteScore >= model.SiteScoreFrom) &&
                    (!model.SiteScoreTo.HasValue || u.SiteScore <= model.SiteScoreTo) &&
                    (!model.UserScoresAverageFrom.HasValue || u.UserScoresAverage >= model.UserScoresAverageFrom) &&
                    (!model.UserScoresAverageTo.HasValue || u.UserScoresAverage <= model.UserScoresAverageTo) &&
                    (!model.GenreId.HasValue || u.Genres.Any(x => x.Id == model.GenreId)) &&
                    (!model.CrewId.HasValue || u.Crews.Any(x => x.Id == model.CrewId));
            }
            return Search(expression, ordering);
        }

        // Platform ...

        public OperationResult AddPlatform(Guid articleId, ICollection<GamePlatform> platforms)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.Platforms == null)
            {
                article.Platforms = new List<ArticlePlatform>();
            }
            var newPlatforms = platforms.Where(x => article.Platforms.All(p => p.GamePlatform != x));
            foreach (var gamePlatform in newPlatforms)
            {
                article.Platforms.Add(new ArticlePlatform(gamePlatform));
            }
            article.EditDate = DateTime.Now;
            return OperationResult.Success;
        }

        public OperationResult RemovePlatform(Guid articleId, ICollection<GamePlatform> platforms)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.Platforms == null)
            {
                return OperationResult.Success;
            }
            foreach (var gamePlatform in platforms)
            {
                var platform = article.Platforms.FirstOrDefault(x => x.GamePlatform == gamePlatform);
                if (platform != null)
                {
                    Context.Entry(platform).State = EntityState.Deleted;
                }
            }
            article.EditDate = DateTime.Now;
            return OperationResult.Success;
        }
        
        // Poster ...

        public string ChangePoster(Guid articleId, Guid? posterId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return null;
            }

            if (!posterId.HasValue)
            {
                article.PosterId = null;
                article.EditDate = DateTime.Now;
                return base.Update(article).Succeeded ? string.Empty : null;
            }

            var file = GpServices.File.FindById(posterId.Value);
            if (file == null || file.FileType != FileType.Image || file.Category != FileCategory.ArticlePoster)
            {
                return null;
            }

            if (article.PosterId == file.Id)
            {
                return file.Address;
            }
            article.PosterId = file.Id;
            article.EditDate = DateTime.Now;
            return base.Update(article).Succeeded ? file.Address : null;
        }

        // Genre ...

        public OperationResult AddToGenre(Guid articleId, Guid genreId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var genre = GpServices.Genre.FindById(genreId);
            if (genre == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.Genres == null)
            {
                article.Genres = new List<Genre> { genre };
            }
            else if (article.Genres.Any(x => x.Id == genre.Id))
            {
                return OperationResult.Success;
            }
            else
            {
                article.Genres.Add(genre);
            }
            return OperationResult.Success;
        }

        public OperationResult RemoveFromGenre(Guid articleId, Guid genreId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var genre = GpServices.Genre.FindById(genreId);
            if (genre == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.Genres == null || article.Genres.All(x => x.Id != genre.Id))
            {
                return OperationResult.Success;
            }
            article.Genres.Remove(genre);
            return OperationResult.Success;
        }

        // Cast ...

        public OperationResult AddToCast(Guid articleId, Guid castId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var cast = GpServices.Cast.FindById(castId);
            if (cast == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.Crews == null)
            {
                article.Crews = new List<Cast> { cast };
            }
            else if (article.Crews.Any(x => x.Id == cast.Id))
            {
                return OperationResult.Success;
            }
            else
            {
                article.Crews.Add(cast);
            }
            return OperationResult.Success;
        }

        public OperationResult RemoveFromCast(Guid articleId, Guid castId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var cast = GpServices.Cast.FindById(castId);
            if (cast == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.Crews == null || article.Crews.All(x => x.Id != cast.Id))
            {
                return OperationResult.Success;
            }
            article.Crews.Remove(cast);
            return OperationResult.Success;
        }

        // Image Gallery ...

        public OperationResult AddToImageGallery(Guid articleId, ICollection<FileBaseInfoModel> images)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.ImageGallery == null)
            {
                article.ImageGallery = new List<File>();
            }
            foreach (var image in images)
            {
                image.Address = image.Address.Trim().ToLower();
                image.Filename = image.Filename.Trim();
                if (string.IsNullOrEmpty(image.Address) || string.IsNullOrEmpty(image.Filename))
                {
                    return OperationResult.Failed(ErrorMessages.Services_General_InputData);
                }
                article.ImageGallery.Add(new File
                {
                    Title = article.Title.Replace(" ", "") + "Image",
                    Address = image.Address,
                    Category = FileCategory.ArticleImage,
                    FileType = FileType.Image,
                    Filename = image.Filename,
                    IsPublic = false,
                    Size = image.Size
                });
            }
            return OperationResult.Success;
        }

        public OperationResult RemoveFromImageGallery(Guid articleId, ICollection<Guid> imageIds)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            foreach (var imageId in imageIds)
            {
                var file = article.ImageGallery.FirstOrDefault(x => x.Id == imageId);
                if (file != null)
                {
                    Context.Entry(file).State = EntityState.Deleted;
                }
            }
            return OperationResult.Success;
        }

        // User Review

        public OperationResult UpdateUserScoresAverage(Guid articleId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }

            if (article.UserReviews == null || !article.UserReviews.Any())
            {
                article.UserScoresAverage = null;
                return Update(article);
            }

            article.UserScoresAverage = (short)(article.UserReviews.Sum(x => x.Score) / article.UserReviews.Count());
            return Update(article);
        }


        #region Events Listeners

        public void OnUserReviewAdded(object sender, UserReviewEventArgs userReview)
        {
            //throw new Exception("sdsd");
            UpdateUserScoresAverage(userReview.ArticleId);
        }

        #endregion
    }
}
