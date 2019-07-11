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
    public class ToxicGasesService
    {
        private IToxicGasesDbContext context;
        private ITreeViewDbContext tvContext;
        private IHistoryParamDbContext historyContext;

        public ToxicGasesService()
        {
            context = new ToxicGasesDbContext();
            tvContext = new TreeViewDbContext();
            historyContext = new HistoryParamDbContext();
        }

        public object GetViewModel(string userName)
        {
            ToxicGasesViewModel viewModel = new ToxicGasesViewModel();

            string buildID = "";
            string meterID = "";

            viewModel.Builds = tvContext.GetBuildsByUserName(userName);
            if (viewModel.Builds.Count > 0)
            {
                buildID = viewModel.Builds.First().BuildID;
            }

            viewModel.Devices = context.GetMeterList(buildID);
            if (viewModel.Devices.Count > 0)
            {
                meterID = viewModel.Devices.First().ID;
            }

            viewModel.CurrentData = context.GetOneMeterValue(meterID);

            return viewModel;
        }

        public object GetViewModel(string userName, string buildID)
        {
            ToxicGasesViewModel viewModel = new ToxicGasesViewModel();

            string meterID = "";
            viewModel.Devices = context.GetMeterList(buildID);
            if (viewModel.Devices.Count > 0)
            {
                meterID = viewModel.Devices.First().ID;
            }

            viewModel.CurrentData = context.GetOneMeterValue(meterID);

            return viewModel;
        }

        public object GetViewModel(string userName, string buildID, string meterID)
        {
            ToxicGasesViewModel viewModel = new ToxicGasesViewModel();

            viewModel.CurrentData = context.GetOneMeterValue(meterID);

            return viewModel;
        }

        public object GetHistoryDataViewModel(string userName, string buildID, string meterID, string date)
        {
            ToxicGasesViewModel viewModel = new ToxicGasesViewModel();

            viewModel.HistoryData = historyContext.GetParamByMeterIDValue(meterID, date,5);

            return viewModel;
        }


    }
}
