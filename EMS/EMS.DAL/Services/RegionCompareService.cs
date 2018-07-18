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
    public class RegionCompareService
    {
        private RegionCompareDbContext context;

        public RegionCompareService()
        {
            context = new RegionCompareDbContext();
        }

        /// <summary>
        /// 区域用能同比分析
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有区域的用能同比分析
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，区域列表，以及用能数据同比分析</returns>
        public RegionCompareViewModel GetViewModelByUserName(string userName)
        {
            DateTime today = DateTime.Now;

            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);
            string regionID;
            if (treeViewInfos.Count > 0)
                regionID = treeViewInfos.First().ID;
            else
                regionID = "";

            List<EMSValue> compareValue = context.GetCompareValueList( energyCode, regionID, today.ToString());

            RegionCompareViewModel ViewModel = new RegionCompareViewModel();
            ViewModel.Builds = builds;
            ViewModel.Energys = energys;
            ViewModel.TreeView = treeViewModel;
            ViewModel.CompareData = compareValue;

            return ViewModel;
        }

        /// <summary>
        /// 区域用能同比分析
        /// 根据建筑ID和日期，获取能源按钮列表，区域列表，以及用能同比分析
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">能耗分类编码</param>
        /// <returns>返回完整的数据：能源按钮列表，区域列表，以及用能数据天报表</returns>
        public RegionCompareViewModel GetViewModel(string buildId, string energyCode)
        {
            DateTime today = DateTime.Now;

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);
            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);
            string regionID;
            if (treeViewInfos.Count > 0)
                regionID = treeViewInfos.First().ID;
            else
                regionID = "";
            
            List<EMSValue> compareValue = context.GetCompareValueList(energyCode, regionID, today.ToString());

            RegionCompareViewModel ViewModel = new RegionCompareViewModel();
            ViewModel.TreeView = treeViewModel;
            ViewModel.CompareData = compareValue;

            return ViewModel;
        }

        /// <summary>
        /// 区域用能同比分析
        /// </summary>
        /// <param name="energyCode">分类编码</param>
        /// <param name="regionID">单个区域ID</param>
        /// <param name="date">日期</param>
        /// <returns>返回：用能数据同比分析</returns>
        public RegionCompareViewModel GetViewModel(string energyCode, string regionID, string date)
        {

            List<EMSValue> compareValue = context.GetCompareValueList(energyCode, regionID, date);

            RegionCompareViewModel ViewModel = new RegionCompareViewModel();
            ViewModel.CompareData = compareValue;

            return ViewModel;
        }
    }
}
