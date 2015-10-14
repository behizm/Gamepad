using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    internal class TrustRate : BaseEntity
    {
        public short Rate { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }



        [ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
