using System.Data.Entity;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Liberary;
using Gamepad.Utility.Async;

namespace Gamepad.Service.Data
{
    internal class GamepadContextInitializer : CreateDatabaseIfNotExists<GamepadContext>
    {
        protected override void Seed(GamepadContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }
}
