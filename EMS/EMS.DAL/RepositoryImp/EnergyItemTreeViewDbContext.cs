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
    public class EnergyItemTreeViewDbContext : IEnergyItemTreeView
    {
        private EnergyDB _db = new EnergyDB();

        public List<TreeViewModel> GetEnergyItemTreeViewList(string buildId)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            List<EnergyItemInfo> energyItemInfos = _db.Database.SqlQuery<EnergyItemInfo>(EnergyItemTreeViewResources.EnergyItemTreeViewSQL, sqlParameters).ToList();

            var parentEnergyItem = energyItemInfos.Where(c => (c.EnergyItemCode == "01A00" || c.EnergyItemCode == "01B00"
                                                           || c.EnergyItemCode == "01C00" || c.EnergyItemCode == "01D00"));

            foreach (var item in parentEnergyItem)
            {
                TreeViewModel parentNode = new TreeViewModel();
                parentNode.Id = item.EnergyItemCode;
                parentNode.Text = item.EnergyItemName;

                List<TreeViewModel> children = GetChildrenNodes2Level(energyItemInfos, item);
                if (children.Count != 0)
                    parentNode.Nodes = children;
                treeViewModel.Add(parentNode);
            }

            return treeViewModel;
        }

        public List<TreeViewModel> GetEnergyItemTreeViewList(string buildId, string energyItemCode)
        {
            throw new NotImplementedException();
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
