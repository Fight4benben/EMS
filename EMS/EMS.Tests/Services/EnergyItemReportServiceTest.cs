using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Services
{
    [TestClass]
    public class EnergyItemReportServiceTest
    {
        [TestMethod]
        public void TestGetEnergyItemReportViewModel()
        {
            EnergyItemReportService service = new EnergyItemReportService();
            EnergyItemReportViewModel EnergyReportViewDay = service.GetEnergyItemReportViewModel("admin");

            Console.WriteLine(UtilTest.GetJson(EnergyReportViewDay));
        }

        [TestMethod]
        public void TestGetEnergyItemReportViewModelByBuildID()
        {
            DateTime today = DateTime.Now;

            EnergyItemReportService service = new EnergyItemReportService();
            EnergyItemReportViewModel EnergyReportViewDay = service.GetEnergyItemReportViewModel("000001G001", today.ToString());

            Console.WriteLine(UtilTest.GetJson(EnergyReportViewDay));
        }

        [TestMethod]
        public void TestGetEnergyItemReportViewModelByEnergyItemIDAndTypeMonth()
        {
            DateTime today = DateTime.Now;
            string buildId = "000001G001";
            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);

            EnergyItemReportService service = new EnergyItemReportService();
            EnergyItemReportViewModel EnergyReportViewDay = service.GetEnergyItemReportViewModel(formulaIDs, today.ToString(), "MM");

            Console.WriteLine(UtilTest.GetJson(EnergyReportViewDay));
        }

        [TestMethod]
        public void TestGetEnergyItemReportViewModelByEnergyItemIDAndTypeYear()
        {
            DateTime today = DateTime.Now;
            string buildId = "000001G001";
            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);

            EnergyItemReportService service = new EnergyItemReportService();
            EnergyItemReportViewModel EnergyReportViewDay = service.GetEnergyItemReportViewModel(formulaIDs, today.ToString(), "YY");

            Console.WriteLine(UtilTest.GetJson(EnergyReportViewDay));
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
