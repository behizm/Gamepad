using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    internal class Role : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; }


        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
