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
    public class ToxicGasesServiceTest
    {

        [TestMethod]
        public void TestGetViewModel_userName()
        {
            ToxicGasesService service = new ToxicGasesService();
            var viewModel = service.GetViewModel("Admin");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetHistoryViewModel_userName()
        {
            ToxicGasesService service = new ToxicGasesService();
            DateTime today =  DateTime.Now;
            var viewModel = service.GetHistoryDataViewModel("Admin","000001G001", "000001G0010001", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }
    }
}
