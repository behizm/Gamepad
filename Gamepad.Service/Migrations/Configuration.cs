using System.Data.Entity.Migrations;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Liberary;

namespace Gamepad.Service.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GamepadContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GamepadContext context)
        {
            context.Seed(context);
        }
    }
}
