using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Models.ViewModels
{
    public class UserAddViewModel
    {
        [Required, StringLength(25, MinimumLength = 5)]
        [RegularExpression("^[a-z0-9._-]{5,25}$")]
        public string Username { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$")]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
