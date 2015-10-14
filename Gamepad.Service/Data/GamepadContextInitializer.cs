using System.Data.Entity;

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
