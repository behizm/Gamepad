using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.EventArgs;

namespace Gamepad.Service.Interfaces
{
    public interface IUserReviewService : IBaseService<IUserReviewService, UserReview>
    {
        event EventHandler<UserReviewEventArgs> UserReviewAdded;
    }
}