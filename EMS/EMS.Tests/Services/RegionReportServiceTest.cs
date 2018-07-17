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
    public class RegionReportServiceTest
    {
        RegionReportService service = new RegionReportService();

        [TestMethod]
        public void TestGetRegionReportViewModelByUser()
        {
            DateTime today = DateTime.Now;
            RegionReportViewModel reportViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

        [TestMethod]
        public void TestGetRegionReportViewModelByBuild_EnergyCode()
        {
            DateTime today = DateTime.Now;
            RegionReportViewModel reportViewModel = service.GetViewModel("000001G001","01000");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

        [TestMethod]
        public void TestGetRegionReportViewModelByRegionIDs_EnergyCode_Date_Type()
        {
            DateTime today = DateTime.Now;
            string[] regionIDs = { "000001G0010002", "000001G0010005", "000001G0010006", "000001G0010007" };
            RegionReportViewModel reportViewModel = service.GetViewModel(regionIDs, "01000", today.ToString(),"MM");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }
    }
}
