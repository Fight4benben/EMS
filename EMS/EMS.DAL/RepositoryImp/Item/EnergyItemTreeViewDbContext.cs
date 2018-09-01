using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class EnergyItemTreeViewDbContext : IEnergyItemTreeViewDbContext
    {
        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 根据建筑ID获取分项用能列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public List<EnergyItemInfo> GetEnergyItemInfoList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            List<EnergyItemInfo> energyItemInfos = _db.Database.SqlQuery<EnergyItemInfo>(EnergyItemTreeViewResources.EnergyItemTreeViewSQL, sqlParameters).ToList();
            return energyItemInfos;
        }

        public List<TreeViewModel> GetEnergyItemTreeViewList(string buildId)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            List<EnergyItemInfo> energyItemInfos = _db.Database.SqlQuery<EnergyItemInfo>(EnergyItemTreeViewResources.EnergyItemTreeViewSQL, sqlParameters).ToList();

            foreach (var item in energyItemInfos)
            {
                EnergyItemInfo info = energyItemInfos.Find(e=>e.EnergyItemCode == item.ParentItemCode);

                if(info == null)
                {
                    TreeViewModel parent = new TreeViewModel();
                    List<TreeViewModel> children = GetChildrenNodes(energyItemInfos,item);
                    parent.Id = item.FormulaID;
                    parent.Text = item.EnergyItemName;

                    if (children.Count != 0)
                        parent.Nodes = children;

                    treeViewModel.Add(parent);
                }
            }

            //var parentItemCodes = energyItemInfos.Where(c => (c.ParentItemCode == "-1" || string.IsNullOrEmpty(c.ParentItemCode)));
            //foreach (var item in parentItemCodes)
            //{
            //    TreeViewModel parentNode = new TreeViewModel();
            //    List<TreeViewModel> children = GetChildrenNodes(energyItemInfos, item);
            //    parentNode.Id = item.EnergyItemCode;
            //    parentNode.Text = item.EnergyItemName;
            //    if (children.Count != 0)
            //        parentNode.Nodes = children;
            //    treeViewModel.Add(parentNode);
            //}

            return treeViewModel;
        }

        public List<TreeViewModel> GetEnergyItemTreeViewList(string buildId, string energyItemCode)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyItemCode)
            };
            List<EnergyItemInfo> energyItemInfos = _db.Database.SqlQuery<EnergyItemInfo>(EnergyItemTreeViewResources.EnergyItemTreeViewByEnergyCodeSQL, sqlParameters).ToList();

            var parentItemCodes = energyItemInfos.Where(c => (c.ParentItemCode == "-1" || string.IsNullOrEmpty(c.ParentItemCode)));
            foreach (var item in parentItemCodes)
            {
                TreeViewModel parentNode = new TreeViewModel();
                List<TreeViewModel> children = GetChildrenNodes(energyItemInfos, item);
                parentNode.Id = item.EnergyItemCode;
                parentNode.Text = item.EnergyItemName;
                if (children.Count != 0)
                    parentNode.Nodes = children;
                treeViewModel.Add(parentNode);
            }

            return treeViewModel;
        }


        /// <summary>
        /// 递归调用方式填充树状结构的子节点
        /// </summary>
        /// <param name="circuits"></param>
        /// <param name="circuit"></param>
        /// <returns></returns>
        List<TreeViewModel> GetChildrenNodes(List<EnergyItemInfo> energyItemInfos, EnergyItemInfo energyItemInfo)
        {
            string parentCode = energyItemInfo.EnergyItemCode;
            List<TreeViewModel> circuitList = new List<TreeViewModel>();
            var children = energyItemInfos.Where(c => c.ParentItemCode == parentCode);

            foreach (var item in children)
            {
                TreeViewModel node = new TreeViewModel();
                node.Id = item.FormulaID;
                node.Text = item.EnergyItemName;
                if (GetChildrenNodes(energyItemInfos, item).Count != 0)
                    node.Nodes = GetChildrenNodes(energyItemInfos, item);

                circuitList.Add(node);
            }

            return circuitList;
        }

        /// <summary>
        /// 获取2级分项节点
        /// </summary>
        /// <param name="energyItemInfos">分项信息</param>
        /// <param name="energyItemInfo">父节点编号</param>
        /// <returns></returns>
        List<TreeViewModel> GetChildrenNodes2Level(List<EnergyItemInfo> energyItemInfos, EnergyItemInfo energyItemInfo)
        {
            List<TreeViewModel> energyItemList = new List<TreeViewModel>();
            string parentId = energyItemInfo.EnergyItemCode;

            switch (parentId)
            {
                case "01A00":
                    var children = energyItemInfos.Where(c => c.EnergyItemCode == "01A[^0]0");
                    foreach (var item in children)
                    {
                        TreeViewModel node = new TreeViewModel();
                        node.Id = item.EnergyItemCode;
                        node.Text = item.EnergyItemName;
                        if (GetChildrenNodes3Level(energyItemInfos, item).Count != 0)
                            node.Nodes = GetChildrenNodes3Level(energyItemInfos, item);

                        energyItemList.Add(node);
                    }
                    break;

            }


            return energyItemList;
        }

        /// <summary>
        /// 获取3级分项节点
        /// </summary>
        /// <param name="energyItemInfos">分项信息</param>
        /// <param name="energyItemInfo">父节点编号</param>
        /// <returns></returns>
        List<TreeViewModel> GetChildrenNodes3Level(List<EnergyItemInfo> energyItemInfos, EnergyItemInfo energyItemInfo)
        {
            List<TreeViewModel> energyItemList = new List<TreeViewModel>();
            string parentId = energyItemInfo.EnergyItemCode;
            switch (parentId)
            {
                case "01A[^0]0":
                    var children = energyItemInfos.Where(c => c.EnergyItemCode == "01A[^00]");
                    foreach (var item in children)
                    {
                        TreeViewModel node = new TreeViewModel();
                        node.Id = item.EnergyItemCode;
                        node.Text = item.EnergyItemName;
                        

                        energyItemList.Add(node);
                    }
                    break;

            }

            return energyItemList;
        }
    }
}
