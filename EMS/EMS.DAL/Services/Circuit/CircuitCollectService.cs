using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.Utils;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class CircuitCollectService
    {
        private HistoryDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();
        public CircuitCollectService()
        {
            context = new HistoryDbContext();
        }

        public CircuitCollectViewModel GetViewModelByUserName(string userName)
        {
            CircuitCollectViewModel viewModel = new CircuitCollectViewModel();
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.Date;
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);

            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<CircuitList> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);
            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.TreeView = treeView;

            return viewModel;
        }

        public CircuitCollectViewModel GetViewModelByBuild(string userName, string buildId)
        {
            CircuitCollectViewModel viewModel = new CircuitCollectViewModel();
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.Date;
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<CircuitList> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);
            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.TreeView = treeView;

            return viewModel;
        }

        public CircuitCollectViewModel GetViewModel(string buildId)
        {
            CircuitCollectViewModel viewModel = new CircuitCollectViewModel();

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            viewModel.Energys = energys;
            viewModel.TreeView = treeView;

            return viewModel;
        }


        public CircuitCollectViewModel GetViewModel(string buildId, string energyCode)
        {
            CircuitCollectViewModel viewModel = new CircuitCollectViewModel();
            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            viewModel.TreeView = treeView;

            return viewModel;
        }

        public CircuitCollectViewModel GetViewModel(string buildId, string energyCode, string[] circuitIDs, string startDate, string endDate)
        {
            CircuitCollectViewModel viewModel = new CircuitCollectViewModel();
            DateTime startTime = Util.ConvertString2DateTime(startDate, "yyyy-MM-dd HH:mm:ss");
            DateTime endTime = Util.ConvertString2DateTime(endDate, "yyyy-MM-dd HH:mm:ss");

            List<CircuitMeterInfo> circuitMeterInfos = context.GetCircuitMeterInfoList(buildId, circuitIDs);
            string[] meterIDs = GetMeterIDs(circuitMeterInfos);
            string[] meterParamIDs = GetMeterParamIDs(circuitMeterInfos);
            List<HistoryValue> startValue = context.GetHistoryValues(meterIDs, meterParamIDs, startTime);
            List<HistoryValue> endValue = context.GetHistoryValues(meterIDs, meterParamIDs, endTime);
            List<CollectValue> data = new List<CollectValue>();
            foreach (var meterID in startValue)
            {
                CollectValue collect = new CollectValue();
                collect.Name = circuitMeterInfos.Find(x => x.MeterID.Equals(meterID.MeterID)).CircuitName;
                collect.StartValue = meterID.Value;
                collect.EndValue = endValue.Find(x => x.MeterID.Equals(meterID.MeterID)).Value;
                collect.DiffValue = collect.EndValue - collect.StartValue;

                data.Add(collect);
            }
            viewModel.Data = data;

            return viewModel;
        }

        public CircuitCollectViewModel GetEPEViewModel(string buildId, string energyCode, string[] circuitIDs, string startDate, string endDate)
        {
            CircuitCollectViewModel viewModel = new CircuitCollectViewModel();
            DateTime startTime = Util.ConvertString2DateTime(startDate, "yyyy-MM-dd HH:mm:ss");
            DateTime endTime = Util.ConvertString2DateTime(endDate, "yyyy-MM-dd HH:mm:ss");

            List<CircuitMeterInfo> circuitMeterInfos = context.GetCircuitMeterInfoListEPE(buildId, circuitIDs);
            string[] meterIDs = GetMeterIDs(circuitMeterInfos);
            string[] meterParamIDs = GetMeterParamIDs(circuitMeterInfos);
            List<HistoryValue> startValue = context.GetHistoryValues(meterIDs, meterParamIDs, startTime);
            List<HistoryValue> endValue = context.GetHistoryValues(meterIDs, meterParamIDs, endTime);
            List<CollectValue> data = new List<CollectValue>();
            foreach (var meterID in startValue)
            {
                CollectValue collect = new CollectValue();
                collect.Name = circuitMeterInfos.Find(x => x.MeterID.Equals(meterID.MeterID)).CircuitName;
                collect.StartValue = meterID.Value;
                collect.EndValue = endValue.Find(x => x.MeterID.Equals(meterID.MeterID)).Value;
                collect.DiffValue = collect.EndValue - collect.StartValue;

                data.Add(collect);
            }
            viewModel.Data = data;

            return viewModel;
        }

        public CircuitCollectViewModel GetMultiRateViewModel(string buildId, string energyCode, string[] circuitIDs, string startDate, string endDate)
        {
            CircuitCollectViewModel viewModel = new CircuitCollectViewModel();
            DateTime startTime = Util.ConvertString2DateTime(startDate, "yyyy-MM-dd HH:mm:ss");
            DateTime endTime = Util.ConvertString2DateTime(endDate, "yyyy-MM-dd HH:mm:ss");

            List<CircuitMeterInfo> circuitMeterInfos = context.GetMultiRateMeterInfoList(buildId, circuitIDs);
            string[] meterIDs = GetMeterIDs(circuitMeterInfos);
            string[] meterParamIDs = GetMeterParamIDs(circuitMeterInfos);
            List<HistoryValue> startValue = context.GetHistoryValues(meterIDs, meterParamIDs, startTime);
            List<HistoryValue> endValue = context.GetHistoryValues(meterIDs, meterParamIDs, endTime);
            List<CollectValue> data = new List<CollectValue>();
            foreach (var meterID in startValue)
            {
                CollectValue collect = new CollectValue();
                collect.Name = circuitMeterInfos.Find(x => x.MeterID.Equals(meterID.MeterID)).CircuitName;
                collect.ParamName = circuitMeterInfos.Find(x => x.MeterID.Equals(meterID.MeterID) && x.MeterParamID.Equals(meterID.MeterParamID)).MeterParamName;
                collect.StartValue = meterID.Value;
                collect.EndValue = endValue.Find(x => x.MeterID.Equals(meterID.MeterID) && x.MeterParamID.Equals(meterID.MeterParamID)).Value;
                collect.DiffValue = collect.EndValue - collect.StartValue;

                data.Add(collect);
            }
            viewModel.Data = data;

            return viewModel;
        }




        /*-------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// 根据建筑ID，和能源类型获取树状结构
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyItemCode">能源类型</param>
        /// <returns>树状结构</returns>
        public List<TreeViewModel> GetTreeListViewModel(string buildId, string energyItemCode)
        {
            List<CircuitList> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyItemCode);
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
        List<TreeViewModel> GetChildrenNodes(List<CircuitList> circuits, CircuitList circuit)
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

        string[] GetCircuitIds(List<CircuitList> circuits)
        {
            List<string> list = new List<string>();
            foreach (var circuit in circuits)
            {
                list.Add(circuit.CircuitId);
            }

            return list.ToArray();
        }

        string[] GetMeterIDs(List<CircuitMeterInfo> circuitMeterInfos)
        {
            List<string> list = new List<string>();
            foreach (var meter in circuitMeterInfos)
            {
                list.Add(meter.MeterID);
            }

            return list.ToArray();
        }

        string[] GetMeterParamIDs(List<CircuitMeterInfo> circuitMeterInfos)
        {
            List<string> list = new List<string>();
            foreach (var meter in circuitMeterInfos)
            {
                list.Add(meter.MeterParamID);
            }

            return list.ToArray();
        }


    }
}
