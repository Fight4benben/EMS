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
    public class DepartmentRankService
    {
        private IDepartmentRankDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();

        public DepartmentRankService()
        {
            context = new DepartmentRankDbContext();
        }

        public DepartmentRankViewModel GetViewModelByName(string userName)
        {
            DateTime today = DateTime.Now;
            DateTime monthBegin = new DateTime(today.Year,today.Month,1);
            DateTime monthEnd = monthBegin.AddMonths(1).AddDays(-1);
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId;

            if (builds.Count > 0)
                buildId = builds.First().BuildID;
            else
                buildId = "";

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<EMSValue> list = context.GetRankList(buildId,monthBegin.ToString("yyyy-MM-dd"),monthEnd.ToString("yyyy-MM-dd"),energyCode);

            DepartmentRankViewModel model = new DepartmentRankViewModel();
            model.Builds = builds;
            model.Energys = energys;
            model.RankValues = list;

            return model;
        }

        public DepartmentRankViewModel GetViewModel(string buildId,string date)
        {
            DateTime monthBegin = Utils.Util.ConvertString2DateTime(date+"-01","yyyy-MM-dd");
            DateTime monthEnd = monthBegin.AddMonths(1).AddDays(-1);
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<EMSValue> list = context.GetRankList(buildId, monthBegin.ToString("yyyy-MM-dd"), monthEnd.ToString("yyyy-MM-dd"), energyCode);
            DepartmentRankViewModel model = new DepartmentRankViewModel();
            model.Energys = energys;
            model.RankValues = list;

            return model;
        }

        public DepartmentRankViewModel GetViewModel(string buildId, string date, string energyCode)
        {
            DateTime monthBegin = Utils.Util.ConvertString2DateTime(date + "-01", "yyyy-MM-dd");
            DateTime monthEnd = monthBegin.AddMonths(1).AddDays(-1);

            List<EMSValue> list = context.GetRankList(buildId, monthBegin.ToString("yyyy-MM-dd"), monthEnd.ToString("yyyy-MM-dd"), energyCode);
            DepartmentRankViewModel model = new DepartmentRankViewModel();
            model.RankValues = list;

            return model;
        }
    }
}
