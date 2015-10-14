using System.ComponentModel.DataAnnotations;
using Gamepad.Service.Data;

namespace Gamepad.Service.Repositories
{
    internal abstract class BaseService
    {
        protected BaseService()
        {
            var context = new GamepadContext();
            RepositoryContext = new RepositoryContext<GamepadContext>(context);
            WarehouseContext = new WarehouseContext<GamepadContext>(context);
        }

        protected RepositoryContext<GamepadContext> RepositoryContext;

        protected WarehouseContext<GamepadContext> WarehouseContext;

        protected bool ValidateModel<T>(T model)
        {
            var vc = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, vc, null, true);
        }
    }
}
