using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using System.Web.Script.Serialization;

namespace EMS.Tests.Services
{
    [TestClass]
    public class CircuitOverviewServiceTest
    {
        [TestMethod]
        public void TestGetCircuitOverviewViewModel()
        {
            CircuitOverviewService service = new CircuitOverviewService();
            CircuitOverviewViewModel circuitOverviewView = service.GetCircuitOverviewViewModel("admin");
            CircuitOverviewViewModel circuitOverviewView2 = service.GetCircuitOverviewViewModel("admin","000001G001");
            CircuitOverviewViewModel circuitOverviewView3 = service.GetCircuitOverviewViewModel("admin","000001G001","01000");
            CircuitOverviewViewModel circuitOverviewView4 = service.GetCircuitOverviewViewModel("admin","000001G001","01000","000001G0010001");
            CircuitOverviewViewModel circuitOverviewView5 = service.GetCircuitOverviewViewModel("admin","000001G001","01000", "000001G0010001","2018-01-30 14:00:00");

            Console.WriteLine(GetJson(circuitOverviewView));
            Console.WriteLine(circuitOverviewView2);
            Console.WriteLine(circuitOverviewView3);
            Console.WriteLine(circuitOverviewView4);
            Console.WriteLine(circuitOverviewView5);
        }

        public static string GetJson(object o)
        {
            StringBuilder stringBuilder = new StringBuilder();
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.Serialize(o, stringBuilder);
            return stringBuilder.ToString();

            // 和下面的代码有同样的效果
            //JavaScriptSerializer json = new JavaScriptSerializer();
            //return json.Serialize(o);
        }

    }
}
