using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IUserReviewService : IBaseService<IUserReviewService, UserReview>
    {
        OperationResult Like(Guid userReviewId, Guid userId);
        OperationResult Dislike(Guid userReviewId, Guid userId);
        OperationResult CancelLike(Guid userReviewId, Guid userId);

        event EventHandler<UserReviewEventArgs> UserReviewAdded;
    }
}