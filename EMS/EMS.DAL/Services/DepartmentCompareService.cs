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
    public class DepartmentCompareService
    {
        private IDepartmentCompareDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();
        public DepartmentCompareService()
        {
            context = new DepartmentCompareDbContext();
        }

        /// <summary>
        /// 部门用能同比分析
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的第一个部门用能数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，部门列表，以及第一个部门用能数据</returns>
        public DepartmentCompareViewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            ITreeViewDbContext treeViewDb = new TreeViewDbContext();
            List<TreeViewModel> treeView = treeViewDb.GetDepartmentTreeViewList(buildId,energyCode);
            string departmentID = treeView.First().Id;

            List<EMSValue> CompareValue = context.GetDepartmentCompareValueList(buildId, energyCode,departmentID, today.ToString("yyyy-MM-dd"));

            DepartmentCompareViewModel CompareViewModel = new DepartmentCompareViewModel();
            CompareViewModel.Builds = builds;
            CompareViewModel.Energys = energys;
            CompareViewModel.TreeView = treeView;
            CompareViewModel.CompareData = CompareValue;

            return CompareViewModel;
        }

        /// <summary>
        /// 部门用能同比分析
        /// 根据建筑ID和时间，获取该建筑对应的能源按钮列表，第一个部门用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回：能源按钮列表，部门列表，以及第一个部门用能数据</returns>
        public DepartmentCompareViewModel GetViewModel(string buildId, string date)
        {

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            ITreeViewDbContext treeViewDb = new TreeViewDbContext();
            List<TreeViewModel> treeView = treeViewDb.GetDepartmentTreeViewList(buildId,energyCode);
            string departmentID = treeView.First().Id;

            List<EMSValue> CompareValue = context.GetDepartmentCompareValueList(buildId, energyCode,departmentID, date);

            DepartmentCompareViewModel CompareViewModel = new DepartmentCompareViewModel();
            CompareViewModel.Energys = energys;
            CompareViewModel.TreeView = treeView;
            CompareViewModel.CompareData = CompareValue;

            return CompareViewModel;
        }

        public DepartmentCompareViewModel GetViewModel(string buildId, string energyCode,string date)
        {
            ITreeViewDbContext treeViewDb = new TreeViewDbContext();
            List<TreeViewModel> treeView = treeViewDb.GetDepartmentTreeViewList(buildId, energyCode);

            string departmentID;
            if (treeView.Count > 0)
                departmentID = treeView.First().Id;
            else
                departmentID = "";

            List<EMSValue> CompareValue = context.GetDepartmentCompareValueList(buildId, energyCode, departmentID, date);

            DepartmentCompareViewModel CompareViewModel = new DepartmentCompareViewModel();
            CompareViewModel.TreeView = treeView;
            CompareViewModel.CompareData = CompareValue;

            return CompareViewModel;
        }

        /// <summary>
        /// 部门用能同比分析
        /// 根据建筑ID，部门ID和时间，获取该部门用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回：部门用能数据</returns>
        public DepartmentCompareViewModel GetViewModel(string buildId, string energyCode,string departmentID, string date)
        {
            List<EMSValue> CompareValue = context.GetDepartmentCompareValueList(buildId,energyCode, departmentID, date);
            DepartmentCompareViewModel CompareViewModel = new DepartmentCompareViewModel();
            CompareViewModel.CompareData = CompareValue;

            return CompareViewModel;
        }
    }
}
