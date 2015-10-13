using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Liberary;
using Gamepad.Utility.Async;

namespace Gamepad.Service.Repositories
{
    public class Mytest
    {
        public void AddUser()
        {
            var context = new GamepadContext();
            var adminUser = new User
            {
                AccessFailed = 0,
                Email = "behnam.zeighami@gmail.com",
                IsActive = true,
                IsLock = false,
                PasswordHash = AsyncTools.ConvertToSync(() => SquirrelHashSystem.EncryptAsync("admin")),
                Username = "admin",
                //Roles = new List<Role>
                //{
                //    new Role {Name = "superadmin"}
                //}
            };
            context.Users.Add(adminUser);
            context.SaveChanges();
        }

        public void AddRole()
        {
            var context = new GamepadContext();
            var item = new Role
            {
                Name = "mydearrole"
            };
            context.Roles.Add(item);
            context.SaveChanges();
        }
    }
}
