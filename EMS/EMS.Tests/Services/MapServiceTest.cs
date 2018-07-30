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
    public class MapServiceTest
    {
        [TestMethod]
        public void TestGetMapViewModelByUserName()
        {
            MapService service = new MapService();

            MapViewModel model = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(model));
        }
    }
}
