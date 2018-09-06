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
    public class AlarmDepartmentComletionRateServiceTest
    {
        [TestMethod]
        public void TestAlarmDepartmentComletionRateByUserName()
        {
            AlarmDepartmentComletionRateService service = new AlarmDepartmentComletionRateService();

            AlarmDepartmentComletionRateViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetDepartmentComletionRateViewModelByMonth()
        {
            DateTime today = DateTime.Now;
            AlarmDepartmentComletionRateService service = new AlarmDepartmentComletionRateService();

            AlarmDepartmentComletionRateViewModel ViewModel = service.GetViewModel("000001G003", "01000", "MM", "2018-08");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestDepartmentComletionRateViewModelByQuarter()
        {
            DateTime today = DateTime.Now;
            AlarmDepartmentComletionRateService service = new AlarmDepartmentComletionRateService();
            AlarmDepartmentComletionRateViewModel ViewModel = service.GetViewModel("000001G003", "01000", "QQ", "2018-09");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestDepartmentComletionRateViewModelByYear()
        {
            DateTime today = DateTime.Now;
            AlarmDepartmentComletionRateService service = new AlarmDepartmentComletionRateService();
            AlarmDepartmentComletionRateViewModel ViewModel = service.GetViewModel("000001G003", "01000", "YY", "2018-8");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
