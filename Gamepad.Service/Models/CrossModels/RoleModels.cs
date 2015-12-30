using System;
using System.ComponentModel.DataAnnotations;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Helpers;

namespace Gamepad.Service.Models.ViewModels
{
    public class RoleBaseModel
    {
        private string _rolename;

        [Display(Name = @"نام نقش")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Rolename
        {
            get { return _rolename; }
            set { _rolename = value.TrimAndLower(); }
        }
    }

    public class RoleRenameModel : RoleBaseModel
    {
        private string _newName;

        [Display(Name = @"نام جدید نقش")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string NewName
        {
            get { return _newName; }
            set { _newName = value.TrimAndLower(); }
        }
    }

    public class RoleUserModel : RoleBaseModel
    {
        private string _username;

        [Display(Name = @"نام کاربری")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 5, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        [RegularExpression("^[a-z0-9._-]{5,25}$", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_RegularExperssion", ErrorMessage = null)]
        public string Username
        {
            get { return _username; }
            set { _username = value.TrimAndLower(); }
        }
    }

    public class RoleSearchModel
    {
        public string Rolename { get; set; }
    }

    public class PermissionBaseModel
    {
        private string _area;
        [Display(Name = @"ناحیه")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(30, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Area
        {
            get { return _area; }
            set { _area = value.TrimAndLower(); }
        }

        private string _controller;
        [Display(Name = @"کنترلر")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(30, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Controller
        {
            get { return _controller; }
            set { _controller = value.TrimAndLower(); }
        }

        private string _action;
        [Display(Name = @"اکشن")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(30, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLength", ErrorMessage = null)]
        public string Action
        {
            get { return _action; }
            set { _action = value.TrimAndLower(); }
        }
    }

    public class PermissionEditModel : PermissionBaseModel
    {
        public Guid Id { get; set; }
    }

    public class PermissionSearchModel
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }

    public class RolePermissionModel : RoleBaseModel
    {
        public Guid PermissionId { get; set; }
    }
}
