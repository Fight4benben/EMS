﻿using EMS.DAL.Entities;
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
    public class OverAllSearchService
    {
        private OverAllSearchDbContext context;

        public OverAllSearchService()
        {
            context = new OverAllSearchDbContext();
        }

        public OverAllSearchViewModel GetViewModelByUserName(string userName)
        {
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId;
            if (builds.Count > 0)
                buildId = builds.First().BuildID;
            else
                buildId = "";

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            OverAllSearchViewModel viewModel = new OverAllSearchViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            return viewModel;

        }

        /// <summary>
        /// 获取搜索结果
        /// </summary>
        /// <param name="timeType">天："DD"；月份："MM"; 季度："QQ"</param>
        /// <param name="type">支路："Circuit"；部门："Dept";区域："Region"</param>

        /// <param name="keyWord">搜索内容</param>
        /// <param name="endDay">结束时间（"yyyy-MM-dd"）</param>
        public OverAllSearchViewModel GetViewModel(string timeType, string type, string keyWord, string buildID, string energyCode, string date)
        {
            DateTime inputDate = Util.ConvertString2DateTime(date, "yyyy-MM-dd"); ;
            
            //每月第一天
            string startDay = inputDate.ToString("yyyy-MM") + "-01";
            //每月最后一天
            string endDay = inputDate.AddMonths(1).AddDays(-inputDate.Day).ToString("yyyy-MM-dd");

            List<EMSValue> timeDataList = new List<EMSValue>();
            List<CompareData> momDataList = new List<CompareData>();
            List<EnergyAverage> monthAverageList = new List<EnergyAverage>();
            List<EnergyAverage> yearAverageList = new List<EnergyAverage>();

            switch (timeType)
            {
                case "DD":

                    timeDataList = context.GetDayList(type, keyWord, buildID, energyCode, inputDate.ToString("yyyy-MM-dd"));
                    momDataList = context.GetMomMonthList(type, keyWord, buildID, energyCode, startDay, endDay);
                    monthAverageList = context.GetMonthAverageList(type, keyWord, buildID, energyCode, startDay, endDay);
                    yearAverageList = context.GetYearAverageList(type, keyWord, buildID, energyCode, inputDate.ToString("yyyy-01") + "-01", endDay);
                    break;

                case "MM":
                    timeDataList = context.GetMonthList(type, keyWord, buildID, energyCode, endDay);
                    momDataList = context.GetMomMonthList(type, keyWord, buildID, energyCode, startDay, endDay);
                    monthAverageList = context.GetMonthAverageList(type, keyWord, buildID, energyCode, startDay, endDay);
                    yearAverageList = context.GetYearAverageList(type, keyWord, buildID, energyCode, inputDate.ToString("yyyy-01") + "-01", endDay);
                    break;

                case "QQ":
                    startDay = inputDate.AddDays(-inputDate.Day + 1).AddMonths(-2).ToString("yyyy-MM-dd");
                    endDay = inputDate.AddMonths(1).AddDays(-inputDate.Day).ToString("yyyy-MM-dd");

                    timeDataList = context.GetQuarterList(type, keyWord, buildID, energyCode, startDay, endDay);
                    momDataList = context.GetMomQuarterList(type, keyWord, buildID, energyCode, startDay, endDay);
                    monthAverageList = context.GetMonthAverageList(type, keyWord, buildID, energyCode, inputDate.ToString("yyyy-MM") + "-01", endDay);
                    yearAverageList = context.GetYearAverageList(type, keyWord, buildID, energyCode, inputDate.ToString("yyyy-01") + "-01", endDay);
                    break;

                default:
                    return null;

            }

            OverAllSearchViewModel viewModel = new OverAllSearchViewModel();
            viewModel.TimeData = timeDataList;
            viewModel.MomData = momDataList;
            viewModel.MonthAverageData = monthAverageList;
            viewModel.YearAverageData = yearAverageList;

            return viewModel;
        }
    }
}
