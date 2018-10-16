using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class AlarmDeviceFreeTimeService
    {
        private AlarmDeviceFreeTimeDbContext context;

        public AlarmDeviceFreeTimeService()
        {
            context = new AlarmDeviceFreeTimeDbContext();
        }

        /// <summary>
        /// 获取设备用能越限告警（设定时间段内用能超过设置起始时间的前一个小时的值）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AlarmDeviceFreeTimeViewModel GetViewModelByUserName(string userName)
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

            //List<AlarmFreeTime> energyAlarmValue = context.GetOverLimitValueList(buildId, today.ToString("yyyy-MM-dd"));

            AlarmDeviceFreeTimeViewModel viewModel = new AlarmDeviceFreeTimeViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.EnergyAlarmData = GetAlarmValue(buildId, today.ToString("yyyy-MM-dd"));

            return viewModel;
        }

        /// <summary>
        /// 获取设备用能越限告警（设定时间段内用能超过设置起始时间的前一个小时的值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public AlarmDeviceFreeTimeViewModel GetViewModel(string buildId, string date)
        {

            AlarmDeviceFreeTimeViewModel viewModel = new AlarmDeviceFreeTimeViewModel();
            viewModel.EnergyAlarmData = GetAlarmValue(buildId, date);
            return viewModel;
        }

        private List<AlarmFreeTime> GetAlarmValue(string buildId,string date)
        {
            List<AlarmFreeTime> energyAlarmValue = new List<AlarmFreeTime>();

            List<AlarmTempValue> T1 = context.GetOverLimitValueT1List(buildId, date);

            List<AlarmTempValue> T2 = context.GetOverLimitValueT2List(buildId, date);


            foreach (var item in T2)
            {
                List<AlarmTempValue> tempList = T1.FindAll(t => t.ID == item.ID);

                decimal rate;
                if (item.Rate == null)
                    rate = 1;
                else
                    rate = (decimal)item.Rate;

                if (tempList.Count == 0)
                    continue;


                foreach (AlarmTempValue tempValue in tempList)
                {
                    if(tempValue.Value>item.Value)
                        energyAlarmValue.Add(new AlarmFreeTime
                        {
                            ID = item.ID,
                            Name = tempValue.Name,
                            TimePeriod = tempValue.TimePeriod,
                            Time = tempValue.Time,
                            Value = tempValue.Value,
                            LimitValue = item.Value ,
                            DiffValue = tempValue.Value - item.Value
                        });
                }
            }

            return energyAlarmValue;
        }


        /// <summary>
        /// 获取设备用能越限告警设备列表（已设置和未设置的列表）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AlarmDeviceFreeTimeViewModel GetSettingViewModelByUserName(string userName)
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

            AlarmDeviceFreeTimeViewModel viewModel = new AlarmDeviceFreeTimeViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.AlarmLimitValues = alarmLimitValues;
            viewModel.UnSettingDevices = unSettingDevices;

            return viewModel;
        }


        /// <summary>
        /// 获取设备用能越限值列表（已设置和未设置的列表）
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public AlarmDeviceFreeTimeViewModel GetSettingAlarmLimitValueViewModel(string buildId)
        {
            //已设置告警值列表
            List<AlarmLimitValue> alarmLimitValues = context.GetAlarmLimitValueList(buildId);
            //未设置列表
            List<TreeViewInfo> unSettingDevices = context.GetUnSettingDeviceList(buildId);

            AlarmDeviceFreeTimeViewModel viewModel = new AlarmDeviceFreeTimeViewModel();
            viewModel.AlarmLimitValues = alarmLimitValues;
            viewModel.UnSettingDevices = unSettingDevices;

            return viewModel;
        }

        /// <summary>
        /// 获取设备用能越限值列表（未设置告警值设备列表）
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public AlarmDeviceFreeTimeViewModel GetUnsettingLimitValueViewModel(string buildId)
        {
            List<TreeViewInfo> unSettingDevices = context.GetUnSettingDeviceList(buildId);
            AlarmDeviceFreeTimeViewModel viewModel = new AlarmDeviceFreeTimeViewModel();
            viewModel.UnSettingDevices = unSettingDevices;

            return viewModel;
        }

        /// <summary>
        /// 设置设备-用能越限值（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="circuitID"></param>
        /// <param name="startTime"> 起始时间：17:00</param>
        /// <param name="endTime">结束时间：08:00</param>
        /// <param name="isOverDay">是否跨越天 1：跨天 ；其他不夸天</param>
        /// <param name="limitValue"> 设定时间段内用能超过设置起始时间的前一个小时的值的百分率</param>
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
