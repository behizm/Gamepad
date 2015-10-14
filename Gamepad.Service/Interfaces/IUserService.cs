using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;

namespace Gamepad.Service.Interfaces
{
    public interface IUserService : IBaseService<IUserService>
    {
        Task<OperationResult> AddUserAsync(UserAddViewModel model);
    }
}
