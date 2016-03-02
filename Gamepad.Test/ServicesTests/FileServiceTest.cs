using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class FileServiceTest
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
        public void AddFile()
        {
            var result = GpServices.File.Insert(new File
            {
                Size = 50,
                Title = "vid5",
                Category = FileCategory.ArticleVideo,
                FileType = FileType.Video,
                IsPublic = false,
                Filename = "file5.jpeg",
                Address = "address5"
            });
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }
    }
}
