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

        [TestMethod]
        public void TestAddUser()
        {
            UserSetService service = new UserSetService();
            UserSetViewModel ViewModel = service.AddUser("tes2","","1");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestUpdateUser_OK()
        {
            UserSetService service = new UserSetService();
            UserSetViewModel ViewModel = service.UpdateUser("tes2", "a", "d41d8cd98f00b204e9800998ecf8427e", "2");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestUpdateUser_NG()
        {
            UserSetService service = new UserSetService();
            UserSetViewModel ViewModel = service.UpdateUser("tes2", "a","n", "2");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            UserSetService service = new UserSetService();
            UserSetViewModel ViewModel = service.DeleteUser("tes2");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}
