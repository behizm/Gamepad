using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    public class Cast : BaseEntity
    {
        public CastType CastType { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string Value { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string FaValue { get; set; }


        public virtual ICollection<Article> Articles { get; set; }
    }

    public enum CastType
    {
        Developer = 0,
        Publisher = 1,
        Brand = 2
    }
}
