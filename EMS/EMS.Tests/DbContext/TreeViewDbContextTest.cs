using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
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
    public class TreeViewDbContextTest
    {
        [TestMethod]
        public void TestGetDepartmentTreeView()
        {
            ITreeViewDbContext context = new TreeViewDbContext();
            //DateTime today = DateTime.Now;

            List<TreeViewModel> treeViewModel = context.GetDepartmentTreeViewList("000001G001","01000");
            Console.WriteLine( UtilTest.GetJson(treeViewModel));
        }

        [TestMethod]
        public void TestGetCircuitTreeView()
        {
            ITreeViewDbContext context = new TreeViewDbContext();

            var treeViewModel = context.GetCircuitTreeListViewModel("000001G001", "01000");

            Console.WriteLine(UtilTest.GetJson(treeViewModel));
        }
    }
}
