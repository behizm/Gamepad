using System;
using System.Collections.Generic;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IArticleService : IBaseService<IArticleService, Article>
    {
        Article FindByTitle(string title);
        Article FindByName(string name);
        Cluster<Article> Search<TOrderingKey>(ArticleSearchModel model, Ordering<Article, TOrderingKey> ordering);
        OperationResult AddPlatform(Guid articleId, ICollection<GamePlatform> platforms);
        OperationResult RemovePlatform(Guid articleId, ICollection<GamePlatform> platforms);
        string ChangePoster(Guid articleId, Guid? posterId);
        OperationResult AddToGenre(Guid articleId, Guid genreId);
        OperationResult RemoveFromGenre(Guid articleId, Guid genreId);
        OperationResult AddToCast(Guid articleId, Guid castId);
        OperationResult RemoveFromCast(Guid articleId, Guid castId);
        OperationResult AddToImageGallery(Guid articleId, ICollection<Guid> imageIds);
        OperationResult RemoveFromImageGallery(Guid articleId, ICollection<Guid> imageIds);
        OperationResult UpdateUserScoresAverage(Guid articleId);

        void OnUserReviewAdded(object sender, UserReviewEventArgs userReview);
    }
}
