using System;
using System.Linq;
using System.Linq.Expressions;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Liberary;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Services
{
    internal class UserService : BaseService<User>, IUserService
    {
        public UserService(GamepadContext context) : base(context)
        {
        }

        public IUserService Clone()
        {
            return new UserService(new GamepadContext());
        }

        public User FindByUsername(string username)
        {
            username = username.ToLower();
            return Get(x => x.Username == username);
        }

        public User FindByEmail(string email)
        {
            email = email.ToLower();
            return Get(x => x.Username == email);
        }

        public bool ValidateUser(string userword)
        {
            userword = userword.ToLower();
            var user = Get(x => x.Username == userword || x.Email == userword);
            return user != null;
        }

        public bool ValidateUserData(string username, string email)
        {
            username = username.ToLower();
            email = email.ToLower();
            var user = Get(x => x.Username == username && x.Email == email);
            return user != null;
        }

        public bool ValidateLogin(string userword, string password)
        {
            userword = userword.ToLower();
            var passwordHashed = GamepadHashSystem.Encrypt(password);
            var user = Get(x => (x.Username == userword || x.Email == userword) && x.PasswordHash == passwordHashed);
            return user != null;
        }

        public OperationResult Insert(User user, string password)
        {
            var passwordHashed = GamepadHashSystem.Encrypt(password);
            user.PasswordHash = passwordHashed;
            return base.Insert(user);
        }

        public override OperationResult Insert(User user)
        {
            user.Username = user.Username.ToLower();
            user.Email = user.Email.ToLower();

            if (!ValidateUserData(user.Username, user.Email))
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }

            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                IsEmailConfirmed = false,
                IsLock = false,
                PasswordHash = user.PasswordHash
            };
            return base.Insert(newUser);
        }

        public override OperationResult Update(User user)
        {
            user.Username = user.Username.ToLower();
            user.Email = user.Email.ToLower();
            return base.Update(user);
        }

        public OperationResult ChangeUsername(string newUsername, string oldUsername)
        {
            var user = FindByUsername(oldUsername);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            user.Username = newUsername;
            return Update(user);
        }

        public OperationResult ChangeEmail(string username, string email)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            user.Email = email;
            return Update(user);
        }

        public OperationResult ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (!ValidateLogin(username, oldPassword))
            {
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);
            }
            user.PasswordHash = GamepadHashSystem.Encrypt(newPassword);
            return Update(user);
        }

        public OperationResult ChangeEmailConfirmed(string username, bool confirmValue)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            user.IsEmailConfirmed = confirmValue;
            return Update(user);
        }

        public OperationResult ChangeLock(string username, bool lockValue)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            user.IsLock = lockValue;
            return Update(user);
        }

        public Cluster<User> Search<TOrderingKey>(UserSearchModel model, Ordering<User, TOrderingKey> ordering)
        {
            Expression<Func<User, bool>> expression;
            if (model == null)
            {
                expression = u => true;
            }
            else
            {
                expression = u =>
                    (string.IsNullOrEmpty(model.Username) || u.Username.Contains(model.Username)) &&
                    (string.IsNullOrEmpty(model.Email) || u.Email.Contains(model.Email)) &&
                    (!model.IsEmailConfirmed.HasValue || u.IsEmailConfirmed == model.IsEmailConfirmed) &&
                    (!model.IsLock.HasValue || u.IsLock == model.IsLock) &&
                    (!model.CreateDateFrom.HasValue || u.CreateDate > model.CreateDateFrom) &&
                    (!model.CreateDateTo.HasValue || u.CreateDate < model.CreateDateTo) &&
                    (!model.LockedDateFrom.HasValue || u.LockedDate > model.CreateDateFrom) &&
                    (!model.LockedDateTo.HasValue || u.LockedDate < model.CreateDateTo) &&
                    (!model.LastLoginDateFrom.HasValue || u.LastLoginDate > model.LastLoginDateFrom) &&
                    (!model.LastLoginDateTo.HasValue || u.LastLoginDate < model.LastLoginDateTo) &&
                    (string.IsNullOrEmpty(model.Alias) || (u.Profile != null && u.Profile.Alias.Contains(model.Alias))) &&
                    (string.IsNullOrEmpty(model.Company) || (u.Profile != null && u.Profile.Company.Contains(model.Company))) &&
                    (string.IsNullOrEmpty(model.Firstname) || (u.Profile != null && u.Profile.Firstname.Contains(model.Firstname))) &&
                    (string.IsNullOrEmpty(model.Lastname) || (u.Profile != null && u.Profile.Lastname.Contains(model.Lastname))) &&
                    (!model.ProfileType.HasValue || (u.Profile != null && u.Profile.ProfileType == model.ProfileType));
            }
            return Search(expression, ordering);
        }

        public OperationResult AddToRole(string username, string roleName)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var role = GpServices.Role.FindByName(roleName);
            if (role == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (user.Roles.Any(r => r.Id == role.Id))
            {
                return OperationResult.Success;
            }
            user.Roles.Add(role);
            return Update(user);
        }

        public OperationResult RemoveFromRole(string username, string roleName)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var role = GpServices.Role.FindByName(roleName);
            if (role == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            if (user.Roles.All(r => r.Id != role.Id))
            {
                return OperationResult.Success;
            }
            user.Roles.Remove(role);
            return Update(user);
        }

        public bool IsInRole(string username, string roleName)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return false;
            }
            roleName = roleName.ToLower();
            return user.Roles.Any(x => x.Name.ToLower() == roleName);
        }

        public string ChangeAvatar(string username, Guid avatarId)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                return null;
            }
            var file = GpServices.File.FindById(avatarId);
            if (file == null)
            {
                return null;
            }
            if (user.Avatar == null)
            {
                user.Avatar = new UserAvatar
                {
                    FileId = file.Id,
                    UserId = user.Id
                };
                Update(user);
                return file.Address;
            }
            if (user.Avatar.FileId == file.Id)
            {
                return file.Address;
            }
            user.Avatar.FileId = file.Id;
            Update(user);
            return file.Address;
        }

        public OperationResult UpdateProfile(Profile profile)
        {
            var user = FindById(profile.UserId);
            if (user == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            user.Profile = profile;
            return Update(user);
        }

        public OperationResult ChangeProfileType(string username, ProfileType profileType)
        {
            var user = FindByUsername(username);
            if (user?.Profile == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            user.Profile.ProfileType = profileType;
            return Update(user);
        }

        public OperationResult ChangeTrustRate(string username, TrustRate rate)
        {
            var user = FindByUsername(username);
            if (user?.Profile == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            var trustRate = user.Profile.TrustRates.FirstOrDefault(x => x.Id == rate.Id);
            if (trustRate == null)
            {
                user.Profile.TrustRates.Add(rate);
                return Update(user);
            }
            if (rate.Rate == trustRate.Rate || rate.Comment == trustRate.Comment)
            {
                return OperationResult.Success;
            }
            trustRate.Rate = rate.Rate;
            trustRate.Comment = rate.Comment;
            user.Profile.TrustRateAverage =
                (short) (user.Profile.TrustRates.Sum(x => x.Rate)/user.Profile.TrustRates.Count());
            return Update(user);
        }

        // events :
        public event EventHandler<UserEventArgs> UserAdded;
        protected virtual void OnUserAdded(UserEventArgs e)
        {
            var handler = UserAdded;
            handler?.Invoke(this, e);
        }

        public event EventHandler<UserEventArgs> UserEdited;
        protected virtual void OnUserEdited(UserEventArgs e)
        {
            var handler = UserEdited;
            handler?.Invoke(this, e);
        }


    }
}
