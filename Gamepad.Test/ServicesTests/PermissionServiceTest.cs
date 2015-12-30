using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
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
            var result = GpServices.Permission.Insert(new Permission
            {
                Area = "admin",
                Controller = "User",
                Action = "createUser"
            });
            Assert.IsTrue(result.Succeeded, result.LastError);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void GetByRoute()
        {
            Console.WriteLine(@"permission get by route ...");
            var result = GpServices.Permission.FindByRoute("admin", "User", "createUser");
            Assert.IsNotNull(result, "failed. (get by route test)");
            permissionId = result.Id;
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void Update()
        {
            var permission = GpServices.Permission.FindById(permissionId);
            Assert.IsNotNull(permission);
            Console.WriteLine(@"update permission ...");
            permission.Action = "UpdateUser";
            var result = GpServices.Permission.Update(permission);
            Assert.IsTrue(result.Succeeded, result.LastError);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void GetById()
        {
            Console.WriteLine(@"permission get by id ...");
            var result = GpServices.Permission.FindById(permissionId);
            Assert.IsNotNull(result, "failed. (get by id test)");
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void AddToRole()
        {
            Console.WriteLine(@"add permission to role ...");
            var result = GpServices.Permission.AddToRole(permissionId, "admin");
            Assert.IsTrue(result.Succeeded, result.LastError);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void IsInRole()
        {
            Console.WriteLine(@"is permission in role ...");
            var result = GpServices.Permission.IsInRole(permissionId, "admin");
            Assert.IsNotNull(result, "failed. (is in role test)");
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void RemoveFromRole()
        {
            Console.WriteLine(@"remove permission from role ...");
            var result = GpServices.Permission.RemoveFromRole(permissionId, "admin");
            Assert.IsTrue(result.Succeeded, result.LastError);
            Console.WriteLine(@"... pass.");
        }

        [TestMethod]
        private void Delete()
        {
            var permission = GpServices.Permission.FindById(permissionId);
            Assert.IsNotNull(permission);
            Console.WriteLine(@"delete permission ...");
            var result = GpServices.Permission.Delete(permissionId);
            Assert.IsTrue(result.Succeeded, result.LastError);
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
