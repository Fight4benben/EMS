using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMS.DAL.Services;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;

namespace EMS.Tests.Services
{
    /// <summary>
    /// HomeServiceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class HomeServiceTest
    {
        [TestMethod]
        public void TestGetHomeViewModel()
        {
            HomeServices homeServices = new HomeServices();

            HomeViewModel homeViewModel = homeServices.GetHomeViewModel("000001G001","2018-01-06");

            Console.WriteLine(homeViewModel);
        }
    }
}
