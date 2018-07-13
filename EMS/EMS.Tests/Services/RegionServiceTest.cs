using System;
using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EMS.Tests.Services
{
    [TestClass]
    public class RegionServiceTest
    {
        [TestMethod]
        public void TestRegionMainUserName()
        {
            RegionMainService service = new RegionMainService();

            RegionMainViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(model);
        }
    }
}
