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
    public class UserMenuServiceTest
    {

        [TestMethod]
        public void TestGetAdminUserMenu()
        {
            UserMenuService service = new UserMenuService();
            UserMenuViewModel ViewModel = service.GetAdminMenuViewModel("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetUserMenu()
        {
            UserMenuService service = new UserMenuService();
            int userID = 17;
            UserMenuViewModel ViewModel = service.GetUserMenuViewModel(userID);

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestSetUserMenu()
        {
            UserMenuService service = new UserMenuService();
            int userID = 23;
            string menuIDs= "1P|3P|4P|4.1|4.2|4.3|4.4|5P|5.1|5.2|5.3|6P|6.1|6.2|6.3|6.4";
            UserMenuViewModel ViewModel = service.SetUserMenu(userID, menuIDs);

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
