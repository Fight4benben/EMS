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
            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            List<AlarmLimitValue> alarmLimitValues = context.GetAlarmLimitValueList(buildId);
            List<EnergyAlarm> deptAlarmValue = context.GetDeptOverLimitValueList(buildId, energyCode, today.ToString("yyyy-MM-dd"));

            AlarmDepartmentOverLimitViewModel viewModel = new AlarmDepartmentOverLimitViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.TreeView = treeViewModel;
            viewModel.AlarmLimitValues = alarmLimitValues;
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

        /// <summary>
        /// 获取部门用能越限值列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public AlarmDepartmentOverLimitViewModel GetAlarmLimitValueViewModel(string buildId)
        {
            List<AlarmLimitValue> alarmLimitValues = context.GetAlarmLimitValueList(buildId);
            AlarmDepartmentOverLimitViewModel viewModel = new AlarmDepartmentOverLimitViewModel();
            viewModel.AlarmLimitValues = alarmLimitValues;

            return viewModel;
        }

        /// <summary>
        /// 设置部门用能越限值（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="departmentID"></param>
        /// <param name="startTime"> 起始时间：17:00</param>
        /// <param name="endTime">结束时间：08:00</param>
        /// <param name="isOverDay">是否跨越天 1：跨天 ；其他不夸天</param>
        /// <param name="limitValue"> 报警阈值</param>
        /// <returns></returns>
        public int SetDeptOverLimitValue(string buildId, string energyCode, string departmentID, string startTime, string endTime, int isOverDay, decimal limitValue)
        {
            int result = context.SetDeptOverLimitValue(buildId, energyCode, departmentID, startTime, endTime, isOverDay, limitValue);

            return result;
        }

        /// <summary>
        /// 删除部门用能越限值
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public int DeleteDeptOverLimitValue(string buildId, string energyCode, string departmentID)
        {
            int result = context.DeleteDeptOverLimitValue(buildId, energyCode, departmentID);

            return result;
        }

    }
}
