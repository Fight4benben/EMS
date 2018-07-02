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
    public class EnergyItemReportService
    {
        private IEnergyItemReportDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();
        public EnergyItemReportService()
        {
            context = new EnergyItemReportDbContext();
        }

        /// <summary>
        /// 分项用能统计
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有分项当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，分项列表，以及第一分类的当日用能数据</returns>
        public EnergyItemReportViewModel GetEnergyItemReportViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, today.ToString(), "DD");

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Builds = builds;
            energyItemReportView.Energys = energys;
            energyItemReportView.TreeView = treeView;
            energyItemReportView.Data = reportValue;

            return energyItemReportView;
        }

        /// <summary>
        /// 分项用能统计
        /// 根据建筑ID和日期，获取第一个分类对应的所有分项当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：能源按钮列表，分项列表，以及第一分类的当日用能数据</returns>
        public EnergyItemReportViewModel GetEnergyItemReportViewModel(string buildId, string date)
        {
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, date, "DD");

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Energys = energys;
            energyItemReportView.TreeView = treeView;
            energyItemReportView.Data = reportValue;

            return energyItemReportView;
        }

        /// <summary>
        /// 分项用能统计
        /// 根据建筑ID和日期，获取第一个分类对应的所有分项当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：能源按钮列表，分项列表，以及第一分类的当日用能数据</returns>
        public EnergyItemReportViewModel GetEnergyItemReportViewModel(string[] formulaIDs, string date, string type)
        {
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, date, type);

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Data = reportValue;

            return energyItemReportView;
        }



        string[] GetEnergyItemCodes(List<EnergyItemInfo> EnergyItemInfos)
        {
            List<string> list = new List<string>();
            foreach (var Item in EnergyItemInfos)
            {
                list.Add(Item.FormulaID);
            }

            return list.ToArray();
        }
    }
}
