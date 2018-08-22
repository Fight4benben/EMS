using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class EnergyClassifyViewModel
    {
        public EnergyClassifyViewModel()
        { }

        public EnergyClassifyViewModel(string energyItemName, decimal? monthValue, decimal? yearValue, string unit)
        {
            this.EnergyItemName = energyItemName;
            this.MonthValue = monthValue;
            this.YearValue = yearValue;
            this.Unit = unit;
        }
        public string EnergyItemName { get; set; }
        public decimal? MonthValue { get; set; }
        public decimal? YearValue { get; set; }
        public string Unit { get; set; }
    }

    public class HourValueViewModel
    {
        public HourValueViewModel()
        { }

        public HourValueViewModel(List<HourValue> todayList,List<HourValue> yesterdayList)
        {
            this.TodayValues = todayList;
            this.YesterdayValues = yesterdayList;
        }

        public List<HourValue> TodayValues { get; set; }
        public List<HourValue> YesterdayValues { get; set; }
    }

    public class CompareViewModel
    {
        public CompareViewModel()
        {}

        public CompareViewModel(string energyItemCode, string energyItemName,decimal? todayValue, decimal? yesterdayValue)
        {
            this.EnergyItemCode = energyItemCode;
            this.EnergyItemName = energyItemName;
            this.TodayValue = todayValue;
            this.YesterdayValue = yesterdayValue;
        }
        public string EnergyItemCode { get; set; }
        public string EnergyItemName { get; set; }
        public decimal? TodayValue { get; set; }
        public decimal? YesterdayValue { get; set; }
    }

    public class BuildViewModel
    {
        public string BuildID { get; set; }
        public string BuildName { get; set; }
    }

    public class HomeViewModel
    {
        public HomeViewModel()
        { }

        public HomeViewModel(BuildInfo buildInfo, List<BuildViewModel> builds, List<EnergyClassifyViewModel> energyClassify,
            List<EnergyItem> energyItems, HourValueViewModel hourValues, List<CompareViewModel> compareValues,string lineMode)
        {
            this.Builds = builds;
            this.CurrentBuild = buildInfo;
            this.EnergyClassify = energyClassify;
            this.EnergyItems = energyItems;
            this.HourValues = hourValues;
            this.CompareValues = compareValues;
            this.LineMode = lineMode;
        }
        public BuildInfo CurrentBuild { get; set; }
        public List<BuildViewModel> Builds{ get; set; }
        public List<EnergyClassifyViewModel> EnergyClassify { get; set; }
        public List<EnergyItem> EnergyItems { get; set; }
        public HourValueViewModel HourValues { get; set; }
        public List<CompareViewModel> CompareValues { get; set; }
        public string LineMode { get; set; }
    }
}
