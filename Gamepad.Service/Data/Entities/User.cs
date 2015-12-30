using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class User : BaseEntity
    {
        [Required, StringLength(25, MinimumLength = 5)]
        [RegularExpression("^[a-z0-9._-]{5,25}$")]
        [Index("IX_Username", IsUnique = true)]
        public string Username { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$")]
        [Index("IX_Email", IsUnique = true)]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public short AccessFailed { get; set; }

        public bool IsLock { get; set; }

        public DateTime? LockedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }



        public virtual UserAvatar Avatar { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<TrackingArticle> TrackingArticles { get; set; }
    }
}
