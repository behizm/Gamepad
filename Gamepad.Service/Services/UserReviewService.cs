using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var article = GpServices.Article.FindById(item.ArticleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (article.UserReviews != null && article.UserReviews.Any(x => x.UserId == item.UserId))
            {
                return OperationResult.Failed("شما قبلا نظر داده اید.");
            }
            item.DislikeCount = 0;
            item.LikeCount = 0;
            item.Article = null;
            if (article.UserReviews == null)
            {
                article.UserReviews = new List<UserReview>();
            }
            article.UserReviews.Add(item);
            article.UserScoresAverage = (short)(article.UserReviews.Sum(x => x.Score) / article.UserReviews.Count());
            return OperationResult.Success;
        }

        public override OperationResult Update(UserReview item)
        {
            item.Article.UserScoresAverage = (short)(item.Article.UserReviews.Sum(x => x.Score) / item.Article.UserReviews.Count());
            return base.Update(item);
        }

        public override OperationResult Delete(UserReview item)
        {
            var article = GpServices.Article.FindById(item.ArticleId);
            if (article == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var userReview = article.UserReviews.FirstOrDefault(x => x.Id == item.Id);
            if (userReview == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            base.Delete(userReview);
            if (!article.UserReviews.Any())
            {
                article.UserScoresAverage = null;
            }
            else
            {
                article.UserScoresAverage = (short)(article.UserReviews.Sum(x => x.Score) / article.UserReviews.Count());
            }
            return OperationResult.Success;
        }

        public override OperationResult Delete(Guid userReviewId)
        {
            var userReview = FindById(userReviewId);
            if (userReview == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            return Delete(userReview);
        }

        public OperationResult Like(Guid userReviewId, Guid userId)
        {
            var review = FindById(userReviewId);
            if (review == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var user = GpServices.User.FindById(userId);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var userReviewLike = review.Likes.FirstOrDefault(x => x.UserId == userId);
            if (userReviewLike != null && userReviewLike.Like)
            {
                return OperationResult.Success;
            }
            if (userReviewLike != null && !userReviewLike.Like)
            {
                userReviewLike.Like = true;
                review.LikeCount = review.Likes.Count(x => x.Like);
                review.DislikeCount = review.Likes.Count(x => !x.Like);
                return OperationResult.Success;
            }
            if (review.Likes == null)
            {
                review.Likes = new List<UserReviewLike>();
            }
            review.Likes.Add(new UserReviewLike
            {
                UserId = userId,
                Like = true,
            });
            review.LikeCount = review.Likes.Count(x => x.Like);
            review.DislikeCount = review.Likes.Count(x => !x.Like);
            return OperationResult.Success;
        }

        public OperationResult Dislike(Guid userReviewId, Guid userId)
        {
            var review = FindById(userReviewId);
            if (review == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var user = GpServices.User.FindById(userId);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var userReviewLike = review.Likes.FirstOrDefault(x => x.UserId == userId);
            if (userReviewLike != null && !userReviewLike.Like)
            {
                return OperationResult.Success;
            }
            if (userReviewLike != null && userReviewLike.Like)
            {
                userReviewLike.Like = false;
                review.LikeCount = review.Likes.Count(x => x.Like);
                review.DislikeCount = review.Likes.Count(x => !x.Like);
                return OperationResult.Success;
            }
            if (review.Likes == null)
            {
                review.Likes = new List<UserReviewLike>();
            }
            review.Likes.Add(new UserReviewLike
            {
                UserId = userId,
                Like = false,
            });
            review.LikeCount = review.Likes.Count(x => x.Like);
            review.DislikeCount = review.Likes.Count(x => !x.Like);
            return OperationResult.Success;
        }

        public OperationResult CancelLike(Guid userReviewId, Guid userId)
        {
            var review = FindById(userReviewId);
            if (review == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var like = review.Likes.FirstOrDefault(x => x.UserId == userId);
            if (like == null)
            {
                return OperationResult.Success;
            }
            Context.Entry(like).State = EntityState.Deleted;
            review.LikeCount = review.Likes.Count(x => x.Like);
            review.DislikeCount = review.Likes.Count(x => !x.Like);
            return OperationResult.Success;
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