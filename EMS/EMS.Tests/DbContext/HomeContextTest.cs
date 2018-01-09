using System;
using System.Collections.Generic;
using EMS.DAL.Entities;
using EMS.DAL.RepositoryImpl;
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
    }
}
