using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Utility.Models;
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
            var result = Services.User.AddUserAsync(new UserAddModel
            {
                Email = "test2@yahoo.com",
                Username = "test2",
                Password = "123456"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void ChangeUsername()
        {
            var result = Services.User.ChangeUsernameAsync(new UserChangeUsernameModel
            {
                Username = "test2",
                NewUsername = "test3",
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void ChangeEmail()
        {
            var result = Services.User.ChangeEmailAsync(new UserModel
            {
                Username = "behi8303",
                Email = "behi8303@yahoo.com",
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void ChangePassword()
        {
            var result = Services.User.ChangePasswordAsync(new UserChangePassModel
            {
                Username = "behi8303",
                OldPassword = "123456",
                Password = "123456",
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void ValidateUsername()
        {
            var result = Services.User.ValidateUsernameAsync(new UserValidateNameModel
            {
                Userword = "behi8303@yahoo.com"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void ValidatePassword()
        {
            var result = Services.User.ValidatePasswordAsync(new UserValidatePassModel
            {
                Username = "behi8303",
                Password = "1234564"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void ChangeEmailConfirmed()
        {
            var result = Services.User.ChangeEmailConfirmedAsync(new UserActiveModel
            {
                Username = "behi8303",
                IsEmailConfirmed = true,
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void ChangeLock()
        {
            var result = Services.User.ChangeLockAsync(new UserLockModel
            {
                Username = "behi8303",
                IsLocked = false
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void GetUser()
        {
            var result = Services.User.GetByUsernameAsync(new UserBaseModel
            {
                Username = "behi8303",
            }).Result;
            Assert.IsNotNull(result);

            result = Services.User.GetByEmailAsync(new UserEmailModel
            {
                Email = "behi8303@yahoo.com",
            }).Result;
            Assert.IsNotNull(result);

            result = Services.User.GetByIdAsync(result.Id).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Search()
        {
            var result = Services.User.SearchAsync(new UserSearchModel
            {
                Username = ""
            }, new Ordering<User, DateTime>
            {
                OrderByKeySelector = x => x.CreateDate
            }).Result;
            Assert.IsNotNull(result);
            Console.WriteLine("count : " + result.CountAll);
            Console.WriteLine("----------------- ");
            result.List.ToList().ForEach(x => Console.WriteLine(x.Username));
        }

        [TestMethod]
        public void ChangeAvatar()
        {
            var result = Services.User.ChangeAvatarAsync(new UserAvatarModel
            {
                Username = "behi8303",
                AvatarId = Guid.Parse("0813fff5-0345-4798-aec1-4db3570346d5")
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
            Console.WriteLine(result.Value);

            var user = Services.User.GetByUsernameAsync(new UserBaseModel
            {
                Username = "behi8303"
            }).Result;
            Assert.IsNotNull(user);
            Console.WriteLine(user.Avatar.File.Title);
        }

        [TestMethod]
        public void EditProfile()
        {
            var result = Services.User.EditProfileAsync(new ProfileAddModel
            {
                Username = "behi8303",
                Firstname = "behnam",
                Lastname = "zeighami",
                Company = "best company",
                Website = ".com"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);

            var user = Services.User.GetByUsernameAsync(new UserBaseModel
            {
                Username = "behi8303"
            }).Result;
            Assert.IsNotNull(user);
            Console.WriteLine(user.Profile.Firstname);
        }

        [TestMethod]
        public void ChangeProfileType()
        {
            var result = Services.User.ChangeProfileTypeAsync(new ProfileChangeTypeModel
            {
                Username = "behi8303",
                ProfileType = ProfileType.Legal
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);

            var user = Services.User.GetByUsernameAsync(new UserBaseModel
            {
                Username = "behi8303"
            }).Result;
            Assert.IsNotNull(user);
            Console.WriteLine(user.Profile.ProfileType);
        }

        [TestMethod]
        public void ChangeTrustRate()
        {
            var result = Services.User.ChangeTrustRateAsync(new ProfileChangeTrustRateModel
            {
                Username = "behizm",
                Rate = 35,
                Comment = "good user",
                ProfileId = Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3")
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);

            var user = Services.User.GetByIdAsync(Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3")).Result;
            Console.WriteLine("av : " + user.Profile.TrustRateAverage);
            foreach (var trustRate in user.Profile.TrustRates)
            {
                Console.WriteLine(trustRate.Rate);
            }
        }

        [TestMethod]
        public void GetTrustRate()
        {
            var result = Services.User.GetUserTrustRatingAsync(new ProfileTrustRateModel
            {
                Username = "test1",
                ProfileId = Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3")
            }).Result;
            Assert.IsNotNull(result);
            Console.WriteLine(result.Rate);
        }

        [TestMethod]
        public void AddToRole()
        {
            var result = Services.User.AddToRoleAsync(new RoleUserModel
            {
                Rolename = "FirstRole",
                Username = "behi8303"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void RemoveFromRole()
        {
            var result = Services.User.RemoveFromRoleAsync(new RoleUserModel
            {
                Rolename = "FirstRole",
                Username = "behi8303"
            }).Result;
            Assert.IsTrue(result.Succeeded, result.Errors != null ? result.Errors.FirstOrDefault() : string.Empty);
        }

        [TestMethod]
        public void IsInRole()
        {
            var result = Services.User.IsInRoleAsync(new RoleUserModel
            {
                Rolename = "FirstRole",
                Username = "behi8303"
            }).Result;
            Assert.IsNotNull(result);
            Console.WriteLine(result.Value);
        }
    }
}
