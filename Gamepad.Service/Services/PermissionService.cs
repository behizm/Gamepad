using System;
using System.Linq;
using System.Linq.Expressions;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Services
{
    internal class PermissionService : BaseService<Permission>, IPermissionService
    {
        public PermissionService(GamepadContext context) : base(context)
        {
        }

        public IPermissionService Clone()
        {
            return new PermissionService(new GamepadContext());
        }

        public override OperationResult Insert(Permission item)
        {
            item.Action = item.Action.ToLower();
            item.Controller = item.Controller.ToLower();
            item.Area = item.Area.ToLower();

            var permission =
                Get(x => x.Area == item.Area && x.Controller == item.Controller && x.Action == item.Action);

            if (permission != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Insert(item);
        }

        public override OperationResult Update(Permission item)
        {
            item.Action = item.Action.ToLower();
            item.Controller = item.Controller.ToLower();
            item.Area = item.Area.ToLower();

            var permission =
                Get(
                    x =>
                        x.Area == item.Area && x.Controller == item.Controller && x.Action == item.Action &&
                        x.Id != item.Id);

            if (permission != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Update(item);
        }

        public Permission FindByRoute(string area, string controller, string action)
        {
            action = action.ToLower();
            controller = controller.ToLower();
            area = area.ToLower();
            return Get(x => x.Area == area && x.Controller == controller && x.Action == action);
        }

        public Cluster<Permission> Search<TOrderingKey>(PermissionSearchModel model, Ordering<Permission, TOrderingKey> ordering)
        {
            Expression<Func<Permission, bool>> expression;
            if (model == null)
            {
                expression = r => true;
            }
            else
            {
                expression = r =>
                    (string.IsNullOrEmpty(model.Area) || r.Area.Contains(model.Area)) &&
                    (string.IsNullOrEmpty(model.Controller) || r.Controller.Contains(model.Controller)) &&
                    (string.IsNullOrEmpty(model.Action) || r.Action.Contains(model.Action));
            }
            return Search(expression, ordering);
        }

        public OperationResult AddToRole(Guid permissionId, string roleName)
        {
            var permission = FindById(permissionId);
            var role = GpServices.Role.FindByName(roleName);
            if (permission == null || role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);

            if (role.Permissions.Any(p => p.Id == permission.Id))
                return OperationResult.Success;

            permission.Roles.Add(role);
            return base.Update(permission);
        }

        public OperationResult RemoveFromRole(Guid permissionId, string roleName)
        {
            var permission = FindById(permissionId);
            var role = GpServices.Role.FindByName(roleName);
            if (permission == null || role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            if (role.Permissions.All(p => p.Id != permission.Id))
                return OperationResult.Success;

            permission.Roles.Remove(role);
            return base.Update(permission);
        }

        public bool IsInRole(Guid permissionId, string roleName)
        {
            var permission = FindById(permissionId);
            var role = GpServices.Role.FindByName(roleName);
            if (permission == null || role == null)
                return false;

            return permission.Roles.Any(r => r.Id == role.Id);
        }

    }
}
