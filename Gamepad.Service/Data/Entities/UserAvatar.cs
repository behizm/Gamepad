using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class UserAvatar
    {
        [Key, ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("File")]
        public Guid FileId { get; set; }
        public virtual File File { get; set; }
    }
}
