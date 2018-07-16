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
    public class EnergyItemStatisticServiceTest
    {
        [TestMethod]
        public void TestGetEnergyItemStatisticViewModelByUserName()
        {
            EnergyItemStatisticService service = new EnergyItemStatisticService();
            EnergyItemStatisticViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetEnergyItemStatisticViewModelByBuilID_Date()
        {
            DateTime today = DateTime.Now;
            EnergyItemStatisticService service = new EnergyItemStatisticService();
            EnergyItemStatisticViewModel ViewModel = service.GetViewModel("000001G001",today.ToString());

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

    }
}
