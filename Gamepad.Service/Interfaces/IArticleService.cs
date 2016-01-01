using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Services;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IArticleService : IBaseService<IArticleService, Article>
    {
        Article FindByTitle(string title);
        Article FindByName(string name);
        Cluster<Article> Search<TOrderingKey>(ArticleSearchModel model, Ordering<Article, TOrderingKey> ordering);
        OperationResult ChangePoster(Guid articleId, Guid fileId);
        OperationResult ChangeMoreInfo(ArticleInfo info);
        OperationResult AddToGenre(Guid articleId, Guid genreId);
        OperationResult RemoveFromGenre(Guid articleId, Guid genreId);
        OperationResult AddCast(Guid articleId, Guid castId);
        OperationResult RemoveCast(Guid articleId, Guid castId);
    }
}
