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
    }
}
