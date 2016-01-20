using System;
using System.Collections.Generic;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Utilities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class ArticleServiceTest
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
            var result = GpServices.Article.Insert(new Article
            {
                EditDate = DateTime.Now,
                Platforms = new List<ArticlePlatform>
                {
                    new ArticlePlatform(GamePlatform.Windows),
                    new ArticlePlatform(GamePlatform.XboxOn),
                    new ArticlePlatform(GamePlatform.Ps4)
                },
                ReleaseDate = DateTime.Now.AddYears(-2),
                SiteScore = 9,
                Title = " Metal Gear   Solid V   ",
                Type = ArticleType.Game,
                MoreInfo = new ArticleInfo
                {
                    Description = "solid snake",
                    FinishTimeAverage = 120,
                }
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Update()
        {
            var article = GpServices.Article.FindByName("metal_gear_solid_v");
            Assert.IsNotNull(article);
            var article2 = GpServices.Article.FindById(article.Id);
            Assert.IsNotNull(article2);
            article.MoreInfo.FinishTimeMain = 70;
            article.MoreInfo.FinishTimeAverage = 115;
            article.ReleaseDate = DateTime.Now.AddMonths(-6);
            var result = GpServices.Article.Update(article);
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Search()
        {
            var articles = GpServices.Article.Search(new ArticleSearchModel
            {
                Platform = GamePlatform.Windows
            }, new Ordering<Article, string>
            {
                OrderByKeySelector = x => x.Title
            });
            Assert.IsNotNull(articles);
            Console.WriteLine(articles.CountAll);
            foreach (var article in articles.List)
            {
                Console.WriteLine(article.Title);
            }
        }

        [TestMethod]
        public void AddPlatform()
        {
            var result = GpServices.Article.AddPlatform(
                Guid.Parse("42cfd283-0bae-4bee-9f65-21f2aa9af207"),
                new List<GamePlatform>
                {
                    GamePlatform.Ps4,
                    GamePlatform.Windows,
                    GamePlatform.XboxOn
                });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void RemovePlatform()
        {
            var result = GpServices.Article.RemovePlatform(
                Guid.Parse("42cfd283-0bae-4bee-9f65-21f2aa9af207"),
                new List<GamePlatform>
                {
                    GamePlatform.XboxOn,
                    GamePlatform.Ps4
                });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangePoster()
        {
            var addr = GpServices.Article.ChangePoster(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"), null);
            Assert.IsNotNull(addr);
            Console.WriteLine(addr);

            var result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void AddGenre()
        {
            var result = GpServices.Article.AddToGenre(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"), Guid.Parse("c7c3bd24-0c46-4641-a888-e375f90b5a86"));
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void RemoveGenre()
        {
            var result = GpServices.Article.RemoveFromGenre(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"), Guid.Parse("c7c3bd24-0c46-4641-a888-e375f90b5a86"));
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void AddCast()
        {
            var result = GpServices.Article.AddToCast(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"), Guid.Parse("e2ccf1d1-2ad8-4739-ae56-771962a3b09d"));
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void RemoveCast()
        {
            var result = GpServices.Article.RemoveFromCast(Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"), Guid.Parse("e2ccf1d1-2ad8-4739-ae56-771962a3b09d"));
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void AddImageGallery()
        {
            var result = GpServices.Article.AddToImageGallery(
                Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"),
                new List<FileBaseInfoModel>
                {
                    new FileBaseInfoModel { Filename = "mgsv_001", Address = "metalimage001.jpg", Size = 57},
                    new FileBaseInfoModel { Filename = "mgsv_002", Address = "metalimage002.jpg", Size = 57},
                    new FileBaseInfoModel { Filename = "mgsv_003", Address = "metalimage003.jpg", Size = 57},
                });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void RemoveImageGallery()
        {
            var result = GpServices.Article.RemoveFromImageGallery(
                Guid.Parse("2e47e8c6-bf3c-46c8-b348-11dabd1f854e"),
                new List<Guid>
                {
                    Guid.Parse("4c848f2d-2894-4df3-a833-69185b38f0cc"),
                    Guid.Parse("6d77d548-e789-4128-9b31-b17d083543a7"),
                });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}