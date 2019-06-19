using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.IRepository.Circuit;
using EMS.DAL.RepositoryImp;
using EMS.DAL.RepositoryImp.Circuit;
using EMS.DAL.ViewModels;
using EMS.DAL.ViewModels.Circuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services.Circuit
{
    public class MultiRateService
    {
        private IMultiRateDbContext context;

        public MultiRateService()
        {
            context = new MultiRateDbContext();
        }

        public MultiRateViewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode = "01000";
            List<EMS.DAL.Entities.CircuitList> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<MultiRateData> data = context.GetReportValueList(buildId, energyCode, "MM", today.ToShortDateString());

            MultiRateViewModel circuitReportView = new MultiRateViewModel();
            circuitReportView.Builds = builds;
            circuitReportView.Energys = energys;
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = "MM";

            return circuitReportView;
        }

        public MultiRateViewModel GetViewModel(string userName, string buildId)
        {
            DateTime today = DateTime.Now;

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode = "01000";
            List<EMS.DAL.Entities.CircuitList> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<MultiRateData> data = context.GetReportValueList(buildId, energyCode, "MM", today.ToShortDateString());

            MultiRateViewModel circuitReportView = new MultiRateViewModel();
            circuitReportView.Energys = energys;
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = "MM";

            return circuitReportView;
        }

        public MultiRateViewModel GetViewModel(string buildId, string type, string date)
        {
            DateTime today = DateTime.Now;

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode = "01000";
            List<EMS.DAL.Entities.CircuitList> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<MultiRateData> data = context.GetReportValueList(buildId, energyCode, type, date);

            MultiRateViewModel circuitReportView = new MultiRateViewModel();
            circuitReportView.Energys = energys;
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = type.ToUpper();

            return circuitReportView;
        }

        public MultiRateViewModel GetViewModel(string buildId, string type, string date, string circuits)
        {
            string[] circuitArry = circuits.Split(',');
            string energyCode = "01000";

            List<MultiRateData> data = context.GetReportValueList(buildId, energyCode, type, date, circuitArry);

            MultiRateViewModel reportView = new MultiRateViewModel();
            reportView.Data = data;
            reportView.ReportType = type;

            return reportView;
        }



        /// <summary>
        /// 根据建筑ID，和能源类型获取树状结构
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyItemCode">能源类型</param>
        /// <returns>树状结构</returns>
        public List<TreeViewModel> GetTreeListViewModel(string buildId, string energyItemCode)
        {
            List<EMS.DAL.Entities.CircuitList> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyItemCode);
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

        string[] GetCircuitIds(List<EMS.DAL.Entities.CircuitList> circuits)
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
