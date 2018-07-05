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

        public DepartmentOverviewModel GetDepartmentOverviewViewModel(string userName)
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

            DepartmentOverviewModel DepartmentOverviewView = new DepartmentOverviewModel();
            DepartmentOverviewView.Builds = builds;
            DepartmentOverviewView.MomDay = momDay;
            DepartmentOverviewView.RankByYear = rankByYear;
            DepartmentOverviewView.PlanValue = planValue;
            DepartmentOverviewView.Last31DayPieChart = last31DayPieChart;
            DepartmentOverviewView.Last31Day = last31Day;

            return DepartmentOverviewView;
        }

        public DepartmentOverviewModel GetDepartmentOverviewViewModel(string buildId, string date)
        {
            List<DepartmentValue> momDay = context.GetMomDayValueList(buildId, date);
            List<DepartmentValue> rankByYear = context.GetRankByYearValueList(buildId, date);
            List<DepartmentValue> planValue = context.GetPlanValueList(buildId, date);
            List<DepartmentValue> last31DayPieChart = context.GetLast31DayPieChartValueList(buildId, date);
            List<DepartmentValue> last31Day = context.GetLast31DayValueList(buildId, date);

            DepartmentOverviewModel DepartmentOverviewView = new DepartmentOverviewModel();
            DepartmentOverviewView.MomDay = momDay;
            DepartmentOverviewView.RankByYear = rankByYear;
            DepartmentOverviewView.PlanValue = planValue;
            DepartmentOverviewView.Last31DayPieChart = last31DayPieChart;
            DepartmentOverviewView.Last31Day = last31Day;

            return DepartmentOverviewView;
        }
    }
}
