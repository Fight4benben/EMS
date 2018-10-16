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
    public class OverAllSearchServerTest
    {
        [TestMethod]
        public void TestOverAllSearchGetCircuit_Day()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("DD","Circuit", "办公", "000001G001","01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetCircuit_Month()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("MM", "Circuit", "层", "000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetCircuit_Quarter()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("QQ", "Circuit", "电梯", "000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetDetp_Day()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("DD", "Dept", "2F", "000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetDetp_Month()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("MM", "Dept", "2F", "000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetDetp_Quarter()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("QQ", "Dept", "2F", "000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetRegion_Day()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("DD", "Region", "办公", "000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetRegion_Month()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("MM", "Region", "食堂", "000001G001", "01000", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetRegion_Quarter()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("QQ", "Region", "宿舍", "000001G001", "01000", today.ToString("yyyy-09"));

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
