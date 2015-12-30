using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Helpers;

namespace Gamepad.Service.Models.ViewModels
{
    public class GenreAddModel
    {
        [Display(Name = @"نام ژانر")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 2, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Name { get; set; }

        [Display(Name = @"نام فارسی ژانر")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 2, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string FaName { get; set; }

        [Display(Name = @"توضیحات ژانر")]
        [StringLength(250, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Description { get; set; }
    }

    public class GenreEditModel : GenreAddModel
    {
        public Guid Id { get; set; }
    }

    public class GenreRemoveModel
    {
        private string _name;
        [Display(Name = @"نام ژانر")]
        [Required(ErrorMessageResourceType = typeof (ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 2, ErrorMessageResourceType = typeof (ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Name
        {
            get { return _name; }
            set { _name = value.TrimAndLower(); }
        }
    }

    public class GenreFindModel : GenreRemoveModel
    {
    }

    public class GenreFindFaModel
    {
        private string _faName;
        [Display(Name = @"نام فارسی ژانر")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 2, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string FaName
        {
            get { return _faName; }
            set { _faName = value.TrimAndLower(); }
        }
    }

    public class GenreSearchModel
    {
        private string _name;
        [Display(Name = @"نام ژانر")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 2, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Name
        {
            get { return _name; }
            set { _name = value.TrimAndLower(); }
        }

        private string _faName;
        [Display(Name = @"نام فارسی ژانر")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 2, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string FaName
        {
            get { return _faName; }
            set { _faName = value.TrimAndLower(); }
        }
    }
}
