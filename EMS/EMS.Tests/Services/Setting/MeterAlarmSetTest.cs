using EMS.DAL.Entities.Setting;
using EMS.DAL.Services;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Services.Setting
{
    [TestClass]
    public class MeterAlarmSetTest
    {
        [TestMethod]
        public void TestGetViewModelBy_userName()
        {
            MeterAlarmSetService service = new MeterAlarmSetService();
            var viewModel = service.GetViewModel("Admin");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_userName_BuildID()
        {
            MeterAlarmSetService service = new MeterAlarmSetService();
            var viewModel = service.GetViewModel("Admin","000001G001");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_userName_BuildID_Code()
        {
            MeterAlarmSetService service = new MeterAlarmSetService();
            var viewModel = service.GetViewModel("Admin", "000001G001","01000");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_userName_BuildID_Code_Circuit()
        {
            MeterAlarmSetService service = new MeterAlarmSetService();
            var viewModel = service.GetViewModel("Admin", "000001G001", "01000", "000001G0010001");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestSetInfo()
        {
            MeterAlarmSetService service = new MeterAlarmSetService();

            MeterAlarmSet setInfo = new MeterAlarmSet();

            setInfo.BuildID = "000001G001";
            setInfo.MeterID = "000001G0010001";
            setInfo.ParamID = "31000000000701";
            setInfo.ParamCode = "Ub";
            setInfo.State = Convert.ToInt32("1");
            setInfo.Level = Convert.ToInt32("2");
            setInfo.Delay = Convert.ToInt32("3");
            setInfo.Lowest = Convert.ToDecimal("1");
            setInfo.Low = Convert.ToDecimal("200");
            setInfo.High = Convert.ToDecimal("230");
            setInfo.Highest = Convert.ToDecimal("240");

            var viewModel = service.SetAlarmInfo(setInfo);

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }


        [TestMethod]
        public void TestDeleteParam()
        {
            MeterAlarmSetService service = new MeterAlarmSetService();

            MeterAlarmSet setInfo = new MeterAlarmSet();

            setInfo.BuildID = "000001G001";
            setInfo.MeterID = "000001G0010001";
            setInfo.ParamID = "31000000000700";
           

            var viewModel = service.DeleteParam(setInfo);

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }


    }
}
