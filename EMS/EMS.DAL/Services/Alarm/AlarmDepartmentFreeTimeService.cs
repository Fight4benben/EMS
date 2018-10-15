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
    public class AlarmDepartmentFreeTimeService
    {
        private AlarmDepartmentFreeTimeDbContext context;

        public AlarmDepartmentFreeTimeService()
        {
            context = new AlarmDepartmentFreeTimeDbContext();
        }

        /// <summary>
        /// 获取部门用能告警-（非工作时间用能告警）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AlarmDepartmentFreeTimeViewModel GetViewModelByUserName(string userName)
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
          
            //List<AlarmFreeTime> deptAlarmValue = context.GetDeptOverLimitValueList(buildId, energyCode, today.ToString("yyyy-MM-dd"));

            AlarmDepartmentFreeTimeViewModel viewModel = new AlarmDepartmentFreeTimeViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.EnergyAlarmData = GetAlarmValue(buildId, energyCode, today.ToString("yyyy-MM-dd"));

            return viewModel;
        }

        /// <summary>
        /// 部门用能越限告警（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode">分类代码</param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public AlarmDepartmentFreeTimeViewModel GetViewModel(string buildId, string energyCode, string date)
        {

            AlarmDepartmentFreeTimeViewModel viewModel = new AlarmDepartmentFreeTimeViewModel();
            viewModel.EnergyAlarmData = GetAlarmValue(buildId, energyCode, date);

            return viewModel;
        }

        private List<AlarmFreeTime> GetAlarmValue(string buildId, string energyCode, string date)
        {
            List<AlarmFreeTime> energyAlarmValue = new List<AlarmFreeTime>();

            List<AlarmTempValue> T1 = context.GetOverLimitValueT1List(buildId, energyCode, date);

            List<AlarmTempValue> T2 = context.GetOverLimitValueT2List(buildId, energyCode, date);


            foreach (var item in T2)
            {
                List<AlarmTempValue> tempList = T1.FindAll(t => t.ID == item.ID);

                if (tempList.Count == 0)
                    continue;


                foreach (AlarmTempValue tempValue in tempList)
                {
                    if (tempValue.Value > item.Value)
                        energyAlarmValue.Add(new AlarmFreeTime
                        {
                            ID = item.ID,
                            Name = tempValue.Name,
                            TimePeriod = tempValue.TimePeriod,
                            Time = tempValue.Time,
                            Value = tempValue.Value,
                            LimitValue = item.Value,
                            DiffValue = tempValue.Value - item.Value
                        });
                }
            }

            return energyAlarmValue;
        }
    }
}
