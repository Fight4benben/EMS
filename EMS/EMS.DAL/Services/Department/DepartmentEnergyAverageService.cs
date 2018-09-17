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
    public class DepartmentEnergyAverageService
    {
        private DepartmentEnergyAverageDbContext context;

        public DepartmentEnergyAverageService()
        {
            context = new DepartmentEnergyAverageDbContext();
        }

        /// <summary>
        /// 部门列表
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有部门
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，能源按钮列表，部门列表</returns>
        public DepartmentEnergyAverageViewModel GetViewModelByUserName(string userName)
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

            //DepartmentEnergyAverageViewModel viewModel = new DepartmentEnergyAverageViewModel();
            //viewModel.Builds = builds;
            //viewModel.Energys = energys;
            //viewModel.TreeView = treeViewModel;
            DepartmentEnergyAverageViewModel viewModel = GetViewModel(buildId,energyCode,"MM",DateTime.Now.ToString("yyyy-MM-dd"));
            viewModel.Builds = builds;
            viewModel.Energys = energys;

            return viewModel;
        }

        /// <summary>
        /// 部门人均用能-单位面积用能
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="departmentID"></param>
        /// <param name="type"> 查询类型：月度平均值:"MM";  季度平均值："QQ";  年度平均值："YY"</param>
        /// <param name="date">结束时间(yyyy-MM-dd)</param>
        /// <returns></returns>
        public DepartmentEnergyAverageViewModel GetViewModel(string buildId, string energyCode, string type, string date)
        {
            DateTime dateTime;
            string startDay, endDay;

            List<EnergyAverage> averageData = new List<EnergyAverage>();
            List<CompareData> compareData = new List<CompareData>();

            switch (type)
            {
                case "MM":
                    dateTime = Util.ConvertString2DateTime(date,"yyyy-MM-dd");
                    startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    averageData = context.GetDeptMonthEnergyAverageList(buildId, energyCode, startDay, endDay);
                    compareData = context.GetDeptMonthCompareList(buildId, energyCode, startDay, endDay);
                    break;

                case "QQ":
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM");
                    startDay = dateTime.AddDays(-dateTime.Day + 1).AddMonths(-2).ToString("yyyy-MM-dd");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    averageData = context.GetDeptQuarterEnergyAverageList(buildId, energyCode, startDay, endDay);
                    compareData = context.GetDeptQuarterCompareList(buildId, energyCode, startDay, endDay);
                    break;

                case "YY":
                    startDay = date + "-01-01";
                    endDay = date + "-12-31";
                    averageData = context.GetDeptYearEnergyAverageList(buildId, energyCode, startDay, endDay);
                    compareData = context.GetDeptYearCompareList(buildId, energyCode, startDay, endDay);
                    break;

                default:
                    dateTime = Util.ConvertString2DateTime(date, "yyyy-MM-dd");
                    startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
                    endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");
                    averageData = context.GetDeptMonthEnergyAverageList(buildId, energyCode, startDay, endDay);
                    compareData = context.GetDeptMonthCompareList(buildId, energyCode, startDay, endDay);
                    break;
            }

            DepartmentEnergyAverageViewModel viewModel = new DepartmentEnergyAverageViewModel();
            viewModel.AverageData = averageData;
            viewModel.CompareData = compareData;

            return viewModel;
        }
    }
}
