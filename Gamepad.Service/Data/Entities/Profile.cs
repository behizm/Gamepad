using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    internal class Profile
    {
        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string Firstname { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(300)]
        public string Website { get; set; }

        public ProfileType ProfileType { get; set; }


        [Key, ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }

    public enum ProfileType
    {
        Actual = 0,
        Legal = 1
    }
}
