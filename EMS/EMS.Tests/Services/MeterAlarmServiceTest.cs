using EMS.DAL.Services;
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
    public class MeterAlarmServiceTest
    {
        [TestMethod]
        public void TestGetViewModelBy_userName()
        {
            MeterAlarmService service = new MeterAlarmService();
            var viewModel = service.GetAlarmingViewModel("Admin");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_userName_Pages()
        {
            MeterAlarmService service = new MeterAlarmService();
            var viewModel = service.GetAlarmingViewModel("Admin",3,50);

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestSetConfirm_MeterAlarm()
        {
            MeterAlarmService service = new MeterAlarmService();
            string ids = "1979";

            var viewModel = service.SetConfirmMeterAlarm("Admin", "test by admin", ids);

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestSetConfirm_AllMeterAlarm()
        {
            MeterAlarmService service = new MeterAlarmService();

            var viewModel = service.SetConfirmAllMeterAlarm("Admin", "test by admin all");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGet_AlarmLog_User()
        {
            MeterAlarmService service = new MeterAlarmService();

            var viewModel = service.GetAlarmLogViewModel("Admin");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGet_AlarmLog_ByUser_Date()
        {
            MeterAlarmService service = new MeterAlarmService();

            var viewModel = service.GetAlarmLogViewModel("Admin","2019-06-22","2019-06-23",1,50);

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

    }
}
