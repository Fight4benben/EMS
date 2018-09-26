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
    public class AlarmDeviceFreeTimeServiceTest
    {
        [TestMethod]
        public void TestGetAlarmDeviceFreeTimeViewModelByUser()
        {
            AlarmDeviceFreeTimeService service = new AlarmDeviceFreeTimeService();
            AlarmDeviceFreeTimeViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetAlarmDeviceFreeTimeViewModelByBuidID()
        {
            DateTime today = DateTime.Now;
            AlarmDeviceFreeTimeService service = new AlarmDeviceFreeTimeService();
            AlarmDeviceFreeTimeViewModel ViewModel = service.GetViewModel("000001G001", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
