using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test
{
    [TestClass]
    public class CommomTest
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
        public void GetLenghtOfString()
        {
            var st = "Intel Quad-Core CPU, AMD Six-Core CPU";
            Console.WriteLine(st.Length);
        }

        [TestMethod]
        public void RegexCheck()
        {
            var st = "behnam.zeighami@gmail.com";
            Console.WriteLine(Regex.IsMatch(st, @"^[a-z0-9._%=-]+@[a-z0-9.-]+\.[A-Za-z]{2,4}$"));
        }


    }
}
