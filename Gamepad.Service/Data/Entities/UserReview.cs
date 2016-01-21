using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class UserReview : BaseEntity
    {
        [StringLength(500, MinimumLength = 20)]
        public string Description { get; set; }

        [Index("IX_Score")]
        public short Score { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public virtual ICollection<UserReviewLike> Likes { get; set; }

    }
}
