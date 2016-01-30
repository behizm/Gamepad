using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class ExternalRankServiceTest
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
            var result = GpServices.ExternalRank.Insert(new ExternalRank
            {
                ArticleId = Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"),
                Score = 90,
                Type = ExternalRankType.Metacritic,
                Url = "site url"
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);

            ShowArticleRanks(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"));
        }

        [TestMethod]
        public void Update()
        {
            var externalRank = GpServices.ExternalRank.FindById(Guid.Parse("f3899058-2d87-4966-a6b7-d822af8c4139"));
            Assert.IsNotNull(externalRank);
            externalRank.Score = 95;

            var result = GpServices.ExternalRank.Update(externalRank);
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);

            ShowArticleRanks(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"));
        }

        [TestMethod]
        public void Delete()
        {
            var result = GpServices.ExternalRank.Delete(Guid.Parse("f3899058-2d87-4966-a6b7-d822af8c4139"));
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);

            ShowArticleRanks(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"));
        }


        private static void ShowArticleRanks(Guid id)
        {
            var article = GpServices.Article.FindById(id);
            Assert.IsNotNull(article);
            Console.WriteLine(@"game : {0}", article.Title);
            foreach (var externalRate in article.ExternalRates)
            {
                Console.WriteLine(@"{0} [{1}]", externalRate.Type, externalRate.Score);
            }
        }
    }
}