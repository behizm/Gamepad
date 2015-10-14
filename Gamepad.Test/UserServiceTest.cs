using System;
using System.Linq;
using Gamepad.Service.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test
{
    [TestClass]
    public class UserServiceTest
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
        public void AddUser()
        {
            var result = Services.User.AddUserAsync(new UserAddViewModel
            {
                Email = "behi8303@yahoo.com",
                Username = "behi8303",
                Password = "123456"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
            

        }


    }
}
