using System;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gamepad.Service.Models.CrossModels;
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
            title = title.ToLower();
            return Get(x => x.Title == title);
        }

        public Article FindByName(string name)
        {
            name = name.ToLower();
            return Get(x => x.Name == name);
        }

        public override OperationResult Insert(Article item)
        {
            item = _normalizeArticleName(item);
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
            item = _normalizeArticleName(item);
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
                    (!model.Platform.HasValue || u.Platform == model.Platform) &&
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

        public string ChangePoster(Guid articleId, Guid posterId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return null;
            }

            var file = GpServices.File.FindById(posterId);
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
            return Update(article);
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
            return Update(article);
        }

        public OperationResult AddCast(Guid articleId, Guid castId)
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
            return Update(article);
        }

        public OperationResult RemoveCast(Guid articleId, Guid castId)
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
            return Update(article);
        }


        #region Private Methods

        private readonly Func<Article, Article> _normalizeArticleName = item =>
        {
            var title = item.Title.Trim();
            while (title.Contains("  "))
            {
                title = title.Replace("  ", " ");
            }
            var name = title.ToLower().Replace(" ", "_");
            item.Title = title;
            item.Name = name;
            return item;
        };

        #endregion

    }
}
