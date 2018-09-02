using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class DepartmentRankTest
    {
        [TestMethod]
        public void TestGetRankList()
        {
            DepartmentRankDbContext context = new DepartmentRankDbContext();
            List<EMSValue> list = context.GetRankList("000001G001","2018-08-01","2018-08-31","01000");

            Console.WriteLine(UtilTest.GetJson(list));

        }
    }
}
