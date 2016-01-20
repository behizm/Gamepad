using System;
using System.Linq;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Services
{
    internal class UserReviewService : BaseService<UserReview>, IUserReviewService
    {
        public UserReviewService(GamepadContext context) : base(context)
        {
        }

        public IUserReviewService Clone()
        {
            return new UserReviewService(new GamepadContext());
        }


        public override OperationResult Insert(UserReview item)
        {
            item.DislikeCount = 0;
            item.LikeCount = 0;
            var result = base.Insert(item);
            if (result.Succeeded)
            {
                OnUserReviewAdded(new UserReviewEventArgs { ArticleId = item.Id });
            }
            return result;
        }


        #region Events

        public event EventHandler<UserReviewEventArgs> UserReviewAdded;
        protected virtual void OnUserReviewAdded(UserReviewEventArgs e)
        {
            var handler = UserReviewAdded;
            handler?.Invoke(this, e);
        }

        #endregion
    }
}