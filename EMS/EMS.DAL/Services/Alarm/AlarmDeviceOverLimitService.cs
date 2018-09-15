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
    public class AlarmDeviceOverLimitService
    {
        private AlarmDeviceOverLimitDbContext context;

        public AlarmDeviceOverLimitService()
        {
            context = new AlarmDeviceOverLimitDbContext();
        }

        /// <summary>
        /// 获取设备用能越限告警（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AlarmDeviceOverLimitViewModel GetViewModelByUserName(string userName)
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
            //List<AlarmLimitValue> alarmLimitValues = context.GetAlarmLimitValueList(buildId);

            //List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);

            //List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            List<EnergyAlarm> energyAlarmValue = context.GetEnergyOverLimitValueList(buildId, today.ToString("yyyy-MM-dd"));

            AlarmDeviceOverLimitViewModel viewModel = new AlarmDeviceOverLimitViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            //viewModel.AlarmLimitValues = alarmLimitValues;
            viewModel.EnergyAlarmData = energyAlarmValue;

            return viewModel;
        }

        /// <summary>
        /// 获取设备用能越限告警（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public AlarmDeviceOverLimitViewModel GetViewModel(string buildId, string date)
        {
            List<EnergyAlarm> energyAlarmValue = context.GetEnergyOverLimitValueList(buildId, date);

            AlarmDeviceOverLimitViewModel viewModel = new AlarmDeviceOverLimitViewModel();
            viewModel.EnergyAlarmData = energyAlarmValue;
            return viewModel;
        }


        /// <summary>
        /// 获取设备用能越限告警设备列表（已设置告警值设备列表）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AlarmDeviceOverLimitViewModel GetSettingViewModelByUserName(string userName)
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
            //已设置告警值列表
            List<AlarmLimitValue> alarmLimitValues = context.GetAlarmLimitValueList(buildId);
            //未设置列表
            List<TreeViewInfo> unSettingDevices = context.GetUnSettingDeviceList(buildId);

            AlarmDeviceOverLimitViewModel viewModel = new AlarmDeviceOverLimitViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.AlarmLimitValues = alarmLimitValues;
            viewModel.UnSettingDevices = unSettingDevices;

            return viewModel;
        }



        /// <summary>
        /// 获取设备用能越限值列表（已设置告警值设备列表）
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public AlarmDeviceOverLimitViewModel GetAlarmLimitValueViewModel(string buildId)
        {
            //已设置告警值列表
            List<AlarmLimitValue> alarmLimitValues = context.GetAlarmLimitValueList(buildId);
            //未设置列表
            List<TreeViewInfo> unSettingDevices = context.GetUnSettingDeviceList(buildId);

            AlarmDeviceOverLimitViewModel viewModel = new AlarmDeviceOverLimitViewModel();
            viewModel.AlarmLimitValues = alarmLimitValues;
            viewModel.UnSettingDevices = unSettingDevices;

            return viewModel;
        }

        /// <summary>
        /// 获取设备用能越限值列表（未设置告警值设备列表）
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public AlarmDeviceOverLimitViewModel GetUnsettingLimitValueViewModel(string buildId)
        {
            List<TreeViewInfo> unSettingDevices = context.GetUnSettingDeviceList(buildId);
            AlarmDeviceOverLimitViewModel viewModel = new AlarmDeviceOverLimitViewModel();
            viewModel.UnSettingDevices = unSettingDevices;

            return viewModel;
        }



        /// <summary>
        /// 设置设备-用能越限值（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="departmentID"></param>
        /// <param name="startTime"> 起始时间：17:00</param>
        /// <param name="endTime">结束时间：08:00</param>
        /// <param name="isOverDay">是否跨越天 1：跨天 ；其他不夸天</param>
        /// <param name="limitValue"> 报警阈值</param>
        /// <returns></returns>
        public int SetDeviceOverLimitValue(string buildId, string circuitID, string startTime, string endTime, int isOverDay, decimal limitValue)
        {
            int result = context.SetDeviceOverLimitValue(buildId, circuitID, startTime, endTime, isOverDay, limitValue);

            return result;
        }

        /// <summary>
        /// 删除设备-用能越限值
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public int DeleteDeviceOverLimitValue(string buildId, string circuitID)
        {
            int result = context.DeleteDeviceOverLimitValue(buildId, circuitID);

            return result;
        }

    }
}
