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
    public class AlarmDepartmentOverLimitServiceTest
    {
        [TestMethod]
        public void TestAlarmDepartmentOverLimitByUserName()
        {
            AlarmDepartmentOverLimitService service = new AlarmDepartmentOverLimitService();

            AlarmDepartmentOverLimitViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestAlarmDepartmentOverLimitByBuildID_Date()
        {
            AlarmDepartmentOverLimitService service = new AlarmDepartmentOverLimitService();
            DateTime today = DateTime.Now.AddDays(-1);
            AlarmDepartmentOverLimitViewModel model = service.GetViewModel("000001G001","01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestSetAlarmDepartmentOverLimitUpdate()
        {
            AlarmDepartmentOverLimitService service = new AlarmDepartmentOverLimitService();
            DateTime today = DateTime.Now.AddDays(-1);
            int model = service.SetDeptOverLimitValue("000001G003", "01000", "000001G0030002","18:00","09:00",1,100);

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestSetAlarmDepartmentOverLimitInsert()
        {
            AlarmDepartmentOverLimitService service = new AlarmDepartmentOverLimitService();
            DateTime today = DateTime.Now.AddDays(-1);
            int model = service.SetDeptOverLimitValue("000001G003", "01000", "000001G0030003", "18:00", "09:00", 1, 100);

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestDeleteAlarmDepartmentOverLimit()
        {
            AlarmDepartmentOverLimitService service = new AlarmDepartmentOverLimitService();
            DateTime today = DateTime.Now.AddDays(-1);
            int model = service.DeleteDeptOverLimitValue("000001G003", "01000", "000001G0030003");

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
