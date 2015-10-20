using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Resources;
using Gamepad.Utility.Helpers;

namespace Gamepad.Service.Models.ViewModels
{
    public class FileAddModel : UserBaseModel
    {
        [Display(Name = @"عنوان فایل")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Title { get; set; }

        [Display(Name = @"نام فایل")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Filename { get; set; }

        public int Size { get; set; }

        public FileType FileType { get; set; }

        public FileCategory Category { get; set; }

        [Display(Name = @"آدرس فایل")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        public string Address { get; set; }

        public bool IsPublic { get; set; }
    }
}
