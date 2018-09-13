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

        /// <summary>
        /// 获取设备用能越限告警（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
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

            //List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);

            //List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            List<EnergyAlarm> energyAlarmValue = context.GetEnergyOverLimitValueList(buildId, today.ToString("yyyy-MM-dd"));

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            //viewModel.TreeView = treeViewModel;
            viewModel.EnergyAlarmData = energyAlarmValue;

            return viewModel;
        }

        /// <summary>
        /// 获取设备用能越限告警（每天设定时间段内用能超过设定阈值）
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
        /// 仪表的环比数据，当日与昨日，当月与去年同期，今年上一季度与去年同期
        /// 默认昨天与前天的数据
        /// </summary>
        /// <returns></returns>
        public EnergyAlarmViewModel GetDeviceMomData(string userName)
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

            List<CompareData> compareDatas = context.GetDayMomValueList(buildId, today.AddDays(-1).ToString("yyyy-MM-dd"));

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.CompareData = compareDatas;
            return viewModel;


        }
        /// <summary>
        /// 获取设备用能 天环比数据 大于20%
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public EnergyAlarmViewModel GetMomDayViewModel(string buildId, string date)
        {
            List<CompareData> compareDatas = context.GetDayMomValueList(buildId, date);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.CompareData = compareDatas;
            return viewModel;
        }

        /// <summary>
        /// 设备用能 月份环比 大于20%
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public EnergyAlarmViewModel GetMomMonthViewModel(string buildId, string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            string startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
            string endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
            List<CompareData> compareDatas = context.GetMonthMomValueList(buildId, startDay, endDay);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.CompareData = compareDatas;
            return viewModel;
        }

        /// <summary>
        /// 设备用能 月份同比 大于20%
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

        /// <summary>
        /// 部门用能 月份环比大于20%
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public EnergyAlarmViewModel GetMomDeptMonthViewModel(string buildId, string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            string startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
            string endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
            List<CompareData> compareDatas = context.GetDeptMomValueList(buildId, startDay, endDay);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.CompareData = compareDatas;
            return viewModel;
        }

        /// <summary>
        /// 部门用能 月份同比大于20%
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public EnergyAlarmViewModel GetCompareDeptMonthViewModel(string buildId, string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            string startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
            string endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
            List<CompareData> compareDatas = context.GetDeptCompareValueList(buildId, startDay, endDay);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.CompareData = compareDatas;
            return viewModel;
        }
    }
}
