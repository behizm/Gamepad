using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    public class Permission : BaseEntity
    {
        [Required, StringLength(30)]
        public string Area { get; set; }

        [Required, StringLength(30)]
        public string Controller { get; set; }

        [Required, StringLength(30)]
        public string Action { get; set; }


        public virtual ICollection<Role> Roles { get; set; }
    }
}
