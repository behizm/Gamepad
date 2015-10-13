using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    internal class User : BaseEntity
    {
        [Required, StringLength(25, MinimumLength = 5)]
        [RegularExpression("^[a-z0-9._-]{5,25}$")]
        public string Username { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$")]
        public string Email { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^\d{12}$")]
        public string Mobile { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public short AccessFailed { get; set; }

        public bool IsLock { get; set; }

        public DateTime? LockedDate { get; set; }



        [ForeignKey("Avatar")]
        public Guid? AvatarId { get; set; }
        public virtual UserAvatar Avatar { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<TrackingArticle> TrackingArticles { get; set; }
    }
}
