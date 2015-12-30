using System;
using System.ComponentModel.DataAnnotations;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Helpers;

namespace Gamepad.Service.Models.ViewModels
{
    public class UserBaseModel
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

    public class UserModel : UserBaseModel
    {
        private string _email;

        [Display(Name = @"ایمیل")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 5, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        [RegularExpression(@"^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_RegularExperssion", ErrorMessage = null)]
        public string Email
        {
            get { return _email; }
            set { _email = value.TrimAndLower(); }
        }
    }

    public class UserEmailModel
    {
        private string _email;

        [Display(Name = @"ایمیل")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 5, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        [RegularExpression(@"^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_RegularExperssion", ErrorMessage = null)]
        public string Email
        {
            get { return _email; }
            set { _email = value.TrimAndLower(); }
        }
    }

    public class UserAddModel : UserModel
    {
        [Display(Name = @"پسورد")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Password { get; set; }
    }

    public class UserChangeUsernameModel : UserBaseModel
    {
        private string _newusername;

        [Display(Name = @"نام کاربری جدید")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(25, MinimumLength = 5, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        [RegularExpression("^[a-z0-9._-]{5,25}$", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_RegularExperssion", ErrorMessage = null)]
        public string NewUsername {
            get { return _newusername; }
            set { _newusername = value.TrimAndLower(); }
        }
    }

    public class UserChangePassModel : UserBaseModel
    {
        [Display(Name = @"پسورد قبلی")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string OldPassword { get; set; }

        [Display(Name = @"پسورد")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Password { get; set; }
    }

    public class UserValidateNameModel
    {
        private string _userword;

        [Display(Name = @"نام کاربری یا ایمیل")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 5, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        [RegularExpression(@"(^[a-z0-9._-]{5,25}$)|(^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$)", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_RegularExperssion", ErrorMessage = null)]
        public string Userword {
            get { return _userword; }
            set { _userword = value.TrimAndLower(); }
        }
    }

    public class UserValidatePassModel : UserBaseModel
    {
        [Display(Name = @"پسورد")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_Required", ErrorMessage = null)]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "Validation_General_StringLengthBound", ErrorMessage = null)]
        public string Password { get; set; }
    }

    public class UserActiveModel : UserBaseModel
    {
        public bool IsEmailConfirmed { get; set; }
    }

    public class UserLockModel : UserBaseModel
    {
        public bool IsLocked { get; set; }
    }

    public class UserDetailsModel : UserModel
    {
        public bool IsEmailConfirmed { get; set; }

        public short AccessFailed { get; set; }

        public bool IsLock { get; set; }

        public DateTime? LockedDate { get; set; }
    }

    public class UserSearchModel
    {
        private string _username;
        public string Username {
            get { return _username; }
            set { _username = value.TrimAndLower(); }
        }

        private string _email;
        public string Email {
            get { return _email; }
            set { _email = value.TrimAndLower(); }
        }

        public bool? IsEmailConfirmed { get; set; }

        public bool? IsLock { get; set; }

        public DateTime? CreateDateFrom { get; set; }

        public DateTime? CreateDateTo { get; set; }

        public DateTime? LockedDateFrom { get; set; }

        public DateTime? LockedDateTo { get; set; }

        public DateTime? LastLoginDateFrom { get; set; }

        public DateTime? LastLoginDateTo { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Company { get; set; }

        public string Alias { get; set; }

        public ProfileType? ProfileType { get; set; }
    }

    public class UserAvatarModel : UserBaseModel
    {
        public Guid AvatarId { get; set; }
    }
}
