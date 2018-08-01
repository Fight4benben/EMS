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
    public class PriceServiceTest
    {
        [TestMethod]
        public void TestGetPriceViewModelByUserName()
        {
            PriceService service = new PriceService();

            PriceViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetPriceViewModelByBuildID_Date_Type()
        {
            PriceService service = new PriceService();
            DateTime today = DateTime.Now;
            PriceViewModel model = service.GetViewModel("000001G001", "2018-07","MM");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetPriceViewModelByBuildID_EnergyCode_Date_Type()
        {
            PriceService service = new PriceService();
            DateTime today = DateTime.Now;
            PriceViewModel model = service.GetViewModel("000001G001","01000", "2018", "YY");

            Console.WriteLine(UtilTest.GetJson(model));
        }

        [TestMethod]
        public void TestGetPriceViewModelByBuildID_EnergyCode_RegionIDs_Date_Type()
        {
            PriceService service = new PriceService();
            DateTime today = DateTime.Now;
            string[] regionIDs = { "000001G0010002", "000001G0010005", "000001G0010006", "000001G0010007" };

            PriceViewModel model = service.GetViewModel("000001G001", "01000", regionIDs, "2018-07", "MM");

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
