using System;
using System.Collections.Generic;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class RegionDbContextTest
    {
        [TestMethod]
        public void TestRegionMainCompare()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<EMSValue> list = context.GetRegionMainCompareValueList("000001G001","2018-07-13","01000","Demo");

            foreach (EMSValue item in list)
            {
                Console.WriteLine("ID:{0};Name:{1};Time:{2};Value:{3}",item.ID,item.Name,item.Time,item.Value);
            }
        }

        [TestMethod]
        public void TestRegionMainRank()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<RankValue> list =  context.GetRegionMainRankValueList("000001G001", "2018/07/13", "01000", "Demo");

            foreach (RankValue item in list)
            {
                Console.WriteLine("ClassifyID:{0};ClassifyName:{1};Name:{2};Value:{3}", item.ClassifyID, item.ClassifyName, item.Name, item.Value);
            }
        }

        [TestMethod]
        public void TestRegionMainPie()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<EMSValue> list = context.GetRegionPieValueList("000001G001", "2018-07-13", "01000", "Demo");

            foreach (EMSValue item in list)
            {
                Console.WriteLine("ID:{0};Name:{1};Time:{2};Value:{3}", item.ID, item.Name, item.Time, item.Value);
            }
        }

        [TestMethod]
        public void TestRegionMainBarTrend()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<EMSValue> list = context.GetRegionStackValueList("000001G001", "2018-07-13", "01000", "Demo");

            foreach (EMSValue item in list)
            {
                Console.WriteLine("ID:{0};Name:{1};Time:{2};Value:{3}", item.ID, item.Name, item.Time, item.Value);
            }
        }
    }
}
