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
    public class DepartmentOverviewServiceTest
    {
        [TestMethod]
        public void TestGetDepartmentOverviewViewModel()
        {
            DateTime today = DateTime.Now;
            DepartmentOverviewService service = new DepartmentOverviewService();
            DepartmentOverviewModel EnergyItemOverviewView = service.GetDepartmentOverviewViewModel("admin");

            Console.WriteLine(UtilTest.GetJson(EnergyItemOverviewView));
           
        }

        [TestMethod]
        public void TestGetDepartmentOverviewViewModelByBuildIDAndTime()
        {
            DateTime today = DateTime.Now;
            DepartmentOverviewService service = new DepartmentOverviewService();
            DepartmentOverviewModel EnergyItemOverviewView = service.GetDepartmentOverviewViewModel("000001G001", today.ToString());

            Console.WriteLine(UtilTest.GetJson(EnergyItemOverviewView));

        }
    }
}
