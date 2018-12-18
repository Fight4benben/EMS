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
    public class DepartmentAreaAvgRankService
    {
        private DepartmentEnergyAverageDbContext context;

        public DepartmentAreaAvgRankService()
        {
            context = new DepartmentEnergyAverageDbContext();
        }

        /// <summary>
        /// 部门 月份 单位面积能耗
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有部门
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，能源按钮列表，部门单位面积能耗</returns>
        public DepartmentAreaAvgRankViewModel GetViewModelByUserName(string userName)
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

            List<EnergyAverage> averageData = new List<EnergyAverage>();
            averageData = context.GetDeptMonthEnergyAverageList(buildId, energyCode, DateTime.Now.ToString("yyyy-MM-01"), DateTime.Now.ToString("yyyy-MM-dd"));

            DepartmentAreaAvgRankViewModel viewModel = new DepartmentAreaAvgRankViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.AverageData = averageData;

            return viewModel;
        }

        /// <summary>
        /// 部门 月份 单位面积能耗
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="date">时间(yyyy-MM-dd)</param>
        /// <returns></returns>
        public DepartmentAreaAvgRankViewModel GetViewModel(string buildId, string energyCode, string date)
        {
            DateTime dateTime;
            string startDay, endDay;
            List<EnergyAverage> averageData = new List<EnergyAverage>();

            dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd");
            startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
            endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
            averageData = context.GetDeptMonthEnergyAverageList(buildId, energyCode, startDay, endDay);

            DepartmentAreaAvgRankViewModel viewModel = new DepartmentAreaAvgRankViewModel();
            viewModel.AverageData = averageData;

            return viewModel;
        }

    }
}
