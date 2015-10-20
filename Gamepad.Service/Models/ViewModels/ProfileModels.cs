using System.ComponentModel.DataAnnotations;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Models.ViewModels
{
    public class ProfileAddModel : UserBaseModel
    {
        [Display(Name = @"نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Firstname { get; set; }

        [Display(Name = @"نام خانوادگی")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Lastname { get; set; }

        [Display(Name = @"عنوان تجاری")]
        [StringLength(100, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Company { get; set; }

        [Display(Name = @"نام مستعار")]
        [StringLength(50, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Alias { get; set; }

        [Display(Name = @"وب سایت")]
        [StringLength(300, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Website { get; set; }
    }

    public class ProfileChangeTypeModel : UserBaseModel
    {
        public ProfileType ProfileType { get; set; }
    }
}
