﻿using EMS.DAL.Services;
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
    public class AlarmDeviceServiceTest
    {
        [TestMethod]
        public void TestGetDeviceAlarmViewModelByUser()
        {
            DateTime today = DateTime.Now;
            AlarmDeviceService service = new AlarmDeviceService();
            AlarmDeviceViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));

        }

        [TestMethod]
        public void TestGetDeviceAlarmViewModelByDay()
        {
            DateTime today = DateTime.Now;
            AlarmDeviceService service = new AlarmDeviceService();
            AlarmDeviceViewModel ViewModel = service.GetViewModel("000001G001", "01000", "DD", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetDeviceAlarmViewModelByMonth()
        {
            DateTime today = DateTime.Now;
            AlarmDeviceService service = new AlarmDeviceService();
            AlarmDeviceViewModel ViewModel = service.GetViewModel("000001G001", "01000", "MM", today.ToString("yyyy-MM"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetDeviceAlarmViewModelByQuarter()
        {
            DateTime today = DateTime.Now;
            AlarmDeviceService service = new AlarmDeviceService();
            AlarmDeviceViewModel ViewModel = service.GetViewModel("000001G001", "01000", "SS", today.ToString("yyyy-MM"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestSetAlarmDeviceLevelUpdata()
        {
            DateTime today = DateTime.Now;
            AlarmDeviceService service = new AlarmDeviceService();
            decimal l1 = 0.3m;
            decimal l2 = 0.7m;
            int result = service.SetDeviceBuildAlarmLevel("000001G002", "01000", l1, l2);

            Console.WriteLine(UtilTest.GetJson(result));
        }

    }
}
