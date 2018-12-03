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
        public void TestGetAllUser()
        {
            UserMenuService service = new UserMenuService();
            UserMenuViewModel ViewModel = service.GetUserMenuViewModel();

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
            int userID = 2;
            UserMenuViewModel ViewModel = service.GetUserMenuViewModel(userID);

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
