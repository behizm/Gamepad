using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    public class Tag : BaseEntity
    {
        [Required, StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
