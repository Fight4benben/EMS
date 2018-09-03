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
    public class AlarmDepartmentOverLimitService
    {
        private AlarmDepartmentOverLimitDbContext context;

        public AlarmDepartmentOverLimitService()
        {
            context = new AlarmDepartmentOverLimitDbContext();
        }

        /// <summary>
        /// 获取部门用能告警-（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AlarmDepartmentOverLimitViewModel GetViewModelByUserName(string userName)
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

            List<EnergyAlarm> deptAlarmValue = context.GetDeptOverLimitValueList(buildId, energyCode, today.ToString("yyyy-MM-dd"));

            AlarmDepartmentOverLimitViewModel viewModel = new AlarmDepartmentOverLimitViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.EnergyAlarmData = deptAlarmValue;

            return viewModel;
        }

        /// <summary>
        /// 部门用能越限告警（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode">分类代码</param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public AlarmDepartmentOverLimitViewModel GetViewModel(string buildId, string energyCode, string date)
        {
            List<EnergyAlarm> deptAlarmValue = context.GetDeptOverLimitValueList(buildId, energyCode, date);

            AlarmDepartmentOverLimitViewModel viewModel = new AlarmDepartmentOverLimitViewModel();
            viewModel.EnergyAlarmData = deptAlarmValue;
            return viewModel;
        }

    }
}
