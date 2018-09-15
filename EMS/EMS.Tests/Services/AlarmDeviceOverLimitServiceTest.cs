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
    public class AlarmDeviceOverLimitServiceTest
    {

        [TestMethod]
        public void TestGetSettingDeviceAlarmViewModelByUser()
        {
            DateTime today = DateTime.Now;
            AlarmDeviceOverLimitService service = new AlarmDeviceOverLimitService();
            AlarmDeviceOverLimitViewModel ViewModel = service.GetSettingViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetSettingDeviceAlarmViewModelByBuildID()
        {
            AlarmDeviceOverLimitService service = new AlarmDeviceOverLimitService();
            AlarmDeviceOverLimitViewModel ViewModel = service.GetSettingAlarmLimitValueViewModel("000001G001");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
