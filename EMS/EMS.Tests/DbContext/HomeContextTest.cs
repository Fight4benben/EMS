using System;
using System.Collections.Generic;
using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class HomeContextTest
    {
        [TestMethod]
        public void TestEnergyClassify()
        {
            HomeDbContext context = new HomeDbContext();
            List<EnergyClassify> list =  context.GetEnergyClassifyValues("000001G001","2018-01-07");

            foreach (EnergyClassify item in list)
            {
                Console.WriteLine("分类名称:{0},当月数据：{1}，当年数据：{2}，单位：{3}，比率：{4}",item.EnergyItemName,item.MonthValue,item.YearValue,item.Unit,item.EnergyRate);
            }
        }

        [TestMethod]
        public void TestGetEnergyItemValues()
        {
            HomeDbContext context = new HomeDbContext();
            List<EnergyItem> list = context.GetEnergyItemValues("000001G001", "2018-01-07");

            foreach (EnergyItem item in list)
            {
                Console.WriteLine("分项名称:{0},分项编号：{1}，分项数据：{2}，建筑编号：{3}", item.EnergyItemName, item.EnergyItemCode, item.Value, item.BuildID);
            }
        }

        [TestMethod]
        public void TestGetHourValues()
        {
            HomeDbContext context = new HomeDbContext();
            List<HourValue> list = context.GetHourValues("000001G001", "2018-01-07 12:00:00");

            foreach (HourValue item in list)
            {
                Console.WriteLine("分类名称:{0},时间：{1}，数据：{2}", item.EnergyItemCode, item.ValueTime.ToString("yyyy-MM-dd HH:mm:ss"), item.Value);
            }
        }

        [TestMethod]
        public void TestGetBuildsByUserName()
        {
            HomeDbContext context = new HomeDbContext();
            List<BuildViewModel> list = context.GetBuildsByUserName("admin");

            foreach (BuildViewModel item in list)
            {
                Console.WriteLine("建筑ID:{0},建筑名称：{1}", item.BuildID, item.BuildName);
            }
        }

        [TestMethod]
        public void TestGetNameByCode()
        {
            HomeDbContext context = new HomeDbContext();
            EnergyItemDict energy = context.GetEnergyItemByCode("01000");

            Console.WriteLine(energy.EnergyItemName+":"+energy.EnergyItemCode);
        }
    }
}
