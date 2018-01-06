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

        [TestMethod]
        public void TestUserMatch()
        {
            IUserContext userContext = new UserContext();
            //数据库中用户名admin,密码：空字符串的MD5
            //1."":验证通过，返回true； userContext.MatchUser("admin","");
            //2.错误密码：测试通过，返回false；userContext.MatchUser("admin","error");
            //3.null：执行不通过，需要在测试代码中增加判断或者将null转化为空
            bool result = userContext.MatchUser("admin",null);
            Assert.AreEqual(true,result);
        }
    }
}
