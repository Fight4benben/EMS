using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMS.DAL;
using EMS.DAL.Entities;
using System.Linq;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System.Collections.Generic;

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
            Assert.AreEqual(1, count);
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
            bool result = userContext.MatchUser("admin", null);
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// 根据list生成树状结构
        /// </summary>
        /// <param name="treeViewInfos"></param>
        /// <returns></returns>
        public static List<TreeViewModel> GetTreeViewModel(List<TreeViewInfo> treeViewInfos)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();

            foreach (var item in treeViewInfos)
            {
                TreeViewInfo info = treeViewInfos.Find(e => e.ID == item.ParentID);

                if (info == null)
                {
                    TreeViewModel parent = new TreeViewModel();
                    List<TreeViewModel> children = GetChildrenNodes(treeViewInfos, item);
                    parent.Id = item.ID;
                    parent.Text = item.Name;

                    if (children.Count != 0)
                        parent.Nodes = children;

                    treeViewModel.Add(parent);
                }
            }

            return treeViewModel;
        }

        /// <summary>
        /// 递归调用方式填充树状结构的子节点
        /// </summary>
        /// <param name="treeViewInfos"></param>
        /// <param name="parentItem"></param>
        /// <returns></returns>
        public static List<TreeViewModel> GetChildrenNodes(List<TreeViewInfo> treeViewInfos, TreeViewInfo parentItem)
        {
            string parentID = parentItem.ID;
            List<TreeViewModel> treeViewList = new List<TreeViewModel>();
            var children = treeViewInfos.Where(c => c.ParentID == parentID);

            foreach (var item in children)
            {
                TreeViewModel node = new TreeViewModel();
                node.Id = item.ID;
                node.Text = item.Name;
                if (GetChildrenNodes(treeViewInfos, item).Count > 0)
                    node.Nodes = GetChildrenNodes(treeViewInfos, item);

                treeViewList.Add(node);
            }

            return treeViewList;
        }
    }
}
