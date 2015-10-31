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
    internal class RoleService : BaseService, IRoleService
    {
        public IRoleService Shadow()
        {
            return new RoleService();
        }

        public async Task<OperationResult> CreateAsync(RoleBaseModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var role = await RepositoryContext.RetrieveAsync<Role>(x => x.Name == model.Rolename);
            if (role != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Existed_.Fill("نقش"));

            var newRole = new Role
            {
                Name = model.Rolename
            };
            await RepositoryContext.CreateAsync(newRole);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> RenameAsync(RoleRenameModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var roleTask = RepositoryContext.RetrieveAsync<Role>(x => x.Name == model.Rolename);
            var dubRoleTask = RepositoryContext.Shadow().RetrieveAsync<Role>(x => x.Name == model.NewName);

            var role = await roleTask;
            if (role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("نقش"));

            var dubRole = await dubRoleTask;
            if (dubRole != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Existed_.Fill("نقش با این نام"));

            role.Name = model.NewName;
            await RepositoryContext.UpdateAsync(role);
            return RepositoryContext.OperationResult;
        }

        public async Task<OperationResult> DeleteAsync(RoleBaseModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var role = await RepositoryContext.RetrieveAsync<Role>(x => x.Name == model.Rolename);
            if (role == null)
                return OperationResult.Failed(ErrorMessages.Services_General_NotFound_.Fill("نقش"));

            if (role.Users.Any() || role.Permissions.Any())
                return OperationResult.Failed(ErrorMessages.Services_General_NonDeletable_.Fill("نقش"));

            await RepositoryContext.DeleteAsync(role);
            return RepositoryContext.OperationResult;
        }

        public async Task<Role> GetRoleByName(RoleBaseModel model)
        {
            if (!ValidateModel(model))
                return null;

            return await RepositoryContext.RetrieveAsync<Role>(r => r.Name == model.Rolename);
        }

        public async Task<Cluster<Role>> SearchAsync<TOrderingKey>(RoleSearchModel model, Ordering<Role, TOrderingKey> ordering)
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

            var resultTask = RepositoryContext.SearchAsync(expression);
            var countTask = RepositoryContext.Shadow().CountAsync(expression);

            var result = await resultTask;
            if (result == null)
                return null;

            var count = await countTask;
            if (count == null)
                return null;

            var rvalue = new Cluster<Role>();
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
        
    }
}
