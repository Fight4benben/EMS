using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EMS.Tests.Services
{
    [TestClass]
    public class CircuitCompareServiceTest
    {
        [TestMethod]
        public void TestGetCircuitCompareViewModel()
        {
            CircuitCompareService service = new CircuitCompareService();
            CircuitCompareViewModel circuitCompaerView = service.GetCircuitCompareViewModel("admin");
            CircuitCompareViewModel circuitCompaerView2 = service.GetCircuitCompareViewModel("000001G001", "2018-02-07 14:00:00");
            CircuitCompareViewModel circuitCompaerView3 = service.GetCircuitCompareViewModel("000001G001", "01000", "2018-02-07 14:00:00");
            CircuitCompareViewModel circuitCompaerView4 = service.GetCircuitCompareViewModel("000001G001", "01000", "000001G0010001","2018-02-07 14:00:00");

            //Console.WriteLine(GetJson(circuitCompaerView));
            Console.WriteLine(GetJson(circuitCompaerView2));
            //Console.WriteLine(GetJson(circuitCompaerView3));
            //Console.WriteLine(GetJson(circuitCompaerView4));
        }

        public static string GetJson(object o)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //JavaScriptSerializer json = new JavaScriptSerializer();
            //json.Serialize(o, stringBuilder);
            //return stringBuilder.ToString();

            // 和下面的代码有同样的效果
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(o);
        }

    }
}
