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
            DateTime today = DateTime.Now.AddDays(-8);
            EnergyAlarmViewModel model = service.GetViewModel("000001G001", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestMomDayByBuildID_Date()
        {
            EnergyAlarmService service = new EnergyAlarmService();
            
            EnergyAlarmViewModel model = service.GetMomDayViewModel("000001G001", "2018-08-20");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestMomMonthByBuildID_Date()
        {
            EnergyAlarmService service = new EnergyAlarmService();

            //EnergyAlarmViewModel model = service.GetCompareMonthViewModel("000001G001", "2018-08-20");
            EnergyAlarmViewModel model = service.GetMomMonthViewModel("000001G001", "2018-08");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestCompareMonthByBuildID_Date()
        {
            EnergyAlarmService service = new EnergyAlarmService();

            EnergyAlarmViewModel model = service.GetCompareMonthViewModel("000001G001", "2018-08-30");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestMomDeptByBuildID_Date()
        {
            EnergyAlarmService service = new EnergyAlarmService();
            EnergyAlarmViewModel model = service.GetMomDeptMonthViewModel("000001G001", "2018-08-30");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestCompareDeptByBuildID_Date()
        {
            EnergyAlarmService service = new EnergyAlarmService();

            //EnergyAlarmViewModel model = service.GetCompareMonthViewModel("000001G001", "2018-08-20");
            EnergyAlarmViewModel model = service.GetCompareDeptMonthViewModel("000001G001", "2018-08");

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
