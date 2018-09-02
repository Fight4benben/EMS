using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Services
{
    [TestClass]
    public class DeviceAlarmServiceTest
    {
        [TestMethod]
        public void TestGetDeviceAlarmViewModelByUser()
        {
            DateTime today = DateTime.Now;
            DeviceAlarmService service = new DeviceAlarmService();
            DeviceAlarmViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));

        }

        [TestMethod]
        public void TestGetDeviceAlarmViewModelByDay()
        {
            DateTime today = DateTime.Now;
            DeviceAlarmService service = new DeviceAlarmService();
            DeviceAlarmViewModel ViewModel = service.GetViewModel("000001G001", "01000", "DD", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetDeviceAlarmViewModelByMonth()
        {
            DateTime today = DateTime.Now;
            DeviceAlarmService service = new DeviceAlarmService();
            DeviceAlarmViewModel ViewModel = service.GetViewModel("000001G001", "01000", "MM", today.ToString("yyyy-MM"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetDeviceAlarmViewModelByQuarter()
        {
            DateTime today = DateTime.Now;
            DeviceAlarmService service = new DeviceAlarmService();
            DeviceAlarmViewModel ViewModel = service.GetViewModel("000001G001", "01000", "SS", today.ToString("yyyy-MM"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

    }
}
