using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class EnergyItemReportDbContextTest
    {
        [TestMethod]
        public void TestGetEnergyItemReportDayValue()
        {
            IEnergyItemReportDbContext context = new EnergyItemReportDbContext();
            IEnergyItemTreeViewDbContext TreeViewcontext = new EnergyItemTreeViewDbContext();
            DateTime today = DateTime.Now.AddDays(-1);
            string buildID = "000001G001";
            List<EnergyItemInfo> energyItemInfos = TreeViewcontext.GetEnergyItemInfoList(buildID);

            string[] energyCodes = GetEnergyItemCodes(energyItemInfos);

            List<ReportValue> reportValue = context.GetReportValueList(energyCodes, today.ToString(),"DD");

            Console.WriteLine(UtilTest.GetJson(reportValue));
           
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
