using EMS.DAL.Services;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EMS.Tests.Services
{
    [TestClass]
    public class NoWorkDayServiceTest
    {

        [TestMethod]
        public void TestGetNoWorkDayViewModelBy_userName()
        {
            NoWorkDayService service = new NoWorkDayService();
            var viewModel = service.GetViewModel("Admin");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetNoWorkDayViewModelBy_userName_Build()
        {
            NoWorkDayService service = new NoWorkDayService();
            var viewModel = service.GetViewModel("Admin","000001G001");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetNoWorkDayViewModelBy_userName_Build_Code()
        {
            NoWorkDayService service = new NoWorkDayService();
            var viewModel = service.GetViewModel("Admin", "000001G001","02000");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetNoWorkDayViewModelBy_userName_Build_Code_Date()
        {
            NoWorkDayService service = new NoWorkDayService();
            DateTime today = DateTime.Now;
            var viewModel = service.GetViewModel("Admin", "000001G001", "02000", today.ToString("yyyy-MM-01"), today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetNoWorkDayViewModelBy_userName_Build_Code_Date_Ids()
        {
            NoWorkDayService service = new NoWorkDayService();
            DateTime today = DateTime.Now;
            string ids = "000001G0010001,000001G0010002";
            var viewModel = service.GetViewModel("Admin", "000001G001", "01000", ids, today.ToString("yyyy-MM-01"), today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }
    }
}
