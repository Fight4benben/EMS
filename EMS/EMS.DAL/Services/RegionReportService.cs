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
        /// 部门用能统计报表
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有部门的用能天报表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，部门列表，以及用能数据天报表</returns>
        public RegionReportViewModel GetViewModelByUserName(string userName)
        {
            DateTime today = DateTime.Now;
            
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);
            string[] RegionIDs = Util.GetAllIDs(treeViewInfos);

            List<ReportValue> reportValue = context.GetReportValueList(RegionIDs,energyCode, today.ToString(), "DD");

            RegionReportViewModel reportView = new RegionReportViewModel();
            reportView.Builds = builds;
            reportView.Energys = energys;
            reportView.TreeView = treeViewModel;
            reportView.Data = reportValue;
            reportView.ReportType = "DD";

            return reportView;
        }
    }
}
