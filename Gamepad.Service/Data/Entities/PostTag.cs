using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    public class PostTag : BaseEntity
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
    }
}
