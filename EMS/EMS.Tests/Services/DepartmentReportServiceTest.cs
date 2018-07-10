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
    public class DepartmentReportServiceTest
    {
        [TestMethod]
        public void TestGetDepartmentReportViewModelByUser()
        {
            DateTime today = DateTime.Now;
            DepartmentReportService service = new DepartmentReportService();
            DepartmentReportViewModel reportViewModel = service.GetViewModel("admin");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

        [TestMethod]
        public void TestGetDepartmentReportViewModelByBuild_Date()
        {
            DateTime today = DateTime.Now;
            DepartmentReportService service = new DepartmentReportService();
            DepartmentReportViewModel reportViewModel = service.GetViewModel("000001G001", today.ToString());

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

        [TestMethod]
        public void TestGetDepartmentReportViewModelByBuild_Date_DD()
        {
            DateTime today = DateTime.Now;
            string[] departmentIDs = { "D000001G001001", "D000001G001002", "D000001G001003" };
            DepartmentReportService service = new DepartmentReportService();

            DepartmentReportViewModel reportViewModel = service.GetViewModel(departmentIDs, today.ToString(),"DD");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

        [TestMethod]
        public void TestGetDepartmentReportViewModelByBuild_Date_MM()
        {
            DateTime today = DateTime.Now;
            string[] departmentIDs = { "D000001G001001", "D000001G001002", "D000001G001003" };
            DepartmentReportService service = new DepartmentReportService();

            DepartmentReportViewModel reportViewModel = service.GetViewModel(departmentIDs, today.ToString(), "MM");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

        [TestMethod]
        public void TestGetDepartmentReportViewModelByBuild_Date_YY()
        {
            DateTime today = DateTime.Now;
            string[] departmentIDs = { "D000001G001001", "D000001G001002", "D000001G001003" };
            DepartmentReportService service = new DepartmentReportService();

            DepartmentReportViewModel reportViewModel = service.GetViewModel(departmentIDs, today.ToString(), "YY");

            Console.WriteLine(UtilTest.GetJson(reportViewModel));
        }

    }
}
