using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class TrustRate : BaseEntity
    {
        public short Rate { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }



        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
