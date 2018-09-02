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
    public class AlarmDepartmentServiceTest
    {
        [TestMethod]
        public void TestGetAlarmDepartmentViewModelByUser()
        {
            DateTime today = DateTime.Now;
            AlarmDepartmentService service = new AlarmDepartmentService();
            AlarmDepartmentViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetAlarmDepartmentViewModelByDay()
        {
            DateTime today = DateTime.Now;
            AlarmDepartmentService service = new AlarmDepartmentService();
            AlarmDepartmentViewModel ViewModel = service.GetViewModel("000001G001", "01000", "DD", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetAlarmDepartmentViewModelByMonth()
        {
            DateTime today = DateTime.Now;
            AlarmDepartmentService service = new AlarmDepartmentService();
            AlarmDepartmentViewModel ViewModel = service.GetViewModel("000001G001", "01000", "MM", today.ToString("yyyy-MM"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetAlarmDepartmentViewModelByQuarter()
        {
            DateTime today = DateTime.Now;
            AlarmDepartmentService service = new AlarmDepartmentService();
            AlarmDepartmentViewModel ViewModel = service.GetViewModel("000001G001", "01000", "SS", today.ToString("yyyy-MM"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
