using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class EnergyItemStatisticService
    {
        private EnergyItemStatisticDbContext context;

        public EnergyItemStatisticService()
        {
            context = new EnergyItemStatisticDbContext();
        }

        /// <summary>
        /// 分类用能统计
        /// 初始加载：根据用户名查询建筑列表，分类能耗数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回：当月计划用能值，当年计划用能值，当月实际用能值，当年实际用能值，分类能耗单位</returns>
        public EnergyItemStatisticViewModel GetViewModelByUserName(string userName)
        {
            EnergyItemStatisticViewModel viewModel = new EnergyItemStatisticViewModel();
            DateTime today = DateTime.Now;
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);

            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            List<ReportValue> monthPlanValue = context.GetMonthPlanValueList(buildId, today.ToString());
            List<ReportValue> yearPlanValue = context.GetYearPlanValueList(buildId, today.ToString());
            List<ReportValue> monthRealValue = context.GetMonthRealValueList(buildId, today.ToString());
            List<ReportValue> yearRealValue = context.GetYearRealValueList(buildId, today.ToString());

            viewModel.MonthPlanData = monthPlanValue;
            viewModel.YearPlanData = yearPlanValue;
            viewModel.MonthRealData = monthRealValue;
            viewModel.YearRealData = yearRealValue;
            viewModel.EnergyUnit = energys;

            return viewModel;
        }

        /// <summary>
        /// 分类用能统计
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns>返回：当月计划用能值，当年计划用能值，当月实际用能值，当年实际用能值，分类能耗单位</returns>
        public EnergyItemStatisticViewModel GetViewModel(string buildId, string date)
        {
            EnergyItemStatisticViewModel viewModel = new EnergyItemStatisticViewModel();
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            List<ReportValue> monthPlanValue = context.GetMonthPlanValueList(buildId, date);
            List<ReportValue> yearPlanValue = context.GetYearPlanValueList(buildId, date);
            List<ReportValue> monthRealValue = context.GetMonthRealValueList(buildId, date);
            List<ReportValue> yearRealValue = context.GetYearRealValueList(buildId, date);

            viewModel.MonthPlanData = monthPlanValue;
            viewModel.YearPlanData = yearPlanValue;
            viewModel.MonthRealData = monthRealValue;
            viewModel.YearRealData = yearRealValue;
            viewModel.EnergyUnit = energys;

            return viewModel;
        }
    }
}
