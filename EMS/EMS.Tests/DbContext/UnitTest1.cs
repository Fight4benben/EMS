using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMS.DAL;
using EMS.DAL.Entities;
using System.Linq;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImpl;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            EnergyDB db = new EnergyDB();
            int count = db.BuildInfo.ToList().Count();
            Assert.AreEqual(1,count);
        }

        [TestMethod]
        public void TestRepository()
        {
            IHomeDbContext context = new HomeDbContext();
            BuildInfo build = context.GetBuildById("000001G001");
            Assert.AreEqual(build.BuildId, "000001G001");
        }
    }
}
