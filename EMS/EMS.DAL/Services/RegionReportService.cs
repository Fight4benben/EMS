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
    public class RegionReportService
    {
        private RegionReportDbContext context;

        public RegionReportService()
        {
            context = new RegionReportDbContext();
        }

        /// <summary>
        /// 区域用能统计报表
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有区域的用能天报表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，区域列表，以及用能数据天报表</returns>
        public RegionReportViewModel GetViewModelByUserName(string userName)
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
            string[] RegionIDs = Util.GetAllIDs(treeViewInfos);

            List<ReportValue> reportValue = context.GetReportValueList(energyCode, RegionIDs, today.ToString(), "DD");

            RegionReportViewModel reportView = new RegionReportViewModel();
            reportView.Builds = builds;
            reportView.Energys = energys;
            reportView.TreeView = treeViewModel;
            reportView.Data = reportValue;
            reportView.ReportType = "DD";

            return reportView;
        }

        public RegionReportViewModel GetViewModel(string buildId,string date,string type)
        {
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);
            string[] RegionIDs = Util.GetAllIDs(treeViewInfos);

            List<ReportValue> reportValue = context.GetReportValueList(energyCode, RegionIDs, date, type);

            RegionReportViewModel reportView = new RegionReportViewModel();
            reportView.Energys = energys;
            reportView.TreeView = treeViewModel;
            reportView.Data = reportValue;
            reportView.ReportType = type;

            return reportView;
        }

        /// <summary>
        /// 区域用能统计报表
        /// 根据建筑ID和日期，获取能源按钮列表，区域列表，以及用能数据天报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">能耗分类编码</param>
        /// <returns>返回完整的数据：能源按钮列表，区域列表，以及用能数据天报表</returns>
        public RegionReportViewModel GetViewModel(string buildId, string energyCode,string date,string type)
        {
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);
            string[] RegionIDs = Util.GetAllIDs(treeViewInfos);

            List<ReportValue> reportValue = context.GetReportValueList(energyCode, RegionIDs, date, type);

            RegionReportViewModel reportView = new RegionReportViewModel();
            reportView.TreeView = treeViewModel;
            reportView.Data = reportValue;
            reportView.ReportType =type;

            return reportView;
        }

        /// <summary>
        /// 区域用能统计报表
        /// 根据区域，时间，报表类型，获取指定的用能数据
        /// </summary>
        /// <param name="RegionIDs">区域ID</param>
        /// <param name="energyCode">能耗分类编码</param>
        /// <param name="date">时间</param>
        /// <param name="type">报表类型：DD:日报
        ///                            MM:月报
        ///                            YY:年报
        /// </param>
        /// <returns>返回：指定用能数据</returns>
        public RegionReportViewModel GetViewModel(string energyCode, string[] RegionIDs, string date, string type)
        {
            if (type == "MM")
            {
                date += "-01";
            }
            else if (type == "YY")
            {
                date += "-01-01";
            }

            List<ReportValue> reportValue = context.GetReportValueList(energyCode, RegionIDs, date, type);

            RegionReportViewModel reportView = new RegionReportViewModel();
            reportView.Data = reportValue;
            reportView.ReportType = type;

            return reportView;
        }
    }
}
