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
        /// 区域用能统计报表
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有区域的用能天报表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，区域列表，以及用能数据天报表</returns>
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

            DepartmentEnergyAverageViewModel viewModel = new DepartmentEnergyAverageViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.TreeView = treeViewModel;

            return viewModel;
        }

        public DepartmentEnergyAverageViewModel GetViewModel(string buildId, string energyCode, string departmentID,string type, string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            string startDay = dateTime.AddDays(-dateTime.Day + 1).ToString("yyyy-MM-dd");
            string endDay = dateTime.AddMonths(1).AddDays(-dateTime.Day).ToString("yyyy-MM-dd");

            List<EnergyAverage> averageData = new List<EnergyAverage>();

            switch (type)
            {
                case "MM":
                    averageData = context.GetDeptMonthEnergyAverageList(buildId, energyCode, departmentID, startDay, endDay);
                    break;

                case "QQ":
                    averageData = context.GetDeptQuarterEnergyAverageList(buildId, energyCode, departmentID, startDay, endDay);
                    break;

                case "YY":
                    averageData = context.GetDeptYearEnergyAverageList(buildId, energyCode, departmentID, startDay, endDay);
                    break;

                default:
                    averageData = context.GetDeptMonthEnergyAverageList(buildId, energyCode, departmentID, startDay, endDay);
                    break;
            }

            DepartmentEnergyAverageViewModel viewModel = new DepartmentEnergyAverageViewModel();
            viewModel.AverageData = averageData;

            return viewModel;
        }
    }
}
