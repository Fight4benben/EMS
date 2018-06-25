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
    public class EnergyItemOverviewServiceTest
    {
        [TestMethod]
        public void TestGetEnergyItemOverviewViewModel()
        {
            DateTime today = DateTime.Now;
            EnergyItemOverviewService service = new EnergyItemOverviewService();
            EnergyItemOverviewModel EnergyItemOverviewView = service.GetEnergyItemOverviewViewModel("admin");
            EnergyItemOverviewModel EnergyItemOverviewView2 = service.GetEnergyItemOverviewViewModel("000001G001", today.ToString());

            Console.WriteLine(GetJson(EnergyItemOverviewView));
            //Console.WriteLine(circuitOverviewView2);
            //Console.WriteLine(circuitOverviewView3);
            //Console.WriteLine(circuitOverviewView4);
            //Console.WriteLine(circuitOverviewView5);
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
