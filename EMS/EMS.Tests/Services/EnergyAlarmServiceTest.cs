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
            AlarmDeviceOverLimitService service = new AlarmDeviceOverLimitService();

            AlarmDeviceOverLimitViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestEnergyAlarmByBuildID_Date()
        {
            AlarmDeviceOverLimitService service = new AlarmDeviceOverLimitService();
            DateTime today = DateTime.Now.AddDays(-8);
            AlarmDeviceOverLimitViewModel model = service.GetViewModel("000001G001", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        
    }
}
