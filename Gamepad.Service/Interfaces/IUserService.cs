using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IUserService : IBaseService<IUserService, User>
    {
        User FindByUsername(string username);
        User FindByEmail(string email);
        bool ValidateUser(string userword);
        bool ValidateUserData(string username, string email);
        bool ValidateLogin(string userword, string password);
        OperationResult Insert(User user, string password);
        OperationResult ChangeUsername(string newUsername, string oldUsername);
        OperationResult ChangeEmail(string username, string email);
        OperationResult ChangePassword(string username, string oldPassword, string newPassword);
        OperationResult ChangeEmailConfirmed(string username, bool confirmValue);
        OperationResult ChangeLock(string username, bool lockValue);
        Cluster<User> Search<TOrderingKey>(UserSearchModel model, Ordering<User, TOrderingKey> ordering);

        OperationResult AddToRole(string username, string roleName);
        OperationResult RemoveFromRole(string username, string roleName);
        bool IsInRole(string username, string roleName);

        string ChangeAvatar(string username, Guid avatarId);
        OperationResult UpdateProfile(Profile profile);
        OperationResult ChangeProfileType(string username, ProfileType profileType);
        OperationResult ChangeTrustRate(string username, TrustRate rate);

        // Events
        event EventHandler<UserEventArgs> UserAdded;
        event EventHandler<UserEventArgs> UserEdited;
    }
}
