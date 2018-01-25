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
    public class CircuitReportService
    {
        private ICircuitReportDbContext context;

        public CircuitReportService()
        {
            context = new CircuitReportDbContext();
        }

        /// <summary>
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的回路，所有回路的日报表（时间是当日）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public CircuitReportViewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode = energys.First().EnergyItemCode;
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId,energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId,energyCode);

            List<ReportValue> data = context.GetReportValueList(circuitIds,today.ToShortDateString(),"DD");

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.Builds = builds;
            circuitReportView.Energys = energys;
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = "DD";

            return circuitReportView;
        }

        public CircuitReportViewModel GetViewModel(string buildId,string type,string date)
        {
            DateTime now = Utils.Util.ConvertString2DateTime(date,"yyyy-MM-dd");
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode = energys.First().EnergyItemCode;
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<ReportValue> data = context.GetReportValueList(circuitIds, now.ToShortDateString(), type);

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.Energys = energys;
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = type;

            return circuitReportView;
        }

        public CircuitReportViewModel GetViewModel(string buildId,string energyCode, string type, string date)
        {
            DateTime now = Utils.Util.ConvertString2DateTime(date, "yyyy-MM-dd");
           
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<ReportValue> data = context.GetReportValueList(circuitIds, now.ToShortDateString(), type);

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = type;

            return circuitReportView;
        }

        public CircuitReportViewModel GetViewModel(string buildId, string energyCode, string circuits,string type, string date)
        {
            DateTime now = Utils.Util.ConvertString2DateTime(date, "yyyy-MM-dd");

            string[] circuitIds = circuits.Split(',');

            List<ReportValue> data = context.GetReportValueList(circuitIds, now.ToShortDateString(), type);

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.Data = data;
            circuitReportView.ReportType = type;

            return circuitReportView;
        }

        //public CircuitReportViewModel GetViewModel(string buildId,string date,string type)
        //{
        //    DateTime today = Utils.Util.ConvertString2DateTime(date,"yyyy-MM-dd");
        //}

        public List<TreeViewModel> GetTreeListViewModel(string buildId, string energyItemCode)
        {
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId,energyItemCode);
            var parentCircuits = circuits.Where(c=>(c.ParentId=="-1"||string.IsNullOrEmpty(c.ParentId)));
            List<TreeViewModel> treeList = new List<TreeViewModel>();

            foreach (var item in parentCircuits)
            {
                TreeViewModel parentNode = new TreeViewModel();
                List<TreeViewModel>  children = GetChildrenNodes(circuits,item);
                parentNode.Id = item.CircuitId;
                parentNode.Text = item.CircuitName;
                if (children.Count != 0)
                    parentNode.Nodes = children;
                treeList.Add(parentNode);
            }

            return treeList;
        }

        List<TreeViewModel> GetChildrenNodes(List<Circuit> circuits, Circuit circuit)
        {
            string parentId = circuit.CircuitId;
            List<TreeViewModel> circuitList = new List<TreeViewModel>();
            var children = circuits.Where(c=>c.ParentId==parentId);

            foreach (var item in children)
            {
                TreeViewModel node = new TreeViewModel();
                node.Id = item.CircuitId;
                node.Text = item.CircuitName;
                if (GetChildrenNodes(circuits, item).Count != 0)
                    node.Nodes = GetChildrenNodes(circuits,item);

                circuitList.Add(node);
            }

            return circuitList;
        }

        string[] GetCircuitIds(List<Circuit> circuits)
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
