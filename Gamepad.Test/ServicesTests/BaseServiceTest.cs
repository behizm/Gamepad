using System;
using System.Linq;
using Gamepad.Service.Liberary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class BaseServiceTest
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
        public void SwearWordFilter()
        {
            var swears = AppConfigs.SwearWords;
            var text = "you son of  the    bitch, fuck you for that shit.";
            Console.WriteLine(text);
            var swearsList = swears.Split(':').Select(x => x.Trim()).ToList();
            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }
            foreach (var swear in swearsList)
            {
                if (text.Contains(swear))
                {
                    text = text.Replace(swear, "***");
                }
            }
            Console.WriteLine(text);
        }
    }
}
