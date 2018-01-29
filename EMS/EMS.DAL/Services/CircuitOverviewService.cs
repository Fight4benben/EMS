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
    public class CircuitOverviewService
    {
        private ICircuitOverviewDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();

        public CircuitOverviewService()
        {
            context = new CircuitOverviewDbContext();
        }

        public CircuitOverviewViewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            string energyCode = energys.First().EnergyItemCode;
            List<Circuit> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);


            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<CircuitValue> loadData = context.GetCircuitLoadValueList(buildId, circuitIds[0], today.ToString());
            List<CircuitValue> dayData = context.GetCircuitMomDayValueList(buildId, circuitIds[0], today.ToString());
            List<CircuitValue> monthData = context.GetCircuitMomMonthValueList(buildId, circuitIds[0], today.ToString());
            List<CircuitValue> last48HoursData = context.GetCircuit48HoursValueList(buildId, circuitIds[0], today.ToString());
            List<CircuitValue> last31DayData = context.GetCircuit31DaysValueList(buildId, circuitIds[0], today.ToString());
            List<CircuitValue> last12MonthData = context.GetCircuit12MonthValueList(buildId, circuitIds[0], today.ToString());
            List<CircuitValue> last3YearData = context.GetCircuit3YearValueList(buildId, circuitIds[0], today.ToString());

            CircuitOverviewViewModel circuitOverviewView = new CircuitOverviewViewModel();
            circuitOverviewView.Builds = builds;
            circuitOverviewView.Energys = energys;
            circuitOverviewView.TreeView = treeView;
            circuitOverviewView.LoadData = loadData;
            circuitOverviewView.MomDayData = dayData;
            circuitOverviewView.MomMonthData = monthData;
            circuitOverviewView.Last48HoursData = last48HoursData;
            circuitOverviewView.Last31DaysData = last31DayData;
            circuitOverviewView.Last12MonthData = last12MonthData;
            circuitOverviewView.Last3YearData = last3YearData;

            return circuitOverviewView;
        }







        /// <summary>
        /// 根据建筑ID，和能源类型获取树状结构
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyItemCode">能源类型</param>
        /// <returns>树状结构</returns>
        public List<TreeViewModel> GetTreeListViewModel(string buildId, string energyItemCode)
        {
            List<Circuit> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyItemCode);
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
        List<TreeViewModel> GetChildrenNodes(List<Circuit> circuits, Circuit circuit)
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
