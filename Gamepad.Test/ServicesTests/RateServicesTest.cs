using System;
using System.Collections.Generic;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class RateServicesTest
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
            var articleId = Guid.Parse("42cfd283-0bae-4bee-9f65-21f2aa9af207");
            var result = GpServices.Rate.Insert(new Rate
            {
                ArticleId = articleId,
                RateSource = RateSource.Esrb,
                Url = "link",
                Value = RatingCategory.EsrbM,
                RateContents = new List<RateContent>
                {
                    new RateContent(RateSource.Esrb,RateContentValue.EsrbBloodAndGore),
                    new RateContent(RateSource.Esrb, RateContentValue.EsrbMatureHumor)
                }
            });
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Update()
        {
            var rateId = Guid.Parse("69436e1f-e67e-4338-b4b8-376e6eabd76b");
            var rate = GpServices.Rate.FindById(rateId);
            Assert.IsNotNull(rate);

            rate.RateSource = RateSource.Pegi;
            rate.Value = RatingCategory.Pegi16;
            var rateContents = new List<RateContent>
            {
                new RateContent(RateSource.Pegi, RateContentValue.EsrbAlcoholReference),
                new RateContent(RateSource.Pegi, RateContentValue.EsrbAnimatedBlood),
            };
            var result = GpServices.Rate.Update(rate, rateContents);
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Delete()
        {
            var rateId = Guid.Parse("69436e1f-e67e-4338-b4b8-376e6eabd76b");
            var result = GpServices.Rate.Delete(rateId);
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}