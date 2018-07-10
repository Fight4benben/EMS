using EMS.Tests.Utils;
using EMS.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMS.Tests.Controllers
{
    [TestClass]
    public class DeptOverviewControllerTest
    {
        [TestMethod]
        public void TestGetDeptOverviewControllerByUser()
        {
            // Arrange
            DeptOverviewController controller = new DeptOverviewController();

            // Act
            //ViewResult result = controller.Get() as ViewResult;
             var result = controller.Get() ;
            Console.WriteLine(result); 

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
