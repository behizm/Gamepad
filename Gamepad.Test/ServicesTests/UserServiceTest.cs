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
                Email = "test14@yahoo.com",
                Username = "test14",
            }, "123456");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeUsername()
        {
            var result = GpServices.User.ChangeUsername("test014", "test14");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeEmail()
        {
            var result = GpServices.User.ChangeEmail("test014", "test014@yahoo.com");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangePassword()
        {
            var result = GpServices.User.ChangePassword("test014", "123456", "123456789");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ValidateUser()
        {
            var result = GpServices.User.ValidateUser("behi8303@yahoo.com");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateUserPassword()
        {
            var result = GpServices.User.ValidateUserPassword("test014", "123456789");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ChangeEmailConfirmed()
        {
            var result = GpServices.User.ChangeEmailConfirmed("test014", false);
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeLock()
        {
            var result = GpServices.User.ChangeLock("test014", false);
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ValidateLogin()
        {
            var result = GpServices.User.ValidateLogin("test014", "123456789c");
            if (!result.Succeeded)
            {
                Console.WriteLine(result.LastError);
            }
            result = GpServices.User.SaveChanges();
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
                Username = "te"
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
        public void AddToRole()
        {
            var result = GpServices.User.AddToRole("test014", "FirstRole");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void IsInRole()
        {
            var result = GpServices.User.IsInRole("test014", "FirstRole");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveFromRole()
        {
            var result = GpServices.User.RemoveFromRole("test014", "FirstRole");
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeAvatar()
        {
            var result = GpServices.User.ChangeAvatar("test014", Guid.Parse("0813fff5-0345-4798-aec1-4db3570346d5"));
            Assert.IsNotNull(result);
            Console.WriteLine(result);
            var saveResult = GpServices.User.SaveChanges();
            Assert.IsTrue(saveResult.Succeeded, saveResult.LastError);
        }

        [TestMethod]
        public void EditProfile()
        {
            var user1 = GpServices.User.FindByUsername("test014");
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
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeProfileType()
        {
            var result = GpServices.User.ChangeProfileType("test014", ProfileType.Legal);
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeTrustRate()
        {
            var result = GpServices.User.ChangeTrustRate("test014", new TrustRate
            {
                Rate = 40,
                Comment = "good user test14",
                ProfileId = Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3")
            });
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.User.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);

            var user = GpServices.User.FindById(Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3"));
            Console.WriteLine(@"av : " + user.Profile.TrustRateAverage);
            foreach (var trustRate in user.Profile.TrustRates)
            {
                Console.WriteLine(trustRate.Rate);
            }
        }
    }
}
