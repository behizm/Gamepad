using System;
using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Utility.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IPermissionService : IBaseService<IPermissionService>
    {
        Task<OperationResult> CreateAsync(PermissionBaseModel model);
        Task<OperationResult> UpdateAsync(PermissionEditModel model);
        Task<OperationResult> DeleteAsync(Guid id);
        Task<Permission> GetByIdAsync(Guid id);
        Task<Permission> GetByRouteAsync(PermissionBaseModel model);
        Task<Cluster<Permission>> SearchAsync<TOrderingKey>(PermissionSearchModel model, Ordering<Permission, TOrderingKey> ordering);
        Task<OperationResult> AddToRoleAsync(RolePermissionModel model);
        Task<OperationResult> RemoveFromRoleAsync(RolePermissionModel model);
        Task<bool?> IsInRoleAsync(RolePermissionModel model);
    }
}
