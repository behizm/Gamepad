using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
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
            var result = GpServices.Role.Insert("firstRole");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Rename()
        {
            var result = GpServices.Role.Rename("FirstRoles","FirstRole");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Search()
        {
            var result = GpServices.Role.Search(new RoleSearchModel
            {
                Rolename = "r"
            }, new Ordering<Role,DateTime>
            {
                OrderByKeySelector = r => r.CreateDate
            });
            Assert.IsNotNull(result);
            Console.WriteLine(result.CountAll);
            result.List.ToList().ForEach(x => Console.WriteLine(x.Name));
        }

        [TestMethod]
        public void Delete()
        {
            var result = GpServices.Role.Delete("FirstRoles");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}
