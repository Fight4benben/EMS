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
    public class THParamService
    {
        //使用历史参数查询的DBContext
        private HistoryParamDbContext context;
        public THParamService()
        {
            context = new HistoryParamDbContext();
        }
        /// <summary>
        /// 温湿度参数查询
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑的84000分类包含的支路列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>：包含建筑列表，支路列表</returns>
        public HistoryParamViewModel GetViewModelByUserName(string userName)
        {
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId;
            if (builds.Count > 0)
                buildId = builds.First().BuildID;
            else
                buildId = "";

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            List<EnergyItemDict> eCode = energys.FindAll(x => x.EnergyItemCode.Equals("84000"));
            string energyCode;
            if (eCode.Count > 0)
            {
                energyCode = eCode.First().EnergyItemCode;
            }
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, "84000");
            //List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Builds = builds;
            viewMode.TreeList = treeViewInfos;

            return viewMode;
        }

        public HistoryParamViewModel GetViewModelByBuild(string userName,string buildId)
        {
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            List<EnergyItemDict> eCode = energys.FindAll(x => x.EnergyItemCode.Equals("84000"));
            string energyCode;
            if (eCode.Count > 0)
            {
                energyCode = eCode.First().EnergyItemCode;
            }
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, "84000");
            //List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Builds = builds;
            viewMode.TreeList = treeViewInfos;

            return viewMode;
        }

        public HistoryParamViewModel GetViewModel(string buildId)
        {
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            List<EnergyItemDict> eCode = energys.FindAll(x => x.EnergyItemCode.Equals("84000"));
            string energyCode;
            if (eCode.Count > 0)
            {
                energyCode = eCode.First().EnergyItemCode;
            }
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, "84000");
            //List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.TreeList = treeViewInfos;

            return viewMode;
        }

        /// <summary>
        /// 获取支路包含的所有参数
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="circuitID"></param>
        /// <returns></returns>
        public HistoryParamViewModel GetViewModel(string buildId, string circuitID)
        {
            List<MeterParam> meterParamList = context.GetMeterParam(buildId, circuitID);
            string startTime = DateTime.Now.ToString("yyyy-MM-dd");

            //获取仪表参数ID，并传入GetParamValue中获取其数据
            List<string> paramIDs = new List<string>(); ;
            foreach (var pID in meterParamList)
            {
                paramIDs.Add(pID.ParamID);
            }

            List<HistoryParameterValue> parameterValue = context.GetParamValue(circuitID, paramIDs.ToArray(), startTime, 5);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            //viewMode.MeterParam = meterParamList;
            viewMode.Data = parameterValue;

            return viewMode;
        }

        public HistoryParamViewModel GetViewModel(string buildId, string circuitID,string startTime)
        {
            List<MeterParam> meterParamList = context.GetMeterParam(buildId, circuitID);

            //获取仪表参数ID，并传入GetParamValue中获取其数据
            List<string> paramIDs = new List<string>(); ;
            foreach (var pID in meterParamList)
            {
                paramIDs.Add(pID.ParamID);
            }

            List<HistoryParameterValue> parameterValue = context.GetParamValue(circuitID, paramIDs.ToArray(), startTime, 5);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Data = parameterValue;

            return viewMode;
        }

        public HistoryParamViewModel GetViewModel(string buildId, string circuitID, string startTime,int step)
        {
            List<MeterParam> meterParamList = context.GetMeterParam(buildId, circuitID);

            //获取仪表参数ID，并传入GetParamValue中获取其数据
            List<string> paramIDs = new List<string>(); ;
            foreach (var pID in meterParamList)
            {
                paramIDs.Add(pID.ParamID);
            }

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
            List<HistoryParameterValue> parameterValue = context.GetParamValue(circuitID, paramIDs.ToArray(), startTime, step);

            HistoryParamViewModel viewMode = new HistoryParamViewModel();
            viewMode.Data = parameterValue;

            return viewMode;
        }
    }
}
