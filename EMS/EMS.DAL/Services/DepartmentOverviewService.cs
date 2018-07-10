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
    public class DepartmentOverviewService
    {
        private IDepartmentOverviewDbContext context;
        public DepartmentOverviewService()
        {
            context = new DepartmentOverviewDbContext();
        }

        /// <summary>
        /// 部门用能概况
        /// 初始加载：获取用户名查询建筑列表，部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势</returns>
        public DepartmentOverviewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<DepartmentValue> momDay = context.GetMomDayValueList(buildId, today.ToString());
            List<DepartmentValue> rankByYear = context.GetRankByYearValueList(buildId, today.ToString());
            List<DepartmentValue> planValue = context.GetPlanValueList(buildId, today.ToString());
            List<DepartmentValue> last31DayPieChart = context.GetLast31DayPieChartValueList(buildId, today.ToString());
            List<DepartmentValue> last31Day = context.GetLast31DayValueList(buildId, today.ToString());

            DepartmentOverviewModel OverviewViewModel = new DepartmentOverviewModel();
            OverviewViewModel.Builds = builds;
            OverviewViewModel.MomDay = momDay;
            OverviewViewModel.RankByYear = rankByYear;
            OverviewViewModel.PlanValue = planValue;
            OverviewViewModel.Last31DayPieChart = last31DayPieChart;
            OverviewViewModel.Last31Day = last31Day;

            return OverviewViewModel;
        }

        /// <summary>
        /// 部门用能概况
        /// 根据建筑ID和时间，获取该建筑包含部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回：部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势</returns>
        public DepartmentOverviewModel GetViewModel(string buildId, string date)
        {
            List<DepartmentValue> momDay = context.GetMomDayValueList(buildId, date);
            List<DepartmentValue> rankByYear = context.GetRankByYearValueList(buildId, date);
            List<DepartmentValue> planValue = context.GetPlanValueList(buildId, date);
            List<DepartmentValue> last31DayPieChart = context.GetLast31DayPieChartValueList(buildId, date);
            List<DepartmentValue> last31Day = context.GetLast31DayValueList(buildId, date);

            DepartmentOverviewModel OverviewViewModel = new DepartmentOverviewModel();
            OverviewViewModel.MomDay = momDay;
            OverviewViewModel.RankByYear = rankByYear;
            OverviewViewModel.PlanValue = planValue;
            OverviewViewModel.Last31DayPieChart = last31DayPieChart;
            OverviewViewModel.Last31Day = last31Day;

            return OverviewViewModel;
        }
    }
}
