using EMS.DAL.Entities;
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
    public class EnergyAlarmService
    {
        private EnergyAlarmDbContext context;

        public EnergyAlarmService()
        {
            context = new EnergyAlarmDbContext();
        }

        public EnergyAlarmViewModel GetViewModelByUserName(string userName)
        {
            DateTime today = DateTime.Now;

            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId;
            if (builds.Count > 0)
                buildId = builds.First().BuildID;
            else
                buildId = "";

            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<TreeViewInfo> treeViewInfos = context.GetTreeViewInfoList(buildId, energyCode);

            List<TreeViewModel> treeViewModel = Util.GetTreeViewModel(treeViewInfos);

            List<EnergyAlarm> energyAlarmValue = context.GetEnergyOverLimitValueList(buildId, today.ToString("yyyy-MM-dd"));

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.TreeView = treeViewModel;
            viewModel.Data = energyAlarmValue;

            return viewModel;
        }

        public EnergyAlarmViewModel GetViewModel(string buildId, string date)
        {
            string startTime= string.Format("{0:yyyy-MM-dd}", date);

            List<EnergyAlarm> energyAlarmValue = context.GetEnergyOverLimitValueList(buildId, startTime);

            EnergyAlarmViewModel viewModel = new EnergyAlarmViewModel();
            viewModel.Data = energyAlarmValue;
            return viewModel;
        }
    }
}
