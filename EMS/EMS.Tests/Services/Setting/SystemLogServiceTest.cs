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
    public class SystemLogServiceTest
    {
        [TestMethod]
        public void TestGetLogByDatetime()
        {
            SystemLogService service = new SystemLogService();
            string startDay = DateTime.Now.ToString("yyyy-MM-dd");
            string endDay = DateTime.Now.ToString("yyyy-MM-dd");

            SystemLogViewModel ViewModel = service.GetViewModel(startDay,endDay);

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByDay()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("DD");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByWeek()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("WW");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByMonth()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("MM");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByYear()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("YY");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByLastDay()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("LDD");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByLastWeek()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("LWW");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByLastMonth()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("LMM");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetLogByLastYear()
        {
            SystemLogService service = new SystemLogService();
            SystemLogViewModel ViewModel = service.GetViewModel("LYY");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
