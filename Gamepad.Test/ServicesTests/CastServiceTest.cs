using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Utilities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class CastServiceTest //: BaseServiceTest
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
            var result = GpServices.Cast.Insert(new Cast
            {
                Value = "EA",
                FaValue = "ایی ای",
                CastType = CastType.Publisher
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.Cast.Insert(new Cast
            {
                Value = "Battlefield",
                FaValue = "میدان نبرد",
                CastType = CastType.Brand
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.Cast.Insert(new Cast
            {
                Value = "Konami",
                FaValue = "کونامی",
                CastType = CastType.Publisher
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Find()
        {
            var cast = GpServices.Cast.FindByName("ea");
            Assert.IsNotNull(cast);
            Assert.IsTrue(string.Equals(cast.Value, "Ea", StringComparison.CurrentCultureIgnoreCase));

            cast = GpServices.Cast.FindByName("ایی ای");
            Assert.IsNotNull(cast);
            Assert.IsTrue(string.Equals(cast.Value, "Ea", StringComparison.CurrentCultureIgnoreCase));

            cast = GpServices.Cast.FindById(cast.Id);
            Assert.IsNotNull(cast);
            Assert.IsTrue(string.Equals(cast.Value, "Ea", StringComparison.CurrentCultureIgnoreCase));
        }

        [TestMethod]
        public void Update()
        {
            var cast = GpServices.Cast.FindByName("ea");
            Assert.IsNotNull(cast);
            Assert.IsTrue(string.Equals(cast.Value, "Ea", StringComparison.CurrentCultureIgnoreCase));

            cast.Value = "eA";
            var result = GpServices.Cast.Update(cast);
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Search()
        {
            var result = GpServices.Cast.Search(new CastSearchModel
            {
                CastType = CastType.Developer
            }, new Ordering<Cast, string>
            {
                OrderByKeySelector = x => x.Value
            });
            Assert.IsNotNull(result);
            Console.WriteLine(result.CountAll);
        }

        [TestMethod]
        public void Delete()
        {
            var cast = GpServices.Cast.FindByName("ea");
            Assert.IsNotNull(cast);
            Assert.IsTrue(string.Equals(cast.Value, "Ea", StringComparison.CurrentCultureIgnoreCase));
            var result = GpServices.Cast.Delete(cast);
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}
