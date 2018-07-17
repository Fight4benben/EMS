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
    public class RegionCompareServiceTest
    {
        RegionCompareService service = new RegionCompareService();

        [TestMethod]
        public void TestGetRegionCompareViewModelByUser()
        {
            RegionCompareViewModel compareViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(compareViewModel));
        }

        [TestMethod]
        public void TestGetRegionCompareViewModelByBuild_EnergyCode()
        {
            DateTime today = DateTime.Now;
            RegionCompareViewModel reportViewModel = service.GetViewModel("000001G001", "01000");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

        [TestMethod]
        public void TestGetRegionCompareViewModelByRegionID_EnergyCode_Date()
        {
            DateTime today = DateTime.Now;
            RegionCompareViewModel compareViewModel = service.GetViewModel("01000", "000001G0010002", today.ToString());

            Console.WriteLine(UtilTest.GetJson(compareViewModel));
        }
    }
}
