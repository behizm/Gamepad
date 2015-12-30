using System;
using System.ComponentModel.DataAnnotations;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Models.ViewModels
{
    public class SystemHardwareAddModel
    {
        [Display(Name = @"نوع سخت افزار")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Mandatory", ErrorMessage = null)]
        public SystemHardwareType? HardwareType { get; set; }

        [Display(Name = @"نام سخت افزار")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Re", ErrorMessage = null)]
        [StringLength(100, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Name { get; set; }
    }

    public class SystemHardwareEditModel : SystemHardwareAddModel
    {
        [Display(Name = @"سخت افزار")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Mandatory", ErrorMessage = null)]
        public Guid? Id { get; set; }
    }

    public class SystemHardwareSearchModel
    {
        public SystemHardwareType? HardwareType { get; set; }
        public string Name { get; set; }
    }
}
