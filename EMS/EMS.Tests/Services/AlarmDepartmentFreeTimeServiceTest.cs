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
    public class AlarmDepartmentFreeTimeServiceTest
    {
        [TestMethod]
        public void TestAlarmDepartmentFreeTimeByUserName()
        {
            AlarmDepartmentFreeTimeService service = new AlarmDepartmentFreeTimeService();

            AlarmDepartmentFreeTimeViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestAlarmDepartmentFreeTimeByBuildID_Date()
        {
            AlarmDepartmentFreeTimeService service = new AlarmDepartmentFreeTimeService();
            DateTime today = DateTime.Now.AddDays(-1);
            AlarmDepartmentFreeTimeViewModel model = service.GetViewModel("000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
