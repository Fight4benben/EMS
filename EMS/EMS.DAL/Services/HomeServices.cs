using EMS.DAL.Entities;
using EMS.DAL.IRepository;
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
    public class HomeServices
    {
        IHomeDbContext context;
        public HomeServices()
        {
           context = new HomeDbContext();
        }

        public HomeViewModel GetHomeViewModel(string userName)
        {
            DateTime today = DateTime.Now.AddHours(-1);
            
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string firstBuildId = builds.First().BuildID;

            BuildInfo currentBuild = context.GetBuildById(firstBuildId);
            List<EnergyClassify> energyClassifyList = context.GetEnergyClassifyValues(firstBuildId,DateTime.Now.ToShortDateString());
            List<EnergyItem> energyItemList = context.GetEnergyItemValues(firstBuildId, DateTime.Now.ToShortDateString());
            List<HourValue> todayValues = context.GetHourValues(firstBuildId,today.ToString("yyyy-MM-dd HH:00:00"));
            List<HourValue> yesterdayValues = context.GetHourValues(firstBuildId, today.AddDays(-1).ToString("yyyy-MM-dd 23:00:00"));

            HomeViewModel homeViewModel = new HomeViewModel(currentBuild,builds,
                TransferEnergyClassifyToViewModel(energyClassifyList),energyItemList,
                new HourValueViewModel(todayValues,yesterdayValues),
                GetCompareViewModelByHourValueList(todayValues,yesterdayValues));

            return homeViewModel;
        }

        public HomeViewModel GetHomeViewModel(string buildId,string date)
        {
            DateTime today = Util.ConvertString2DateTime(date,"yyyy-MM-dd");
            if (today.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                today = DateTime.Now.AddHours(-1);
            }
            else if (DateTime.Now.Subtract(today).Days < 0)
            {
                throw new Exception("传入日期大于当前日期，查不到数据！");
            }
            else
            {
                today = new DateTime(today.Year,today.Month,today.Day,23,0,0);
            }

            
            return GetCommonViewModel(buildId, today);
        }

        HomeViewModel GetCommonViewModel(string buildId,DateTime date)
        {
            BuildInfo currentBuild = context.GetBuildById(buildId);
            List<EnergyClassify> energyClassifyList = context.GetEnergyClassifyValues(buildId, date.ToShortDateString());
            List<EnergyItem> energyItemList = context.GetEnergyItemValues(buildId, date.ToShortDateString());
            List<HourValue> todayValues = context.GetHourValues(buildId, date.ToString("yyyy-MM-dd HH:00:00"));
            List<HourValue> yesterdayValues = context.GetHourValues(buildId, date.AddDays(-1).ToString("yyyy-MM-dd 23:00:00"));

            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.CurrentBuild = currentBuild;
            homeViewModel.EnergyClassify = TransferEnergyClassifyToViewModel(energyClassifyList);
            homeViewModel.EnergyItems = energyItemList;
            homeViewModel.HourValues = new HourValueViewModel(todayValues, yesterdayValues);
            homeViewModel.CompareValues = GetCompareViewModelByHourValueList(todayValues, yesterdayValues);

            return homeViewModel;
        }

        /// <summary>
        /// 将分类数据的实体模型转换成最终页面引用的视图数据模型
        /// </summary>
        /// <param name="list">energyClassifyList</param>
        /// <returns>List<EnergyClassifyViewModel></returns>
        List<EnergyClassifyViewModel> TransferEnergyClassifyToViewModel(List<EnergyClassify> list)
        {
            List<EnergyClassifyViewModel> energyClassifyViewModels = new List<EnergyClassifyViewModel>();
            decimal? monthTotal = 0;
            decimal? yearTotal = 0;
            foreach (var item in list)
            {
                if (item.EnergyRate != null && item.MonthValue != null)
                {
                    monthTotal += item.MonthValue * item.EnergyRate;
                }
                if (item.EnergyRate != null && item.YearValue != null)
                {
                    yearTotal += item.YearValue * item.EnergyRate;
                }
                energyClassifyViewModels.Add(new EnergyClassifyViewModel(item.EnergyItemName, item.MonthValue, item.YearValue, item.Unit));
            }

            energyClassifyViewModels.Add(new EnergyClassifyViewModel("综合能耗",monthTotal,yearTotal,"千克标准煤"));

            return energyClassifyViewModels;
        }

        List<CompareViewModel> GetCompareViewModelByHourValueList(List<HourValue> todayList, List<HourValue> yesterdayList)
        {
            List<CompareViewModel> compareViewModels = new List<CompareViewModel>();

            foreach (var item in todayList)
            {
                CompareViewModel compareViewModel = compareViewModels.Find(delegate (CompareViewModel compare) { return compare.EnergyItemCode == item.EnergyItemCode; });
                HourValue yesterdayValue = yesterdayList.Find(delegate (HourValue hour) { return hour.EnergyItemCode == item.EnergyItemCode && hour.ValueTime.Hour == item.ValueTime.Hour; });
                if (compareViewModel == null)
                {

                    compareViewModels.Add(new CompareViewModel(item.EnergyItemCode, context.GetEnergyItemByCode(item.EnergyItemCode).EnergyItemName,
                        item.Value, yesterdayValue == null ? 0 : yesterdayValue.Value));
                }
                else
                {
                    compareViewModel.TodayValue += item.Value;
                    compareViewModel.YesterdayValue += (yesterdayValue == null ? 0 : yesterdayValue.Value);
                }
            }

            return compareViewModels;
        }
    }
}
