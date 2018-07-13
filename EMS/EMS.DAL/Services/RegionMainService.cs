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
    public class RegionMainService
    {
        private RegionMainDbContext context;

        public RegionMainService()
        {
            context = new RegionMainDbContext();
        }

        public RegionMainViewModel GetViewModelByUserName(string userName)
        {
            RegionMainViewModel model = new RegionMainViewModel();

            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            string showMode;
            BuildExtendInfo filterType = context.GetExtendInfoByBuildId(buildId);

            if (filterType == null)
                showMode = "Publish";
            else
                showMode = filterType.ShowMode;

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<EMSValue> compareValues = context.GetRegionMainCompareValueList(buildId,DateTime.Now.ToShortDateString(), energyCode, showMode);
            List<RankValue> rankValues = context.GetRegionMainRankValueList(buildId, DateTime.Now.ToString("yyyy-MM-dd"), energyCode, showMode);
            List<EMSValue> pieValues = context.GetRegionPieValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);
            List<EMSValue> stackValues = context.GetRegionStackValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);

            model.Builds = builds;
            model.Energys = energys;
            model.CompareValues = compareValues;
            model.RankValues = rankValues;
            model.PieValues = pieValues;
            model.StackValues = stackValues;

            return model;
        }

        public RegionMainViewModel GetViewModel(string buildId)
        {
            RegionMainViewModel model = new RegionMainViewModel();

            string showMode;
            BuildExtendInfo filterType = context.GetExtendInfoByBuildId(buildId);

            if (filterType == null)
                showMode = "Publish";
            else
                showMode = filterType.ShowMode;

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<EMSValue> compareValues = context.GetRegionMainCompareValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);
            List<RankValue> rankValues = context.GetRegionMainRankValueList(buildId, DateTime.Now.ToString("yyyy-MM-dd"), energyCode, showMode);
            List<EMSValue> pieValues = context.GetRegionPieValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);
            List<EMSValue> stackValues = context.GetRegionStackValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);

            model.Energys = energys;
            model.CompareValues = compareValues;
            model.RankValues = rankValues;
            model.PieValues = pieValues;
            model.StackValues = stackValues;

            return model;
        }

        public RegionMainViewModel GetViewModel(string buildId,string energyCode)
        {
            RegionMainViewModel model = new RegionMainViewModel();
            string showMode;
            BuildExtendInfo filterType = context.GetExtendInfoByBuildId(buildId);

            if (filterType == null)
                showMode = "Publish";
            else
                showMode = filterType.ShowMode;

            List<EMSValue> compareValues = context.GetRegionMainCompareValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);
            List<RankValue> rankValues = context.GetRegionMainRankValueList(buildId, DateTime.Now.ToString("yyyy-MM-dd"), energyCode, showMode);
            List<EMSValue> pieValues = context.GetRegionPieValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);
            List<EMSValue> stackValues = context.GetRegionStackValueList(buildId, DateTime.Now.ToShortDateString(), energyCode, showMode);

            model.CompareValues = compareValues;
            model.RankValues = rankValues;
            model.PieValues = pieValues;
            model.StackValues = stackValues;

            return model;
        }
    }
}
