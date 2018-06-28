using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using EMS.Tests.Utils;
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

            Console.WriteLine(UtilTest.GetJson(EnergyItemOverviewView));
            //Console.WriteLine(circuitOverviewView2);
            //Console.WriteLine(circuitOverviewView3);
            //Console.WriteLine(circuitOverviewView4);
            //Console.WriteLine(circuitOverviewView5);
        }

    }
}
