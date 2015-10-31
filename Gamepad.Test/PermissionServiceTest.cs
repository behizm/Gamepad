using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Utility.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test
{
    [TestClass]
    public class PermissionServiceTest
    {
        [ClassInitialize]
        public static void ClassSetup(TestContext context)
        {
            var stringList = AppDomain.CurrentDomain.BaseDirectory.Split('\\').ToList();
            stringList[stringList.Count - 3] = "Gamepad.Web";
            stringList[stringList.Count - 2] = "App_Data";
            stringList.RemoveAt(stringList.Count - 1);
            var path = stringList.Aggregate((i, j) => i + "\\" + j);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private Guid permissionId;

        [TestMethod]
        private void Create()
        {
            Console.WriteLine(@"create permission ...");
            var result = Services.Permission.CreateAsync(new PermissionBaseModel
            {
                Area = "admin",
                Controller = "User",
                Action = "createUser"
            }).Result;
            Assert.IsTrue(result.Succeeded,
                "... failed." + Environment.NewLine + result.Errors != null
                    ? result.Errors.FirstOrDefault()
                    : string.Empty);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void GetByRoute()
        {
            Console.WriteLine(@"permission get by route ...");
            var result = Services.Permission.GetByRouteAsync(new PermissionBaseModel
            {
                Area = "admin",
                Controller = "User",
                Action = "createUser"
            }).Result;
            Assert.IsNotNull(result, "failed. (get by route test)");
            permissionId = result.Id;
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void Update()
        {
            Console.WriteLine(@"update permission ...");
            var result = Services.Permission.UpdateAsync(new PermissionEditModel
            {
                Area = "admin",
                Controller = "User1",
                Action = "createUser",
                Id = permissionId
            }).Result;
            Assert.IsTrue(result.Succeeded,
                "failed. (update test)" + Environment.NewLine + result.Errors != null
                    ? result.Errors.FirstOrDefault()
                    : string.Empty);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void GetById()
        {
            Console.WriteLine(@"permission get by id ...");
            var result = Services.Permission.GetByIdAsync(permissionId).Result;
            Assert.IsNotNull(result, "failed. (get by id test)");
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void AddToRole()
        {
            Console.WriteLine(@"add permission to role ...");
            var result = Services.Permission.AddToRoleAsync(new RolePermissionModel
            {
                Rolename = "superadmin",
                PermissionId = permissionId
            }).Result;
            Assert.IsTrue(result.Succeeded,
                "failed. (add to role test)" + Environment.NewLine + result.Errors != null
                    ? result.Errors.FirstOrDefault()
                    : string.Empty);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void IsInRole()
        {
            Console.WriteLine(@"is permission in role ...");
            var result = Services.Permission.IsInRoleAsync(new RolePermissionModel
            {
                Rolename = "superadmin",
                PermissionId = permissionId
            }).Result;
            Assert.IsNotNull(result, "failed. (is in role test)");
            Assert.IsTrue(result.Value);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void RemoveFromRole()
        {
            Console.WriteLine(@"remove permission from role ...");
            var result = Services.Permission.RemoveFromRoleAsync(new RolePermissionModel
            {
                Rolename = "superadmin",
                PermissionId = permissionId
            }).Result;
            Assert.IsTrue(result.Succeeded,
                "failed. (remove from role test)" + Environment.NewLine + result.Errors != null
                    ? result.Errors.FirstOrDefault()
                    : string.Empty);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void Delete()
        {
            Console.WriteLine(@"delete permission ...");
            var result = Services.Permission.DeleteAsync(permissionId).Result;
            Assert.IsTrue(result.Succeeded,
                "failed. (delete test)" + Environment.NewLine + result.Errors != null
                    ? result.Errors.FirstOrDefault()
                    : string.Empty);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        public void Overall()
        {
            Create();
            GetByRoute();
            Update();
            GetById();
            AddToRole();
            IsInRole();
            RemoveFromRole();
            Delete();
        }
    }
}
