using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Utility.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IRoleService : IBaseService<IRoleService>
    {
        Task<OperationResult> CreateAsync(RoleBaseModel model);
        Task<OperationResult> RenameAsync(RoleRenameModel model);
        Task<OperationResult> DeleteAsync(RoleBaseModel model);
        Task<Role> GetRoleByName(RoleBaseModel model);
        Task<Cluster<Role>> SearchAsync<TOrderingKey>(RoleSearchModel model, Ordering<Role, TOrderingKey> ordering);
    }
}
