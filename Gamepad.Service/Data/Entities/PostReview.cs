using System;
using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    public class PostReview : BaseEntity
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        public short Max { get; set; }

        public short Score { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
