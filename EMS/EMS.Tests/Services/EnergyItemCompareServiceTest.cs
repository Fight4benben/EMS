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
    public class EnergyItemCompareServiceTest
    {
        [TestMethod]
        public void TestGetEnergyItemCompareViewModel()
        {
            EnergyItemCompareService service = new EnergyItemCompareService();
            EnergyItemCompareViewModel EnergyCompaerView = service.GetEnergyItemCompareViewModel("admin");
            EnergyItemCompareViewModel EnergyCompaerView2 = service.GetEnergyItemCompareViewModel("000001G001", "2018-06-07 14:00:00");


            //Console.WriteLine(UtilTest.GetJson(EnergyCompaerView));
            Console.WriteLine(UtilTest.GetJson(EnergyCompaerView2));
        }
    }
}
