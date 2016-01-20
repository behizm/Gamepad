using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.EventArgs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class UserReviewServiceTest
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
            var userReview = new UserReview
            {
                ArticleId = Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"),
                Description = "bazi kheili kheili khobie.",
                Score = 90,
                UserId = Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3")
            };

            var result = GpServices.UserReview.Insert(userReview);
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}