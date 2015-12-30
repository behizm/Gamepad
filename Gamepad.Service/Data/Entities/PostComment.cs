using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class PostComment : BaseEntity
    {
        [Required, StringLength(300)]
        public string Content { get; set; }

        public bool IsForbiddenComment { get; set; }


        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("Post")]
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }


        [ForeignKey("Parent")]
        public Guid? ParentId { get; set; }
        public virtual PostComment Parent { get; set; }
    }
}
