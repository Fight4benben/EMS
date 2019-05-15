using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class CircuitRingRatioService
    {
        private ICircuitCompareDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();

        public CircuitRingRatioService()
        {
            context = new CircuitCompareDbContext();
        }


        /// <summary>
        /// 支路同比分析
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的第一个回路当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表，以及第一支路数据</returns>
        public CircuitCompareViewModel GetDayRingRationViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;
            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<EMS.DAL.Entities.Circuit> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);
            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> compareData = context.GetDayRingCompareValueList(buildId, circuitId, today.ToString());

            CircuitCompareViewModel circuitCompareView = new CircuitCompareViewModel();
            circuitCompareView.Builds = builds;
            circuitCompareView.Energys = energys;
            circuitCompareView.TreeView = treeView;
            circuitCompareView.CompareData = compareData;

            return circuitCompareView;
        }

        public CircuitCompareViewModel GetDayRingRationViewModelByBuild(string userName,string buildId)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;
            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<EMS.DAL.Entities.Circuit> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);
            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> compareData = context.GetDayRingCompareValueList(buildId, circuitId, today.ToString());

            CircuitCompareViewModel circuitCompareView = new CircuitCompareViewModel();
            circuitCompareView.Builds = builds;
            circuitCompareView.Energys = energys;
            circuitCompareView.TreeView = treeView;
            circuitCompareView.CompareData = compareData;

            return circuitCompareView;
        }

        /// <summary>
        /// 根据用户传入的建筑ID，查找该建筑包含的分类能耗，所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回数据：能源按钮列表，回路列表，以及第一支路数据</returns>
        public CircuitCompareViewModel GetDayRingRationViewModel(string buildId, string date)
        {
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;
            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<EMS.DAL.Entities.Circuit> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);
            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> compareData = context.GetDayRingCompareValueList(buildId, circuitId, date);

            CircuitCompareViewModel circuitCompareView = new CircuitCompareViewModel();
            circuitCompareView.Energys = energys;
            circuitCompareView.TreeView = treeView;
            circuitCompareView.CompareData = compareData;

            return circuitCompareView;
        }

        /// <summary>
        /// 根据用户传入的建筑ID，分类能耗编码，查找所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回数据：回路列表，以及第一支路数据</returns>
        public CircuitCompareViewModel GetDayRingRationViewModel(string buildId, string energyCode, string date)
        {
            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);
            List<EMS.DAL.Entities.Circuit> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);
            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> compareData = context.GetDayRingCompareValueList(buildId, circuitId, date);

            CircuitCompareViewModel circuitCompareView = new CircuitCompareViewModel();
            circuitCompareView.TreeView = treeView;
            circuitCompareView.CompareData = compareData;

            return circuitCompareView;
        }


        /// <summary>
        /// 根据用户传入的建筑ID，分类能耗编码和支路编码，查找该支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗编码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回数据：支路用能数据</returns>
        public CircuitCompareViewModel GetDayRingRationViewModel(string buildId, string energyCode, string circuitId, string date)
        {
            List<CircuitValue> compareData = context.GetDayRingCompareValueList(buildId, circuitId, date);
            CircuitCompareViewModel circuitCompareView = new CircuitCompareViewModel();
            circuitCompareView.CompareData = compareData;

            return circuitCompareView;
        }




        /*-----------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// 根据建筑ID，和能源类型获取树状结构
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyItemCode">能源类型</param>
        /// <returns>树状结构</returns>
        public List<TreeViewModel> GetTreeListViewModel(string buildId, string energyItemCode)
        {
            List<EMS.DAL.Entities.Circuit> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyItemCode);
            var parentCircuits = circuits.Where(c => (c.ParentId == "-1" || string.IsNullOrEmpty(c.ParentId)));
            List<TreeViewModel> treeList = new List<TreeViewModel>();

            foreach (var item in parentCircuits)
            {
                TreeViewModel parentNode = new TreeViewModel();
                List<TreeViewModel> children = GetChildrenNodes(circuits, item);
                parentNode.Id = item.CircuitId;
                parentNode.Text = item.CircuitName;
                if (children.Count != 0)
                    parentNode.Nodes = children;
                treeList.Add(parentNode);
            }

            return treeList;
        }

        /// <summary>
        /// 递归调用方式填充树状结构的子节点
        /// </summary>
        /// <param name="circuits"></param>
        /// <param name="circuit"></param>
        /// <returns></returns>
        List<TreeViewModel> GetChildrenNodes(List<EMS.DAL.Entities.Circuit> circuits, EMS.DAL.Entities.Circuit circuit)
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

        string[] GetCircuitIds(List<EMS.DAL.Entities.Circuit> circuits)
        {
            List<string> list = new List<string>();
            foreach (var circuit in circuits)
            {
                list.Add(circuit.CircuitId);
            }

            return list.ToArray();
        }
    }
}
