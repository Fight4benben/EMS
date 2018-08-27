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
    public class EnergyAlarmService
    {
        private EnergyAlarmDbContext context;

        public EnergyAlarmService()
        {
            context = new EnergyAlarmDbContext();
        }

        public EnergyAlarmViewModel GetViewModelByUserName(string userName)
        {
            DateTime today = DateTime.Now;

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

            List<EnergyAlarm> energyAlarmValue = context.GetEnergyOverLimitValueList(buildId, today.ToString("yyyy-MM-dd"));

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.TreeView = treeViewModel;
            viewModel.EnergyAlarmData = energyAlarmValue;

            return viewModel;
        }

        /// <summary>
        /// 获取设备用你越限告警
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public EnergyAlarmViewModel GetViewModel(string buildId, string date)
        {
            List<EnergyAlarm> energyAlarmValue = context.GetEnergyOverLimitValueList(buildId, date);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.EnergyAlarmData = energyAlarmValue;
            return viewModel;
        }
        /// <summary>
        /// 获取设备用能 天环比数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public EnergyAlarmViewModel GetCompareDayViewModel(string buildId, string date)
        {
            List<CompareData> compareDatas = context.GetDayCompareValueList(buildId, date);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.CompareData = compareDatas;
            return viewModel;
        }

        /// <summary>
        /// 获取设备用能 月份同比
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public EnergyAlarmViewModel GetCompareMonthViewModel(string buildId, string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            string startDay = dateTime.AddDays(-dateTime.Day+1).ToString("yyyy-MM-dd");
            string endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
            List<CompareData> compareDatas = context.GetMonthCompareValueList(buildId,startDay,endDay);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.CompareData = compareDatas;
            return viewModel;
        }
    }
}
