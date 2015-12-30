using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class GenreServiceTest
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
        public void Overall()
        {
            Console.WriteLine(@"add test start ...");
            var result = GpServices.Genre.Insert(new Genre
            {
                Name = "MyDrama",
                FaName = "درام من",
                Description = "my movie genre"
            });
            Assert.IsTrue(result.Succeeded, result.LastError);
            Console.WriteLine(@"add test success.");

            Console.WriteLine(@"find by name test start ...");
            var genre = GpServices.Genre.FindByName("mydrama");
            Assert.IsNotNull(genre);
            Assert.IsTrue(string.Equals(genre.Name, "MyDrama", StringComparison.CurrentCultureIgnoreCase));
            Console.WriteLine(@"find test success.");

            Console.WriteLine(@"find by farsi name test start ...");
            genre = GpServices.Genre.FindByName("درام من" );
            Assert.IsNotNull(genre);
            Assert.IsTrue(string.Equals(genre.Name, "MyDrama", StringComparison.CurrentCultureIgnoreCase));
            Console.WriteLine(@"find test success.");

            Console.WriteLine(@"find by id test start ...");
            genre = GpServices.Genre.FindById(genre.Id);
            Assert.IsNotNull(genre);
            Assert.IsTrue(string.Equals(genre.Name, "MyDrama", StringComparison.CurrentCultureIgnoreCase));
            Console.WriteLine(@"find test success.");

            Console.WriteLine(@"update test start ...");
            genre.FaName = "درام خودم";
            result = GpServices.Genre.Update(genre);
            Assert.IsTrue(result.Succeeded, result.LastError);
            Console.WriteLine(@"update test success.");

            Console.WriteLine(@"search test start ...");
            var genres =
                GpServices.Genre.Search(
                    new GenreSearchModel { FaName = "درام خودم" },
                    new Ordering<Genre, string> { OrderByKeySelector = x => x.Name });
            Assert.IsNotNull(genres);
            Assert.IsTrue(genres.CountAll == 1);
            Console.WriteLine(@"search test success.");

            Console.WriteLine(@"remove test start ...");
            result = GpServices.Genre.Delete(genre.Id);
            Assert.IsTrue(result.Succeeded, result.Errors.Any() ? result.Errors.Last() : string.Empty);
            Console.WriteLine(@"remove test success.");
        }
    }
}
