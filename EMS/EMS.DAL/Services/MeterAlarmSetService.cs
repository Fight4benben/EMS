using EMS.DAL.Entities;
using EMS.DAL.Entities.Setting;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMS.DAL.Services
{
    public class MeterAlarmSetService
    {
        private IMeterAlarmSetDbContext context;
        private ITreeViewDbContext tvContext;

        public MeterAlarmSetService()
        {
            context = new MeterAlarmSetDbContext();
            tvContext = new TreeViewDbContext();
        }

        public MeterAlarmSetViewModel GetViewModel(string userName)
        {
            MeterAlarmSetViewModel viewModel = new MeterAlarmSetViewModel();

            string buildID = "";
            string energyCode = "";

            viewModel.Builds = tvContext.GetBuildsByUserName(userName);
            if (viewModel.Builds.Count > 0)
            {
                buildID = viewModel.Builds.First().BuildID;
            }

            List<EnergyItemDict> energys = tvContext.GetEnergyItemDictByBuild(buildID);
            if (energys.Count > 0)
            {
                energyCode = energys.First().EnergyItemCode;
            }

            List<TreeViewModel> treeView = tvContext.GetCircuitTreeListViewModel(buildID, energyCode);
            List<MeterAlarmSet> data = context.GetMeterParamList(buildID, treeView.First().Id);

            viewModel.Energys = energys;
            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }

        public MeterAlarmSetViewModel GetViewModel(string userName,string buildID)
        {
            MeterAlarmSetViewModel viewModel = new MeterAlarmSetViewModel();

            string energyCode = "";

            List<EnergyItemDict> energys = tvContext.GetEnergyItemDictByBuild(buildID);
            if (energys.Count > 0)
            {
                energyCode = energys.First().EnergyItemCode;
            }

            List<TreeViewModel> treeView = tvContext.GetCircuitTreeListViewModel(buildID, energyCode);
            List<MeterAlarmSet> data = context.GetMeterParamList(buildID, treeView.First().Id);

            viewModel.Energys = energys;
            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }

        public MeterAlarmSetViewModel GetViewModel(string userName, string buildID, string energyCode)
        {
            MeterAlarmSetViewModel viewModel = new MeterAlarmSetViewModel();


            List<TreeViewModel> treeView = tvContext.GetCircuitTreeListViewModel(buildID, energyCode);
            List<MeterAlarmSet> data = context.GetMeterParamList(buildID, treeView.First().Id);

            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }

        public MeterAlarmSetViewModel GetViewModel(string userName, string buildID, string energyCode, string circuitID)
        {
            MeterAlarmSetViewModel viewModel = new MeterAlarmSetViewModel();

            List<MeterAlarmSet> data = context.GetMeterParamList(buildID, circuitID);

            viewModel.Data = data;

            return viewModel;
        }
    }
}
