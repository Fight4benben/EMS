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
            var viewModel = service.GetViewModel("Admin");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

    }
}
