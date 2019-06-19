using EMS.DAL.Services;
using EMS.DAL.Services.Circuit;
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
    public class MultiRateServiceTest
    {
        [TestMethod]
        public void TestGetChildrenCircuit()
        {
            MultiRateService service = new MultiRateService();
            List<TreeViewModel> treeView = service.GetTreeListViewModel("000001G001", "01000");

            Console.WriteLine(UtilTest.GetJson(treeView));
        }

        [TestMethod]
        public void TestGetViewModelBy_userName()
        {
            MultiRateService service = new MultiRateService();
            var   viewModel = service.GetViewModel("Admin"); 

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_user_Build()
        {
            MultiRateService service = new MultiRateService();
            var viewModel = service.GetViewModel("Admin","000001G003");

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_Build_type_date_DD()
        {
            MultiRateService service = new MultiRateService();
            DateTime today = DateTime.Now;
            var viewModel = service.GetViewModel( "000001G003","DD", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_Build_type_date_MM()
        {
            MultiRateService service = new MultiRateService();
            DateTime today = DateTime.Now;
            var viewModel = service.GetViewModel("000001G003", "MM", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_Build_type_date_YY()
        {
            MultiRateService service = new MultiRateService();
            DateTime today = DateTime.Now;
            var viewModel = service.GetViewModel("000001G001", "YY", today.ToString("yyyy-MM-dd"));

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

        [TestMethod]
        public void TestGetViewModelBy_Build_type_date_IDS_YY()
        {
            MultiRateService service = new MultiRateService();
            DateTime today = DateTime.Now;
            string ids= "000001G0030275,000001G0030276";
            var viewModel = service.GetViewModel("000001G003", "YY", today.ToString("yyyy-MM-dd"), ids);

            Console.WriteLine(UtilTest.GetJson(viewModel));
        }

    }
}
