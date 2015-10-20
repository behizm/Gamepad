using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class Profile
    {
        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string Firstname { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        [StringLength(300)]
        public string Website { get; set; }

        public ProfileType ProfileType { get; set; }

        public short TrustRateAverage { get; set; }



        [Key, ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<TrustRate> TrustRates { get; set; }
    }

    public enum ProfileType
    {
        Actual = 0,
        Legal = 1
    }
}
