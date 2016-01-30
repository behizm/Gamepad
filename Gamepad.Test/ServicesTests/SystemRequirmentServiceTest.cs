using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class SystemRequirmentServiceTest
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
        public void Insert()
        {
            var result = GpServices.SystemRequirmentService.Insert(new SystemRequirment
            {
                ArticleId = Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"),
                SystemHardwareId = Guid.Parse("43a30355-baa8-431a-80aa-4159d32da2c5"),
                RequirmentType = SystemRequirmentType.Recommend
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Delete()
        {
            var result = GpServices.SystemRequirmentService.Delete(Guid.Parse("a6066fb5-da46-4a3b-8d4a-9c3fa24f5997"));
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}