using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Utility.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test
{
    [TestClass]
    public class RoleServiceTest
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

        [TestMethod]
        public void Add()
        {
            var result = Services.Role.CreateAsync(new RoleBaseModel
            {
                Rolename = "firstRole"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void Rename()
        {
            var result = Services.Role.RenameAsync(new RoleRenameModel
            {
                Rolename = "FirstRoles",
                NewName = "FirstRole"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void Delete()
        {
            var result = Services.Role.DeleteAsync(new RoleBaseModel
            {
                Rolename = "FirstRole"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void Search()
        {
            var result = Services.Role.SearchAsync(new RoleSearchModel
            {
                Rolename = "r"
            }, new Ordering<Role,DateTime>
            {
                OrderByKeySelector = r => r.CreateDate
            }).Result;
            Assert.IsNotNull(result);
            Console.WriteLine(result.CountAll);
            result.List.ToList().ForEach(x => Console.WriteLine(x.Name));
        }
    }
}
