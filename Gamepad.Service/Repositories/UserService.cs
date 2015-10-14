using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Liberary;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;
using Gamepad.Utility.Helpers;

namespace Gamepad.Service.Repositories
{
    internal class UserService : BaseService, IUserService
    {
        public IUserService Clone()
        {
            return new UserService();
        }

        public async Task<OperationResult> AddUserAsync(UserAddViewModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var passwordHashed = await GamepadHashSystem.EncryptAsync(model.Password);
            if (passwordHashed.IsNothing())
                return OperationResult.Failed(ErrorMessages.Services_General_OperationError);

            model.Username = model.Username.TrimAndLower();
            model.Email = model.Email.TrimAndLower();
            var user =
                await RepositoryContext.RetrieveAsync<User>(x => x.Username == model.Username || x.Email == model.Email);
            if (user != null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserExisted);

            var newUser = new User
            {
                AccessFailed = 0,
                Email = model.Email,
                IsActive = false,
                IsLock = false,
                PasswordHash = passwordHashed,
                Username = model.Username
            };
            await RepositoryContext.CreateAsync(newUser);
            return RepositoryContext.OperationResult;
        }


    }
}
