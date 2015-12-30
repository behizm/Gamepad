using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IRoleService : IBaseService<IRoleService, Role>
    {
        Role FindByName(string name);
        OperationResult Insert(string roleName);
        OperationResult Rename(string newRoleName, string oldRoleName);
        OperationResult Delete(string roleName);
        Cluster<Role> Search<TOrderingKey>(RoleSearchModel model, Ordering<Role, TOrderingKey> ordering);
    }
}
