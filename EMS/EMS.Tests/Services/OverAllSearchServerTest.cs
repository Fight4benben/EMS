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
        public void TestOverAllSearchGetCircuit()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("Circuit", "进线","000001G003", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetDetp()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("Dept", "研发", "000001G003", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestOverAllSearchGetRegion()
        {
            OverAllSearchService service = new OverAllSearchService();
            DateTime today = DateTime.Now;

            OverAllSearchViewModel model = service.GetViewModel("Region", "老厂区", "000001G001", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
