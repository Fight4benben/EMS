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
    public class AlarmDeviceService
    {
        private AlarmDeviceDbContext context;

        public AlarmDeviceService()
        {
            context = new AlarmDeviceDbContext();
        }

        /// <summary>
        /// 默认获取 建筑ID，分类能耗，建筑报警等级，设备用能越限-天环比
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>建筑ID，分类能耗，建筑报警等级，设备用能越限-天环比</returns>
        public AlarmDeviceViewModel GetViewModelByUserName(string userName)
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
            List<BuildAlarmLevel> buildAlarmLevels = context.GetBuildAlarmLevelValueList(buildId);

            List<CompareData> deviceAlarmValue = context.GetMomDayValueList(buildId, energyCode, today.ToString("yyyy-MM-dd"));

            AlarmDeviceViewModel viewModel = new AlarmDeviceViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.BuildAlarmLevels = buildAlarmLevels;
            viewModel.CompareDatas = deviceAlarmValue;

            return viewModel;
        }

        /// <summary>
        /// 获取设备告警-天环比
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类代码</param>
        /// <param name="date">结束时间（yyyy-MM-dd HH:00） </param>
        /// <returns></returns>
        public AlarmDeviceViewModel GetMomDayViewModel(string buildId, string energyCode, string date)
        {

            List<BuildAlarmLevel> buildAlarmLevels = context.GetBuildAlarmLevelValueList(buildId);

            List<CompareData> deviceAlarmValue = context.GetMomDayValueList(buildId, energyCode, date);

            AlarmDeviceViewModel viewModel = new AlarmDeviceViewModel();

            viewModel.BuildAlarmLevels = buildAlarmLevels;
            viewModel.CompareDatas = deviceAlarmValue;

            return viewModel;
        }

        /// <summary>
        /// 获取设备告警
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类代码</param>
        /// <param name="type">数据类型: "DD"：日环比； "MM"：月同比； "SS":季度 </param>
        /// <param name="date">结束时间（yyyy-MM-dd HH:00） </param>
        /// <returns></returns>
        public AlarmDeviceViewModel GetViewModel(string buildId, string energyCode,string type, string date)
        {

            DateTime dateTime;
            string startDay, endDay;
            List<BuildAlarmLevel> buildAlarmLevels = context.GetBuildAlarmLevelValueList(buildId);
            List<CompareData> deviceAlarmValue = new List<CompareData>();

            switch (type)
            {
                case "DD":
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd HH:mm");
                    startDay = dateTime.ToString("yyyy-MM-dd")+" 23:00";
                    deviceAlarmValue = context.GetMomDayValueList(buildId, energyCode, startDay);
                    break;

                case "MM":
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd");
                    startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    deviceAlarmValue = context.GetCompareMonthValueList(buildId, energyCode, startDay, endDay);
                    break;

                case "SS":
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM");
                    startDay = dateTime.AddDays(-dateTime.Day + 1).AddMonths(-2).ToString("yyyy-MM-dd");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    deviceAlarmValue = context.GetCompareQuarterValueList(buildId, energyCode, startDay, endDay);
                    break;

                default:
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd HH:mm");
                    startDay = dateTime.ToString("yyyy-MM-dd HH:mm");
                    deviceAlarmValue = context.GetMomDayValueList(buildId, energyCode, startDay);
                    break;
            }

            AlarmDeviceViewModel viewModel = new AlarmDeviceViewModel();
            viewModel.BuildAlarmLevels = buildAlarmLevels;
            viewModel.CompareDatas = deviceAlarmValue;

            return viewModel;
        }

        public int SetDeviceBuildAlarmLevel(string buildId, string energyCode, decimal level1, decimal level2)
        {
            int result = context.SetBuildAlarmLevel(buildId, energyCode, level1, level2);
            return result;
        }
    }
}
