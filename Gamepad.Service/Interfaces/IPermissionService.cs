using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IPermissionService : IBaseService<IPermissionService, Permission>
    {
        Permission FindByRoute(string area, string controller, string action);
        Cluster<Permission> Search<TOrderingKey>(PermissionSearchModel model, Ordering<Permission, TOrderingKey> ordering);
        OperationResult AddToRole(Guid permissionId, string roleName);
        OperationResult RemoveFromRole(Guid permissionId, string roleName);
        bool IsInRole(Guid permissionId, string roleName);
    }
}
