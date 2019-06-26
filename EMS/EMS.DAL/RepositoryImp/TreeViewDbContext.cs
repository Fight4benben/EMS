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
    public class TreeViewDbContext:ITreeViewDbContext
    {
        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 根据当前登录用户获取建筑列表
        /// </summary>
        /// <param name="userName">登陆用户名</param>
        /// <returns>建筑列表</returns>
        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        /// <summary>
        /// 根据建筑ID ，获取该建筑关联的分类能耗
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }

        /// <summary>
        /// 获取部门树状结构
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public List<TreeViewModel> GetDepartmentTreeViewList(string buildId,string energyCode)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(TreeViewResources.DepartmentTreeViewByBuildIDSQL, sqlParameters).ToList();

            //var parentItemCodes = treeViewInfos.Where(c => (c.ParentID == "-1"));
            //foreach (var parentItem in parentItemCodes)
            //{
            //    TreeViewModel parentNode = new TreeViewModel();
            //    List<TreeViewModel> children = GetChildrenNodes(treeViewInfos, parentItem);
            //    parentNode.Id = parentItem.ID;
            //    parentNode.Text = parentItem.Name;
            //    if (children.Count != 0)
            //        parentNode.Nodes = children;
            //    treeViewModel.Add(parentNode);
            //}
            treeViewModel = EMS.DAL.Utils.Util.GetTreeViewModel(treeViewInfos);

            return treeViewModel;
        }

        /// <summary>
        /// 获取部门ID数组
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public string[] GetDepartmentIDs(string buildId,string energyCode)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(TreeViewResources.DepartmentTreeViewByBuildIDSQL, sqlParameters).ToList();

            return GetIDs(treeViewInfos);
        }

        public List<TreeViewModel> GetRegionTreeViewList(string buildId)
        {
            throw new NotImplementedException();
        }

        public List<TreeViewModel> GetRegionTreeViewList(string buildId, string energyItemCode)
        {
            throw new NotImplementedException();
        }

        public string[] GetRegionIDs(string buildId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 递归调用方式填充树状结构的子节点
        /// </summary>
        /// <param name="treeViewInfos"></param>
        /// <param name="parentItem"></param>
        /// <returns></returns>
        List<TreeViewModel> GetChildrenNodes(List<TreeViewInfo> treeViewInfos, TreeViewInfo parentItem)
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

        List<TreeViewModel> GetChildrenNodes(List<EMS.DAL.Entities.CircuitList> circuits, EMS.DAL.Entities.CircuitList circuit)
        {
            string parentId = circuit.CircuitId;
            List<TreeViewModel> circuitList = new List<TreeViewModel>();
            var children = circuits.Where(c => c.ParentId == parentId);

            foreach (var item in children)
            {
                TreeViewModel node = new TreeViewModel();
                node.Id = item.CircuitId;
                node.Text = item.CircuitName;
                if (GetChildrenNodes(circuits, item).Count != 0)
                    node.Nodes = GetChildrenNodes(circuits, item);

                circuitList.Add(node);
            }

            return circuitList;
        }

        string[] GetIDs(List<TreeViewInfo> treeViewInfos)
        {
            List<string> list = new List<string>();
            foreach (var Item in treeViewInfos)
            {
                list.Add(Item.ID);
            }
            return list.ToArray();
        }

       
    }
}
