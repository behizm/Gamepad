using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    internal class PostTag : BaseEntity
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
    }
}
