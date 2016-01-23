using System;
using System.Linq;
using Gamepad.Service.Liberary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class ConfigServiceTest
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
            var result = GpServices.Config.Insert("Sitename", "Gamepad");
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Update()
        {
            var result = GpServices.Config.Update("sitename", "Gamepad");
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void FindByKey()
        {
            var config = GpServices.Config.FindByKey("Sitename");
            Assert.IsNotNull(config);
            Console.WriteLine(@"value : " + config.Value);

            var value = GpServices.Config.GetValue("sitename");
            Assert.IsNotNull(value);
            Console.WriteLine(@"value : " + value);

            var dic = GpServices.Config.GetConfigsDictionary();
            Assert.IsNotNull(dic);
            Console.WriteLine(@"value : " + dic["Sitename"]);
            Console.WriteLine();
            foreach (var keyValuePair in dic)
            {
                Console.WriteLine(keyValuePair.Key + @" : " + keyValuePair.Value);
            }
        }

        [TestMethod]
        public void Delete()
        {
            var result = GpServices.Config.Delete("Developer");
            Assert.IsTrue(result.Succeeded, result.LastError);

            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void GetAppConfig()
        {
            Console.WriteLine(@"Sitename : " + AppConfigs.Sitename);
            Console.WriteLine(@"Sitename : " + AppConfigs.Sitename);
            Console.WriteLine(@"Sitename : " + AppConfigs.Sitename);
            Console.WriteLine(@"Sitename : " + AppConfigs.Sitename);
        }
    }
}