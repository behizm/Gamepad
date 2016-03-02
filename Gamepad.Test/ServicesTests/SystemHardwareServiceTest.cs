using System;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamepad.Test.ServicesTests
{
    [TestClass]
    public class SystemHardwareServiceTest
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
        public void Add()
        {
            var result = GpServices.SystemHardware.Insert(new SystemHardware
            {
                HardwareType = SystemHardwareType.Vga,
                Name = "GeForce Gtx 650 Ti"
            });
            Assert.IsTrue(result.Succeeded, result.LastError);
            result = GpServices.SaveChanges();
            Assert.IsTrue(result.Succeeded, result.LastError);
        }

        [TestMethod]
        public void Search_Find()
        {
            var result = GpServices.SystemHardware.Search(new SystemHardwareSearchModel
            {
                HardwareType = SystemHardwareType.Vga,
                Name = "980"
            }, new Ordering<SystemHardware, SystemHardwareType>
            {
                OrderByKeySelector = x => x.HardwareType
            });
            Assert.IsNotNull(result);
            Console.WriteLine(result.CountAll);

            var hardware = GpServices.SystemHardware.FindById(result.List.First().Id);
            Assert.IsNotNull(hardware);
            Console.WriteLine(hardware.HardwareType + " : " + hardware.Name);
        }

        [TestMethod]
        public void Edit()
        {
            var result = GpServices.SystemHardware.Search(new SystemHardwareSearchModel
            {
                HardwareType = SystemHardwareType.Vga,
                Name = "980"
            }, new Ordering<SystemHardware, SystemHardwareType>
            {
                OrderByKeySelector = x => x.HardwareType
            });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CountAll > 0);
            var hardware = GpServices.SystemHardware.FindById(result.List.First().Id);
            hardware.Name = "GeForce Gtx 980 Ti";
            var editResult = GpServices.SystemHardware.Update(hardware);
            Assert.IsTrue(editResult.Succeeded, editResult.LastError);
            editResult = GpServices.SaveChanges();
            Assert.IsTrue(editResult.Succeeded, editResult.LastError);
        }

        [TestMethod]
        public void Remove()
        {
            var result = GpServices.SystemHardware.Search(new SystemHardwareSearchModel
            {
                HardwareType = SystemHardwareType.Vga,
                Name = "980"
            }, new Ordering<SystemHardware, SystemHardwareType>
            {
                OrderByKeySelector = x => x.HardwareType
            });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CountAll > 0);
            var hardware = GpServices.SystemHardware.FindById(result.List.First().Id);
            var removeResult = GpServices.SystemHardware.Delete(hardware.Id);
            Assert.IsTrue(removeResult.Succeeded, removeResult.LastError);
            removeResult = GpServices.SaveChanges();
            Assert.IsTrue(removeResult.Succeeded, removeResult.LastError);
        }
    }
}
