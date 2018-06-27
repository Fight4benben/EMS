using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class EnergyItemTreeViewDbContextTest
    {
        [TestMethod]
        public void TestGetEnergyItemTreeView()
        {
            IEnergyItemTreeView context = new EnergyItemTreeViewDbContext();
            //DateTime today = DateTime.Now;

            List<TreeViewModel> treeViewModel = context.GetEnergyItemTreeViewList("000001G008");
            foreach (var item in treeViewModel)
            {
                Console.WriteLine("分项ID：{0}, 分项名称：{1}, 子节点：{2}；", item.Id, item.Text,item.Nodes);
            }
        }
    }
}
