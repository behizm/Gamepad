using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class UserReviewLike : BaseEntity
    {
        public bool Like { get; set; }

        [ForeignKey("UserReview")]
        public Guid UserReviewId { get; set; }
        public virtual UserReview UserReview { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
