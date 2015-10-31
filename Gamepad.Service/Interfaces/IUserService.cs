using System;
using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Utility.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IUserService : IBaseService<IUserService>
    {
        Task<OperationResult> AddUserAsync(UserAddModel model);
        Task<OperationResult> ChangeUsernameAsync(UserChangeUsernameModel model);
        Task<OperationResult> ChangeEmailAsync(UserModel model);
        Task<OperationResult> ChangePasswordAsync(UserChangePassModel model);
        Task<OperationResult> ValidateUsernameAsync(UserValidateNameModel model);
        Task<OperationResult> ValidatePasswordAsync(UserValidatePassModel model);
        Task<OperationResult> ChangeEmailConfirmedAsync(UserActiveModel model);
        Task<OperationResult> ChangeLockAsync(UserLockModel model);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(UserBaseModel username);
        Task<User> GetByEmailAsync(UserEmailModel email);
        Task<Cluster<User>> SearchAsync<TOrderingKey>(UserSearchModel model, Ordering<User, TOrderingKey> ordering);
        Task<OperationResult<string>> ChangeAvatarAsync(UserAvatarModel model);
        Task<OperationResult> EditProfileAsync(ProfileAddModel model);
        Task<OperationResult> ChangeProfileTypeAsync(ProfileChangeTypeModel model);
        Task<OperationResult> ChangeTrustRateAsync(ProfileChangeTrustRateModel model);
        Task<TrustRate> GetUserTrustRatingAsync(ProfileTrustRateModel model);
        Task<OperationResult> AddToRoleAsync(RoleUserModel model);
        Task<OperationResult> RemoveFromRoleAsync(RoleUserModel model);
        Task<bool?> IsInRoleAsync(RoleUserModel model);
    }
}
