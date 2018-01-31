using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMS.UI;
using EMS.UI.Controllers;

namespace EMS.Tests.Controllers
{
    [TestClass]
    public class CircuitOverviewControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            CircuitOverviewController controller = new CircuitOverviewController();

            // Act
            object result = controller.Get() ;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
