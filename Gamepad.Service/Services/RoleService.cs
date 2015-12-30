using System;
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
    internal class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(GamepadContext context) : base(context)
        {
        }

        public IRoleService Clone()
        {
            return new RoleService(new GamepadContext());
        }

        public Role FindByName(string name)
        {
            name = name.ToLower();
            return Get(x => x.Name.ToLower() == name);
        }

        public override OperationResult Insert(Role item)
        {
            var role = FindByName(item.Name);
            if (role != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Insert(item);
        }

        public OperationResult Insert(string roleName)
        {
            var role = FindByName(roleName);
            if (role != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            var newRole = new Role
            {
                Name = roleName
            };
            return base.Insert(newRole);
        }

        public OperationResult Rename(string newRoleName, string oldRoleName)
        {
            var role = FindByName(oldRoleName);
            if (role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);

            var exictedrole = FindByName(newRoleName);
            if (exictedrole != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            role.Name = newRoleName;
            return Update(role);
        }

        public OperationResult Delete(string roleName)
        {
            var role = FindByName(roleName);
            if (role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);

            return Delete(role);
        }

        public Cluster<Role> Search<TOrderingKey>(RoleSearchModel model, Ordering<Role, TOrderingKey> ordering)
        {
            Expression<Func<Role, bool>> expression;
            if (model == null)
            {
                expression = r => true;
            }
            else
            {
                expression = r =>
                    (string.IsNullOrEmpty(model.Rolename) || r.Name.Contains(model.Rolename));
            }
            return Search(expression, ordering);
        }

    }
}
