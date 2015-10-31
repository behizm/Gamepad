using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;
using Gamepad.Utility.Helpers;
using Gamepad.Utility.Models;

namespace Gamepad.Service.Repositories
{
    internal class PermissionService : BaseService,IPermissionService
    {
        public IPermissionService Shadow()
        {
            return new PermissionService();
        }

        public async Task<OperationResult> CreateAsync(PermissionBaseModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var permission =
                await
                    RepositoryContext.RetrieveAsync<Permission>(
                        x => x.Area == model.Area && x.Controller == model.Controller && x.Action == model.Action);
            if (permission != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Existed_.Fill("دسترسی"));

            var newPermission = new Permission
            {
                Action = model.Action,
                Area = model.Area,
                Controller = model.Controller
            };
            await RepositoryContext.CreateAsync(newPermission);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> UpdateAsync(PermissionEditModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var permission = await RepositoryContext.RetrieveAsync<Permission>(x => x.Id == model.Id);
            if (permission == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("دسترسی"));

            permission.Action = model.Action;
            permission.Area = model.Area;
            permission.Controller = model.Controller;
            await RepositoryContext.UpdateAsync(permission);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var permission = await RepositoryContext.RetrieveAsync<Permission>(x => x.Id == id);
            if (permission == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("دسترسی"));

            await RepositoryContext.DeleteAsync(permission);
            return RepositoryContext.OperationResult;
        }

        public async Task<Permission> GetByIdAsync(Guid id)
        {
            return await RepositoryContext.RetrieveAsync<Permission>(p => p.Id == id);
        }

        public async Task<Permission> GetByRouteAsync(PermissionBaseModel model)
        {
            if (!ValidateModel(model))
                return null;

            return await
                RepositoryContext.RetrieveAsync<Permission>(
                    x => x.Area == model.Area && x.Controller == model.Controller && x.Action == model.Action);
        }

        public async Task<Cluster<Permission>> SearchAsync<TOrderingKey>(PermissionSearchModel model, Ordering<Permission, TOrderingKey> ordering)
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

            var resultTask = RepositoryContext.SearchAsync(expression);
            var countTask = RepositoryContext.Shadow().CountAsync(expression);

            var result = await resultTask;
            if (result == null)
                return null;

            var count = await countTask;
            if (count == null)
                return null;

            var rvalue = new Cluster<Permission>();
            try
            {
                if (ordering.IsAscending)
                {
                    rvalue.List =
                        await result.OrderBy(ordering.OrderByKeySelector)
                            .Skip(ordering.Skip)
                            .Take(ordering.Take)
                            .ToListAsync();
                }
                else
                {
                    rvalue.List =
                        await result.OrderByDescending(ordering.OrderByKeySelector)
                            .Skip(ordering.Skip)
                            .Take(ordering.Take)
                            .ToListAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }

            rvalue.CountAll = count.Value;
            return rvalue;
        }

        public async Task<OperationResult> AddToRoleAsync(RolePermissionModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);
            
            var role = await RepositoryContext.RetrieveAsync<Role>(x => x.Name == model.Rolename);
            if (role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("نقش"));

            var permission = await RepositoryContext.RetrieveAsync<Permission>(x => x.Id == model.PermissionId);
            if (permission == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("دسترسی"));

            if (role.Permissions.Any(p => p.Id == permission.Id))
                return OperationResult.Success;

            role.Permissions.Add(permission);
            await RepositoryContext.UpdateAsync(role);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> RemoveFromRoleAsync(RolePermissionModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var role = await RepositoryContext.RetrieveAsync<Role>(x => x.Name == model.Rolename);
            if (role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("نقش"));

            var permission = await RepositoryContext.RetrieveAsync<Permission>(x => x.Id == model.PermissionId);
            if (permission == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("دسترسی"));

            if (role.Permissions.All(p => p.Id != permission.Id))
                return OperationResult.Success;

            role.Permissions.Remove(permission);
            await RepositoryContext.UpdateAsync(role);
            return RepositoryContext.OperationResult;
        }

        public async Task<bool?> IsInRoleAsync(RolePermissionModel model)
        {
            if (!ValidateModel(model))
                return null;

            var roleT = RepositoryContext.RetrieveAsync<Role>(x => x.Name == model.Rolename);
            var permissionT = RepositoryContext.Shadow().RetrieveAsync<Permission>(x => x.Id == model.PermissionId);

            var role = await roleT;
            if (role == null)
                return null;

            var permission = await permissionT;
            if (permission == null)
                return null;

            return role.Permissions.Any(u => u.Id == permission.Id);
        }
    }
}
