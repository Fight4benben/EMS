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
    public class AlarmDepartmentComletionRateService
    {
        private AlarmDepartmentComletionRateDbContext context;

        public AlarmDepartmentComletionRateService()
        {
            context = new AlarmDepartmentComletionRateDbContext();
        }

        /// <summary>
        /// 部门-月度用能总量,单位面积 同比大于 -2%（下降小于2%）
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，能源按钮列表，月度用能总量,单位面积 同比大于 -2%</returns>
        public AlarmDepartmentComletionRateViewModel GetViewModelByUserName(string userName)
        {
            DateTime today = DateTime.Now;
            string startDay = today.ToString("yyyy-MM-01");
            string endDay = today.ToString("yyyy-MM-dd");

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

            List<DeptCompletionRate> deptCompletionRate = context.GetDeptComletionRateList(buildId);

            List<CompareData> totalCompareData = context.GetDeptTotalValueCompareMonthList(buildId, energyCode, startDay, endDay);
            List<CompareData> areaAvgCompareData = context.GetDeptAreaAvgCompareMonthList(buildId, energyCode, startDay, endDay);


            AlarmDepartmentComletionRateViewModel viewModel = new AlarmDepartmentComletionRateViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.DeptCompletionRate = deptCompletionRate;
            viewModel.TotalCompareData = totalCompareData;
            viewModel.AreaAvgCompareData = areaAvgCompareData;

            return viewModel;
        }

        /// <summary>
        /// 部门-部门用能总量,单位面积 同比大于 -2%（下降小于2%）
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗</param>
        /// <param name="type">类型：MM：月度； QQ：季度； YY：年度 </param>
        /// <param name="date">传入时间格式：月度：yyyy-MM (具体到月份) 
        ///                                季度：yyyy-MM (具体到月份，每个季度最后一个月) 
        ///                                年度：yyyy-MM (具体到月份) 
        /// </param>
        /// <returns></returns>
        public AlarmDepartmentComletionRateViewModel GetViewModel(string buildId, string energyCode, string type, string date)
        {
            DateTime dateTime;
            string startDay, endDay;

            List<CompareData> totalCompareData = new List<CompareData>();
            List<CompareData> areaAvgCompareData = new List<CompareData>();

            switch (type)
            {
                case "MM":
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd");
                    startDay = dateTime.ToString("yyyy-MM-01");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    totalCompareData = context.GetDeptTotalValueCompareMonthList(buildId, energyCode, startDay, endDay);
                    areaAvgCompareData = context.GetDeptAreaAvgCompareMonthList(buildId, energyCode, startDay, endDay);
                    break;

                case "QQ":
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM");
                    startDay = dateTime.AddDays(-dateTime.Day + 1).AddMonths(-2).ToString("yyyy-MM-dd");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    totalCompareData = context.GetDeptTotalValueCompareQuarterList(buildId, energyCode, startDay, endDay);
                    areaAvgCompareData = context.GetDeptAreaAvgCompareQuarterList(buildId, energyCode, startDay, endDay);
                    break;

                case "YY":
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd");
                    startDay = dateTime.ToString("yyyy-01-01");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    totalCompareData = context.GetDeptTotalValueCompareYearList(buildId, energyCode, startDay, endDay);
                    areaAvgCompareData = context.GetDeptAreaAvgCompareYearList(buildId, energyCode, startDay, endDay);
                    break;

                default:
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd");
                    startDay = dateTime.ToString("yyyy-MM-01");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    totalCompareData = context.GetDeptTotalValueCompareMonthList(buildId, energyCode, startDay, endDay);
                    areaAvgCompareData = context.GetDeptAreaAvgCompareMonthList(buildId, energyCode, startDay, endDay);
                    break;
            }

            List<DeptCompletionRate> deptCompletionRate = context.GetDeptComletionRateList(buildId);
            AlarmDepartmentComletionRateViewModel viewModel = new AlarmDepartmentComletionRateViewModel();
            viewModel.DeptCompletionRate = deptCompletionRate;
            viewModel.TotalCompareData = totalCompareData;
            viewModel.AreaAvgCompareData = areaAvgCompareData;


            return viewModel;
        }

    }
}
