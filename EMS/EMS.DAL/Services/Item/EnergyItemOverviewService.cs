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
    public class EnergyItemOverviewService
    {
        private IEnergyItemOverviewDbContext context;
        public EnergyItemOverviewService()
        {
            context = new EnergyItemOverviewDbContext();
        }

        public EnergyItemOverviewModel GetEnergyItemOverviewViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemValue> momDay = context.GetEnergyItemMomDayValueList(buildId,today.ToString());
            List<EnergyItemValue> rankByMonth = context.GetEnergyItemRankByMonthValueList(buildId,today.ToString());
            List<EnergyItemValue> last31DayPieChart = context.GetEnergyItemLast31DayPieChartValueList(buildId,today.ToString());
            List<EnergyItemValue> last31Day = context.GetEnergyItemLast31DayValueList(buildId,today.ToString());

            EnergyItemOverviewModel energyItemOverviewView = new EnergyItemOverviewModel();
            energyItemOverviewView.Builds = builds;
            energyItemOverviewView.EnergyItemMomDay = momDay;
            energyItemOverviewView.EnergyItemRankByMonth = rankByMonth;
            energyItemOverviewView.EnergyItemLast31DayPieChart = last31DayPieChart;
            energyItemOverviewView.EnergyItemLast31Day = last31Day;

            return energyItemOverviewView;
        }

        public EnergyItemOverviewModel GetEnergyItemOverviewViewModelByBuild(string userName,string buildId)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            List<EnergyItemValue> momDay = context.GetEnergyItemMomDayValueList(buildId, today.ToString());
            List<EnergyItemValue> rankByMonth = context.GetEnergyItemRankByMonthValueList(buildId, today.ToString());
            List<EnergyItemValue> last31DayPieChart = context.GetEnergyItemLast31DayPieChartValueList(buildId, today.ToString());
            List<EnergyItemValue> last31Day = context.GetEnergyItemLast31DayValueList(buildId, today.ToString());

            EnergyItemOverviewModel energyItemOverviewView = new EnergyItemOverviewModel();
            energyItemOverviewView.Builds = builds;
            energyItemOverviewView.EnergyItemMomDay = momDay;
            energyItemOverviewView.EnergyItemRankByMonth = rankByMonth;
            energyItemOverviewView.EnergyItemLast31DayPieChart = last31DayPieChart;
            energyItemOverviewView.EnergyItemLast31Day = last31Day;

            return energyItemOverviewView;
        }

        public EnergyItemOverviewModel GetEnergyItemOverviewViewModel(string buildId, string date)
        {
            List<EnergyItemValue> momDay = context.GetEnergyItemMomDayValueList(buildId, date);
            List<EnergyItemValue> rankByMonth = context.GetEnergyItemRankByMonthValueList(buildId, date);
            List<EnergyItemValue> last31DayPieChart = context.GetEnergyItemLast31DayPieChartValueList(buildId, date);
            List<EnergyItemValue> last31Day = context.GetEnergyItemLast31DayValueList(buildId, date);

            EnergyItemOverviewModel energyItemOverviewView = new EnergyItemOverviewModel();
            energyItemOverviewView.EnergyItemMomDay = momDay;
            energyItemOverviewView.EnergyItemRankByMonth = rankByMonth;
            energyItemOverviewView.EnergyItemLast31DayPieChart = last31DayPieChart;
            energyItemOverviewView.EnergyItemLast31Day = last31Day;

            return energyItemOverviewView;
        }
    }
}
