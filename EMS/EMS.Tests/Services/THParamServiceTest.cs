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
    public class THParamServiceTest
    {
        [TestMethod]
        public void TestGetTHParamValueViewModelByUserName()
        {
            THParamService service = new THParamService();

            HistoryParamViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetTHParamValueViewModelByBuildID()
        {
            THParamService service = new THParamService();

            HistoryParamViewModel model = service.GetViewModel("000001G001");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetTHParamValueViewModelByBuildID_CircuitID()
        {
            THParamService service = new THParamService();

            string circuitID = "000001G0010082";

            HistoryParamViewModel model = service.GetViewModel("000001G001", circuitID);

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetTHParamValueViewModelByBuildID_CircuitID_StartTime()
        {
            THParamService service = new THParamService();

            string circuitID = "000001G0010083";

            HistoryParamViewModel model = service.GetViewModel("000001G001", circuitID,DateTime.Now.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetTHParamValueViewModelByBuildID_CircuitID_StartTime_Step()
        {
            THParamService service = new THParamService();

            string circuitID = "000001G0010082";

            HistoryParamViewModel model = service.GetViewModel("000001G001", circuitID, DateTime.Now.ToString("yyyy-MM-dd"),10);

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
