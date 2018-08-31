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
    public class DepartmentEnergyAverageServiceTest
    {
        [TestMethod]
        public void TestGetDeptEnergyAvgViewModelByUser()
        {
            DateTime today = DateTime.Now;
            DepartmentEnergyAverageService service = new DepartmentEnergyAverageService();
            DepartmentEnergyAverageViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));

        }

        [TestMethod]
        public void TestGetDeptEnergyAvgViewModelByMonth()
        {
            DateTime today = DateTime.Now;
            DepartmentEnergyAverageService service = new DepartmentEnergyAverageService();
            DepartmentEnergyAverageViewModel ViewModel = service.GetViewModel("000001G003","01000","MM","2018-08");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetDeptEnergyAvgViewModelByQuarter()
        {
            DateTime today = DateTime.Now;
            DepartmentEnergyAverageService service = new DepartmentEnergyAverageService();
            DepartmentEnergyAverageViewModel ViewModel = service.GetViewModel("000001G003", "01000",  "QQ", "2018-09");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetDeptEnergyAvgViewModelByYear()
        {
            DateTime today = DateTime.Now;
            DepartmentEnergyAverageService service = new DepartmentEnergyAverageService();
            DepartmentEnergyAverageViewModel ViewModel = service.GetViewModel("000001G003", "01000","YY", "2018");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

    }
}
