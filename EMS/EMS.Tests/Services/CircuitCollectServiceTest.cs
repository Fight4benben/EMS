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
    public class CircuitCollectServiceTest
    {
        [TestMethod]
        public void GetCircuitCollectViewModelByUserName()
        {
            CircuitCollectService service = new CircuitCollectService();
            CircuitCollectViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void GetCircuitCollectViewModelByBuildID_EnergyCode()
        {
            CircuitCollectService service = new CircuitCollectService();
            CircuitCollectViewModel ViewModel = service.GetViewModel("000001G001","01000");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void GetCircuitCollectViewModel()
        {
            CircuitCollectService service = new CircuitCollectService();
            string circuitIDs = "000001G0010001,000001G0010002,000001G0010003,000001G0010004,000001G0010005";

            string[] ids = circuitIDs.Split(',');
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.Date;
            CircuitCollectViewModel ViewModel = service.GetViewModel("000001G001", "01000", ids, startTime.ToString(), endTime.ToString());

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void GetMultiRateCollectViewModel()
        {
            CircuitCollectService service = new CircuitCollectService();
            string circuitIDs = "000001G0010001,000001G0010002,000001G0010003,000001G0010004,000001G0010005";

            string[] ids = circuitIDs.Split(',');
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.Date;
            CircuitCollectViewModel ViewModel = service.GetMultiRateViewModel("000001G001", "01000", ids, startTime.ToString("yyyy-MM-dd 00:00:00"), endTime.ToString("yyyy-MM-dd HH:00:00"));

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
