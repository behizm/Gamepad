using System;
using System.Collections.Generic;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IPostService : IBaseService<IPostService, Post>
    {
        Post FindByTitle(string title);
        Post FindByName(string name);
        OperationResult Store(Post item);
        OperationResult Store(PostUpdateModel model);
        OperationResult Publish(Guid postId, DateTime? publishDate = null);
        OperationResult ChangeHide(Guid postId, bool isHidden);
        OperationResult SyncPostReviews(Guid postId, ICollection<PostReview> reviews);
        OperationResult Viewed(Guid postId);
    }
}