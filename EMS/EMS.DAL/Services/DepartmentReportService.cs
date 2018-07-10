
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
    public class DepartmentReportService
    {
        private IDepartmentReportDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();

        public DepartmentReportService()
        {
            context = new DepartmentReportDbContext();
        }

        /// <summary>
        /// 部门用能统计报表
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有部门的用能天报表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，部门列表，以及用能数据天报表</returns>
        public DepartmentReportViewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            ITreeViewDbContext treeViewDb = new TreeViewDbContext();
            List<TreeViewModel> treeView = treeViewDb.GetDepartmentTreeViewList(buildId);
            string[] departmentIDs = treeViewDb.GetDepartmentIDs(buildId);
            List<ReportValue> reportValue = context.GetReportValueList(departmentIDs, today.ToString(), "DD");

            DepartmentReportViewModel reportView = new DepartmentReportViewModel();
            reportView.Builds = builds;
            reportView.Energys = energys;
            reportView.TreeView = treeView;
            reportView.Data = reportValue;
            reportView.ReportType = "DD";

            return reportView;
        }

        /// <summary>
        /// 部门用能统计报表
        /// 根据建筑ID和日期，获取能源按钮列表，部门列表，以及用能数据天报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回完整的数据：能源按钮列表，部门列表，以及用能数据天报表</returns>
        public DepartmentReportViewModel GetViewModel(string buildId, string date)
        {
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            ITreeViewDbContext treeViewDb = new TreeViewDbContext();
            List<TreeViewModel> treeView = treeViewDb.GetDepartmentTreeViewList(buildId);
            string[] departmentIDs = treeViewDb.GetDepartmentIDs(buildId);
            List<ReportValue> reportValue = context.GetReportValueList(departmentIDs, date, "DD");

            DepartmentReportViewModel reportView = new DepartmentReportViewModel();
            reportView.Energys = energys;
            reportView.TreeView = treeView;
            reportView.Data = reportValue;
            reportView.ReportType = "DD";

            return reportView;
        }

        /// <summary>
        /// 部门用能统计报表
        /// 根据部门，时间，报表类型，获取指定的用能概况
        /// </summary>
        /// <param name="departmentIDs">部门ID</param>
        /// <param name="date">时间</param>
        /// <param name="type">报表类型：DD:日报
        ///                            MM:月报
        ///                            YY:年报
        /// </param>
        /// <returns>返回：指定部门，时间，报表类型的用能数据</returns>
        public DepartmentReportViewModel GetViewModel(string[] departmentIDs, string date, string type)
        {
            List<ReportValue> reportValue = context.GetReportValueList(departmentIDs, date, type);

            DepartmentReportViewModel reportView = new DepartmentReportViewModel();
            reportView.Data = reportValue;
            reportView.ReportType = type;

            return reportView;
        }
    }
}
