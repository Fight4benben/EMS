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
    public class BuildSetServiceTest
    {
        [TestMethod]
        public void TestGetBuildListByuser()
        {
            BuildSetService service = new BuildSetService();
            BuildSetViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestBuildInfo()
        {
            BuildSetService service = new BuildSetService();
            BuildSetViewModel ViewModel = service.GetViewModel("000001G001");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestAddBuild_OK()
        {
            BuildSetService service = new BuildSetService();

            BuildSetViewModel ViewModel = service.GetViewModel("000001G001");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestUpdataBuild_OK()
        {
            BuildSetService service = new BuildSetService();

            BuildSetViewModel ViewModel = service.GetViewModel("000001G001");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
