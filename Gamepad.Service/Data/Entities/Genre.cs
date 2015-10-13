using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    internal class Genre : BaseEntity
    {
        [Required, StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        [Required, StringLength(25, MinimumLength = 2)]
        public string FaName { get; set; }

        [Required, StringLength(250)]
        public string Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
