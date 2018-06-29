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
    public class EnergyItemCompareService
    {
        private IEnergyItemCompareDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();
        public EnergyItemCompareService()
        {
            context = new EnergyItemCompareDbContext();
        }

        /// <summary>
        /// 分项用能同比分析
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的第一个回路当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，分项列表，以及第一分类数据</returns>
        public EnergyItemCompareViewModel GetEnergyItemCompareViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;

            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemValue> energyItemCompareValue = context.GetEnergyItemCompareValueList(buildId, energyCode, today.ToString());

            EnergyItemCompareViewModel energyItemCompareView = new EnergyItemCompareViewModel();
            energyItemCompareView.Builds = builds;
            energyItemCompareView.Energys = energys;
            energyItemCompareView.TreeView = treeView;
            energyItemCompareView.CompareData = energyItemCompareValue;

            return energyItemCompareView;
        }

        /// <summary>
        /// 分项用能同比分析
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date"> 传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：能源按钮列表，分项列表，以及第一分类数据</returns>
        public EnergyItemCompareViewModel GetEnergyItemCompareViewModel(string buildId, string date)
        {
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;

            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemValue> energyItemCompareValue = context.GetEnergyItemCompareValueList(buildId, energyCode, date);

            EnergyItemCompareViewModel energyItemCompareView = new EnergyItemCompareViewModel();
            energyItemCompareView.Energys = energys;
            energyItemCompareView.TreeView = treeView;
            energyItemCompareView.CompareData = energyItemCompareValue;

            return energyItemCompareView;
        }
    }
}
