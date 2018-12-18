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
    public class DepartmentAreaAvgRankServiceTest
    {
        [TestMethod]
        public void TestGetDepartmentAreaAvgRankViewModelByUser()
        {
            DateTime today = DateTime.Now;
            DepartmentAreaAvgRankService service = new DepartmentAreaAvgRankService();
            DepartmentAreaAvgRankViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));

        }

        [TestMethod]
        public void TestGetDepartmentAreaAvgRankViewModelByMonth()
        {
            DateTime today = DateTime.Now;
            DepartmentAreaAvgRankService service = new DepartmentAreaAvgRankService();
            DepartmentAreaAvgRankViewModel ViewModel = service.GetViewModel("000001G003", "01000", "2018-08");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
