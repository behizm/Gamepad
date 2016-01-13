using System;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;

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
            var title = item.Title.Trim();
            while (title.Contains("  "))
            {
                title = title.Replace("  ", " ");
            }
            var name = title.ToLower().Replace(" ", "_");

            if (FindByName(name) != null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }

            item.Title = title;
            item.Name = name;
            item.EditDate = null;
            item.UserScoresAverage = null;
            return base.Insert(item);
        }

        public override OperationResult Update(Article item)
        {
            var title = item.Title.Trim();
            while (title.Contains("  "))
            {
                title = title.Replace("  ", " ");
            }
            var name = title.ToLower().Replace(" ", "_");
            var article = FindByName(name);
            if (article.Id != item.Id)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }

            item.Title = title;
            item.Name = name;
            item.EditDate = DateTime.Now;
            return base.Update(item);
        }

        public string ChangePoster(Guid articleId, Guid posterId)
        {
            var article = FindById(articleId);
            if (article == null)
            {
                return null;
            }

            var file = GpServices.File.FindById(posterId);
            if (file == null)
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
    }
}
