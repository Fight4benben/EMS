using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.Utils;
using EMS.DAL.ViewModels;
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
    public class RegionReportDbContextTest
    {
        [TestMethod]
        public void TestRegionTreeView()
        {
            IRegionReportDbContext context = new RegionReportDbContext();
            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList("000001G001","02000");
            List<TreeViewModel> treeViewModel = UnitTest1.GetTreeViewModel(treeViewInfos);

            Console.WriteLine(UtilTest.GetJson(treeViewModel));

        }

        [TestMethod]
        public void TestDeptTreeView()
        {
            IDepartmentReportDbContext context = new DepartmentReportDbContext();
            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList("000001G001", "02000");
            List<TreeViewModel> treeViewModel = UnitTest1.GetTreeViewModel(treeViewInfos);

            Console.WriteLine(UtilTest.GetJson(treeViewModel));

        }
    }
}
