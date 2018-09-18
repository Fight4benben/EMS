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
    public class MeterConnectStateServiceTest
    {
        [TestMethod]
        public void TestGetMeterStateByUser()
        {
            MeterConnectStateService service = new MeterConnectStateService();
            MeterConnectStateViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetMeterStateByType()
        {
            MeterConnectStateService service = new MeterConnectStateService();
            MeterConnectStateViewModel ViewModel = service.GetViewModel("000001G001","01000","1");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
