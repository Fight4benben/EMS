using EMS.DAL.Entities;
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
    public class HistoryParamService
    {
        private HistoryParamDbContext context;
        public HistoryParamService()
        {
            context = new HistoryParamDbContext();
        }

        /// <summary>
        /// 参数查询
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑的能耗分类，第一个能耗分类包含的支路列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>：包含建筑列表，能源按钮列表，支路列表树状结构</returns>
        public HistoryParamViewModel GetViewModelByUserName(string userName)
        {
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId;
            if (builds.Count > 0)
                buildId = builds.First().BuildID;
            else
                buildId = "";

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Builds = builds;
            viewMode.Energys = energys;
            viewMode.TreeView = treeViewModel;

            return viewMode;
        }

        public HistoryParamViewModel GetViewModelByBuild(string userName,string buildId)
        {
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Builds = builds;
            viewMode.Energys = energys;
            viewMode.TreeView = treeViewModel;

            return viewMode;
        }

        public HistoryParamViewModel GetViewModel(string buildId)
        {
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Energys = energys;
            viewMode.TreeView = treeViewModel;

            return viewMode;
        }

        public HistoryParamViewModel GetViewModel(string buildId, string energyCode)
        {
            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.TreeView = treeViewModel;

            return viewMode;
        }

        /// <summary>
        /// 获取支路包含的所有参数
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="circuitID"></param>
        /// <returns></returns>
        public HistoryParamViewModel GetViewModel(string buildId, string energyCode, string circuitID)
        {
            //List<ParamClassify> paramClassifyList = context.GetMeterParamClassify(buildId, circuitID);
            List<MeterParam> meterParamList = context.GetMeterParam(buildId, circuitID);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.MeterParam = meterParamList;

            return viewMode;
        }

        /// <summary>
        /// 获取单个支路中的多个参数的值
        /// </summary>
        /// <param name="circuitID"></param>
        /// <param name="meterParamIDs"></param>
        /// <param name="startTime"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public HistoryParamViewModel GetViewModel(string circuitID, string[] meterParamIDs, string startTime, int step)
        {
            switch (step)
            {
                case 5:
                    step = 5;
                    break;
                case 10:
                    step = 10;
                    break;
                case 15:
                    step = 15;
                    break;
                case 30:
                    step = 30;
                    break;
                case 60:
                    step = 60;
                    break;
                default:
                    step = 5;
                    break;
            }

            List<HistoryParameterValue> parameterValue = context.GetParamValue(circuitID, meterParamIDs, startTime, step);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Data = parameterValue;

            return viewMode;
        }
    }
}
