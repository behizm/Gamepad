using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Liberary;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;
using Gamepad.Utility.Helpers;
using Gamepad.Utility.Models;

namespace Gamepad.Service.Repositories
{
    internal class UserService : BaseService, IUserService
    {
        public IUserService Clone()
        {
            return new UserService();
        }

        public async Task<OperationResult> AddUserAsync(UserAddModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var passwordHashed = await GamepadHashSystem.EncryptAsync(model.Password);
            if (passwordHashed.IsNothing())
                return OperationResult.Failed(ErrorMessages.Services_General_OperationError);

            var user =
                await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username || x.Email == model.Email);
            if (user != null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserExisted);

            var newUser = new User
            {
                AccessFailed = 0,
                Email = model.Email,
                IsEmailConfirmed = false,
                IsLock = false,
                PasswordHash = passwordHashed,
                Username = model.Username
            };
            await RepositoryContext.CreateAsync(newUser);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> ChangeUsernameAsync(UserChangeUsernameModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            if (model.Username == model.NewUsername)
                return OperationResult.Success;

            var user = await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            var count = await RepositoryContext.CountAsync<User>(x => x.Username == model.NewUsername);
            if (count == null)
                return OperationResult.Failed(ErrorMessages.Services_General_OperationError);

            if (count != 0)
                return OperationResult.Failed(ErrorMessages.Services_User_UsernameExisted);

            user.Username = model.NewUsername;
            await RepositoryContext.UpdateAsync(user);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> ChangeEmailAsync(UserModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            if (model.Email == user.Email)
                return OperationResult.Success;

            var count = await RepositoryContext.CountAsync<User>(x => x.Email == model.Email);
            if (count == null)
                return OperationResult.Failed(ErrorMessages.Services_General_OperationError);

            if (count != 0)
                return OperationResult.Failed(ErrorMessages.Services_User_EmailExisted);

            user.Email = model.Email;
            await RepositoryContext.UpdateAsync(user);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> ChangePasswordAsync(UserChangePassModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            var passwordHashed = await GamepadHashSystem.EncryptAsync(model.OldPassword);
            if (passwordHashed.IsNothing())
                return OperationResult.Failed(ErrorMessages.Services_General_OperationError);

            if (user.PasswordHash != passwordHashed)
                return OperationResult.Failed(ErrorMessages.Services_User_InvalidPassword);

            var newPasswordHashed = await GamepadHashSystem.EncryptAsync(model.Password);
            if (newPasswordHashed.IsNothing())
                return OperationResult.Failed(ErrorMessages.Services_General_OperationError);

            user.PasswordHash = newPasswordHashed;
            await RepositoryContext.UpdateAsync(user);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> ValidateUsernameAsync(UserValidateNameModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            User user = null;
            if (Regex.IsMatch(model.Userword, "^[a-z0-9._-]{5,25}$"))
            {
                user = await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Userword);
            }
            else if (Regex.IsMatch(model.Userword, @"^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$"))
            {
                user = await RepositoryContext.RetrieveAsync<User>(x => x.Email == model.Userword);
            }

            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            if (!user.IsEmailConfirmed)
                return OperationResult.Failed(ErrorMessages.Services_User_Unconfirmed);

            return user.IsLock && user.LockedDate > DateTime.Now.AddMinutes(-15)
                ? OperationResult.Failed(ErrorMessages.Services_User_UserIsLocked)
                : OperationResult.Success;
        }

        public async Task<OperationResult> ValidatePasswordAsync(UserValidatePassModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            if (!user.IsEmailConfirmed)
                return OperationResult.Failed(ErrorMessages.Services_User_Unconfirmed);

            if (user.IsLock && user.LockedDate > DateTime.Now.AddMinutes(-15))
                return OperationResult.Failed(ErrorMessages.Services_User_UserIsLocked);

            var passwordHashed = await GamepadHashSystem.EncryptAsync(model.Password);
            if (passwordHashed.IsNothing())
                return OperationResult.Failed(ErrorMessages.Services_General_OperationError);

            if (user.PasswordHash == passwordHashed)
            {
                user.AccessFailed = 0;
                user.IsLock = false;
                user.LastLoginDate = DateTime.Now;
                await RepositoryContext.UpdateAsync(user);
                return OperationResult.Success;
            }

            user.AccessFailed++;
            if (user.AccessFailed >= 5)
            {
                user.IsLock = true;
                user.LockedDate = DateTime.Now;
            }
            await RepositoryContext.UpdateAsync(user);
            return OperationResult.Failed(ErrorMessages.Services_User_InvalidPassword);
        }

        public async Task<OperationResult> ChangeEmailConfirmedAsync(UserActiveModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            user.IsEmailConfirmed = model.IsEmailConfirmed;
            await RepositoryContext.UpdateAsync(user);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> ChangeLockAsync(UserLockModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            if (model.IsLocked)
            {
                user.IsLock = true;
                user.LockedDate = DateTime.MaxValue;
            }
            else
            {
                user.IsLock = false;
                user.AccessFailed = 0;
            }

            await RepositoryContext.UpdateAsync(user);
            return RepositoryContext.OperationResult;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await RepositoryContext.RetrieveAsync<User>(x => x.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(UserBaseModel model)
        {
            if (!ValidateModel(model))
                return null;

            return await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username);
        }

        public async Task<User> GetUserByEmailAsync(UserEmailModel model)
        {
            if (!ValidateModel(model))
                return null;

            return await RepositoryContext.RetrieveAsync<User>(x => x.Email == model.Email);
        }

        public async Task<Cluster<User>> SearchAsync<TOrderingKey>(UserSearchModel model, Ordering<User, TOrderingKey> ordering)
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

            var result = await RepositoryContext.SearchAsync(expression);
            if (result == null)
                return null;

            var rvalue = new Cluster<User>();
            try
            {
                if (ordering.IsAscending)
                {
                    rvalue.List =
                        await result.OrderBy(ordering.OrderByKeySelector)
                            .Skip(ordering.Skip)
                            .Take(ordering.Take)
                            .ToListAsync();
                }
                else
                {
                    rvalue.List =
                        await result.OrderByDescending(ordering.OrderByKeySelector)
                            .Skip(ordering.Skip)
                            .Take(ordering.Take)
                            .ToListAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }

            var count = await RepositoryContext.CountAsync(expression);
            if (count == null)
                return null;

            rvalue.CountAll = count.Value;
            return rvalue;
        }

        public async Task<OperationResult<string>> ChangeAvatarAsync(UserAvatarModel model)
        {
            if (!ValidateModel(model))
                return OperationResult<string>.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(u => u.Username == model.Username);
            if (user == null)
                return OperationResult<string>.Failed(ErrorMessages.Services_User_UserNotFound);

            var file = await RepositoryContext.RetrieveAsync<File>(a => a.Id == model.AvatarId);
            if (file == null)
                return OperationResult<string>.Failed(ErrorMessages.Services_User_AvatarNotFound);

            if (file.FileType != FileType.Image || file.Category != FileCategory.UserAvatar)
                return OperationResult<string>.Failed(ErrorMessages.Services_File_UnacceptableFileType);

            if (user.Avatar == null)
            {
                var avatar = new UserAvatar
                {
                    FileId = file.Id,
                    UserId = user.Id
                };
                await RepositoryContext.CreateAsync(avatar);
            }
            else
            {
                user.Avatar.FileId = file.Id;
                await RepositoryContext.UpdateAsync(user.Avatar);
            }
            OperationResult<string> result = RepositoryContext.OperationResult;
            if (result != null) result.Value = file.Address;
            return result;
        }

        public async Task<OperationResult> EditProfileAsync(ProfileAddModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(u => u.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            if (model.Company.IsNotNothing() && model.Alias.IsNothing())
                model.Alias = model.Company.Length <= 50 ? model.Company : model.Company.Substring(0, 50);

            if (user.Profile == null)
            {
                var profile = new Profile
                {
                    Alias = model.Alias,
                    Company = model.Company,
                    CreateDate = DateTime.Now,
                    Firstname = model.Firstname,
                    Lastname= model.Lastname,
                    ProfileType = ProfileType.Actual,
                    UserId = user.Id,
                    Website = model.Website,
                };
                await RepositoryContext.CreateAsync(profile);
            }
            else
            {
                user.Profile.Alias = model.Alias;
                user.Profile.Company = model.Company;
                user.Profile.Firstname = model.Firstname;
                user.Profile.Lastname = model.Lastname;
                user.Profile.Website = model.Website;
                await RepositoryContext.UpdateAsync(user.Profile);
            }

            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> ChangeProfileTypeAsync(ProfileChangeTypeModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(u => u.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            if (user.Profile == null)
                return OperationResult.Failed(ErrorMessages.Services_User_ProfileNotFound);

            if (model.ProfileType == ProfileType.Legal && (user.Profile.Company.IsNothing() || user.Profile.Website.IsNothing()))
                return OperationResult.Failed(ErrorMessages.Services_User_ProfileInvalidData);

            user.Profile.ProfileType = model.ProfileType;
            await RepositoryContext.UpdateAsync(user.Profile);
            return RepositoryContext.OperationResult;
        }
    }
}
