using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Services.Setting
{
    [TestClass]
    public class UserSetServiceTest
    {
        [TestMethod]
        public void TestGetAllUser()
        {
            UserSetService service = new UserSetService();
            UserSetViewModel ViewModel = service.GetAllUserViewModel();

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
