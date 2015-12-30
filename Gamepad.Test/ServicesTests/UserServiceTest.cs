using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
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
            var result = GpServices.User.Insert(new User
            {
                Email = "test13@yahoo.com",
                Username = "test13",
            }, "123456");
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeUsername()
        {
            var result = GpServices.User.ChangeUsername("test2", "test3");
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeEmail()
        {
            var result = GpServices.User.ChangeEmail("behi8303", "behi8303@yahoo.com");
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangePassword()
        {
            var result = GpServices.User.ChangePassword("behi8303", "123456", "123456");
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ValidateUser()
        {
            var result = GpServices.User.ValidateUser("behi8303@yahoo.com");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateLogin()
        {
            var result = GpServices.User.ValidateLogin("behi8303", "1234564");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ChangeEmailConfirmed()
        {
            var result = GpServices.User.ChangeEmailConfirmed("behi8303", true);
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeLock()
        {
            var result = GpServices.User.ChangeLock("behi8303", false);
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void GetUser()
        {
            var result = GpServices.User.FindByUsername("behi8303");
            Assert.IsNotNull(result);

            result = GpServices.User.FindByEmail("behi8303@yahoo.com");
            Assert.IsNotNull(result);

            result = GpServices.User.FindById(result.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Search()
        {
            var result = GpServices.User.Search(new UserSearchModel
            {
                Username = ""
            }, new Ordering<User, DateTime>
            {
                OrderByKeySelector = x => x.CreateDate
            });
            Assert.IsNotNull(result);
            Console.WriteLine(@"count : " + result.CountAll);
            Console.WriteLine(@"----------------- ");
            result.List.ToList().ForEach(x => Console.WriteLine(x.Username));
        }

        [TestMethod]
        public void ChangeAvatar()
        {
            var result = GpServices.User.ChangeAvatar("behi8303", Guid.Parse("0813fff5-0345-4798-aec1-4db3570346d5"));
            Assert.IsNotNull(result);
            Console.WriteLine(result);

            var user = GpServices.User.FindByUsername("behi8303");
            Assert.IsNotNull(user);
            Console.WriteLine(user.Avatar.File.Title);
        }

        [TestMethod]
        public void EditProfile()
        {
            var user1 = GpServices.User.FindByUsername("behi8303");
            Assert.IsNotNull(user1);

            var result = GpServices.User.UpdateProfile(new Profile
            {
                UserId = user1.Id,
                Firstname = "behnam",
                Lastname = "zeighami",
                Company = "best company",
                Website = ".com"
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            var user = GpServices.User.FindByUsername("behi8303");
            Assert.IsNotNull(user);
            Console.WriteLine(user.Profile.Firstname);
        }

        [TestMethod]
        public void ChangeProfileType()
        {
            var result = GpServices.User.ChangeProfileType("behi8303", ProfileType.Legal);
            Assert.IsTrue(result.Succeeded, result.LastError);

            var user = GpServices.User.FindByUsername("behi8303");
            Assert.IsNotNull(user);
            Console.WriteLine(user.Profile.ProfileType);
        }

        [TestMethod]
        public void ChangeTrustRate()
        {
            var result = GpServices.User.ChangeTrustRate("behizm", new TrustRate
            {
                Rate = 35,
                Comment = "good user",
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            var user = GpServices.User.FindById(Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3"));
            Console.WriteLine(@"av : " + user.Profile.TrustRateAverage);
            foreach (var trustRate in user.Profile.TrustRates)
            {
                Console.WriteLine(trustRate.Rate);
            }
        }

        [TestMethod]
        public void AddToRole()
        {
            var result = GpServices.User.AddToRole("behi8303", "FirstRole");
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void RemoveFromRole()
        {
            var result = GpServices.User.RemoveFromRole("behi8303", "FirstRole");
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void IsInRole()
        {
            var result = GpServices.User.IsInRole("behi8303", "FirstRole");
            Assert.IsTrue(result);
        }
    }
}
