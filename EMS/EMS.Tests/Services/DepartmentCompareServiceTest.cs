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
    public class DepartmentCompareServiceTest
    {
        [TestMethod]
        public void TestGetDepartmentCompareViewModelByUser()
        {
            DateTime today = DateTime.Now;
            DepartmentCompareService service = new DepartmentCompareService();
            DepartmentCompareViewModel ViewModel = service.GetViewModel("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));

        }

        [TestMethod]
        public void TestGetDepartmentCompareViewModelByBuild_Date()
        {
            DateTime today = DateTime.Now;
            DepartmentCompareService service = new DepartmentCompareService();
            DepartmentCompareViewModel ViewModel = service.GetViewModel("000001G001", today.ToString());

            Console.WriteLine(UtilTest.GetJson(ViewModel));

        }

        [TestMethod]
        public void TestGetDepartmentCompareViewModelByBuild_DepartmentID_Date()
        {
            DateTime today = DateTime.Now;
            DepartmentCompareService service = new DepartmentCompareService();
            DepartmentCompareViewModel ViewModel = service.GetViewModel("000001G001", "D000001G001002", today.ToString());

            Console.WriteLine(UtilTest.GetJson(ViewModel));

        }
    }
}
