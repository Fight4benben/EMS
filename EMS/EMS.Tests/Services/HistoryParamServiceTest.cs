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
    public class HistoryParamServiceTest
    {
        [TestMethod]
        public void TestGetParamValueViewModelByUserName()
        {
            HistoryParamService service = new HistoryParamService();

            HistoryParamViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetParamValueViewModelByBuildID()
        {
            HistoryParamService service = new HistoryParamService();

            HistoryParamViewModel model = service.GetViewModel("000001G001");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetParamValueViewModelByBuildID_EnergyCode()
        {
            HistoryParamService service = new HistoryParamService();

            HistoryParamViewModel model = service.GetViewModel("000001G001","02000");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetParamValueViewModelByBuildID_EnergyCode_CircuitID()
        {
            HistoryParamService service = new HistoryParamService();

            string[] ids = new string[] { "000001G0010001", "000001G0010002" };

            HistoryParamViewModel model = service.GetViewModel("000001G001", "01000", ids);

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetParamValueViewModelByCircuitID_ParamID_StartTime_step()
        {
            HistoryParamService service = new HistoryParamService();

            string[] circuitIDs = new string[] { "000001G0010001", "000001G0010002" };
            string[] paramIDs = new string[] { "31000000000700", "31000000000701", "31000000000702", "31000000000709", "31000000000711" };
            DateTime today = DateTime.Now;

            HistoryParamViewModel model = service.GetViewModel(circuitIDs, paramIDs, today,60);

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
