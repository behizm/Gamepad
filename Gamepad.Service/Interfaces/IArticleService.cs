using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IArticleService : IBaseService<IArticleService, Article>
    {
        Article FindByTitle(string title);
        Article FindByName(string name);
        string ChangePoster(Guid articleId, Guid posterId);
        Cluster<Article> Search<TOrderingKey>(ArticleSearchModel model, Ordering<Article, TOrderingKey> ordering);
        OperationResult AddToGenre(Guid articleId, Guid genreId);
        OperationResult RemoveFromGenre(Guid articleId, Guid genreId);
        OperationResult AddCast(Guid articleId, Guid castId);
        OperationResult RemoveCast(Guid articleId, Guid castId);
    }
}
