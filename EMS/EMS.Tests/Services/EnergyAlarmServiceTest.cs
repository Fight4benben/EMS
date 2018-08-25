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
    public class EnergyAlarmServiceTest
    {
        [TestMethod]
        public void TestEnergyAlarmByUserName()
        {
            EnergyAlarmService service = new EnergyAlarmService();

            EnergyAlarmViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestEnergyAlarmByBuildID_Date()
        {
            EnergyAlarmService service = new EnergyAlarmService();
            DateTime today = DateTime.Now;
            EnergyAlarmViewModel model = service.GetViewModel("000001G001", string.Format("{0:d}", today));

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
