using System;
using System.Collections.Generic;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class PostServicesTest
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
            var result = GpServices.Post.Store(new PostUpdateModel
            {
                Title = "New update for battlefield 4.",
                Articles = new List<Guid>
                {
                    Guid.Parse("42cfd283-0bae-4bee-9f65-21f2aa9af207")
                },
                AuthorId = Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3"),
                Description = "game news",
                PostTags = new List<string> { "online", "game", "pc" },
                PostType = PostType.News,
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Update()
        {
            var result = GpServices.Post.Store(new PostUpdateModel
            {
                Id = Guid.Parse("3efab879-39c1-4db5-891f-03f6d1d400ab"),
                Title = "New update for battlefield 4.",
                Articles = new List<Guid>
                {
                    Guid.Parse("42cfd283-0bae-4bee-9f65-21f2aa9af207")
                },
                AuthorId = Guid.Parse("7d2199a9-09a3-470e-b877-d755d69e2fb3"),
                Description = "game news",
                PostTags = new List<string> { "online", "game", "fps" },
                PostType = PostType.News,
                Content = "news body",
                Images = new List<Guid>
                {
                    Guid.Parse("0977b1b9-8c51-4efd-a142-41855c3ae95a"),
                    Guid.Parse("2ffd8d1c-eb52-47cf-a6ff-f0a4a6735a69"),
                    Guid.Parse("4f1115f8-fabb-48b7-a4a3-7082b0da3f82"),
                },
                PublishDate = DateTime.Now.AddDays(1),
                MainImageId = Guid.Parse("0977b1b9-8c51-4efd-a142-41855c3ae95a"),
                VideoId = Guid.Parse("0977b1b9-8c51-4efd-a142-41855c3ae95a"),
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void ChangeHide()
        {
            var result = GpServices.Post.ChangeHide(
                Guid.Parse("3efab879-39c1-4db5-891f-03f6d1d400ab"),
                true);
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Find()
        {
            var post = GpServices.Post.FindById(Guid.Parse("3efab879-39c1-4db5-891f-03f6d1d400ab"));
            Assert.IsNotNull(post);
            Console.WriteLine(post.Title);
            Console.WriteLine("Article : {0}", post.Articles.FirstOrDefault().Title);
            Console.WriteLine("Tags :");
            foreach (var tag in post.Tags)
            {
                Console.WriteLine(tag.Name);
            }
        }

        [TestMethod]
        public void PostReviews()
        {
            var result = GpServices.Post.SyncPostReviews(Guid.Parse("3efab879-39c1-4db5-891f-03f6d1d400ab"),
                new List<PostReview>
                {
                    new PostReview
                    {
                        Title = "Graphic",
                        Max = 100,
                        Score = 85,
                    },
                    new PostReview
                    {
                        Title = "Gameplay",
                        Max = 100,
                        Score = 90,
                    },
                    new PostReview
                    {
                        Title = "Sound",
                        Max = 100,
                        Score = 85,
                    },
                    new PostReview
                    {
                        Title = "Story",
                        Max = 100,
                        Score = 65,
                    },
                    new PostReview
                    {
                        Title = "Overall",
                        Max = 100,
                        Score = 80,
                    },
                });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}