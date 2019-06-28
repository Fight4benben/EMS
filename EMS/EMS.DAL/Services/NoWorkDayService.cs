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
    public class NoWorkDayService
    {
        private INoWorkDayDbContext context;
        private ITreeViewDbContext tvcontext;

        public NoWorkDayService()
        {
            context = new NoWorkDayDbContext();
            tvcontext = new TreeViewDbContext();
        }


        public NoWorkDayViewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            string beginDate = today.ToString("yyyy-MM-01 00:00:00");
            string endDate = today.ToString("yyyy-MM-dd HH:mm:00");
            string energyCode = "";

            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            string buildID = "";
            if (builds.Count > 0)
            {
                buildID = builds.First().BuildID;
            }


            List<EnergyItemDict> energys = tvcontext.GetEnergyItemDictByBuild(buildID);
            if (energys.Count > 0)
            {
                energyCode = energys.First().EnergyItemCode;
            }

            List<TreeViewModel> treeView = tvcontext.GetCircuitTreeListViewModel(buildID, energyCode);

            List<NoWorkDay> data = context.GetCircuitData(buildID, energyCode, beginDate, endDate);

            NoWorkDayViewModel viewModel = new NoWorkDayViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }

        public NoWorkDayViewModel GetViewModel(string userName, string buildID)
        {
            DateTime today = DateTime.Now;
            string beginDate = today.ToString("yyyy-MM-01 00:00:00");
            string endDate = today.ToString("yyyy-MM-dd HH:mm:00");
            string energyCode = "";


            List<EnergyItemDict> energys = tvcontext.GetEnergyItemDictByBuild(buildID);
            if (energys.Count > 0)
            {
                energyCode = energys.First().EnergyItemCode;
            }

            List<TreeViewModel> treeView = tvcontext.GetCircuitTreeListViewModel(buildID, energyCode);
            List<NoWorkDay> data = context.GetCircuitData(buildID, energyCode, beginDate, endDate);

            NoWorkDayViewModel viewModel = new NoWorkDayViewModel();
            viewModel.Energys = energys;
            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }

        public NoWorkDayViewModel GetViewModel(string userName, string buildID, string energyCode)
        {
            DateTime today = DateTime.Now;
            string beginDate = today.ToString("yyyy-MM-01 00:00:00");
            string endDate = today.ToString("yyyy-MM-dd HH:mm:00");

            List<TreeViewModel> treeView = tvcontext.GetCircuitTreeListViewModel(buildID, energyCode);
            List<NoWorkDay> data = context.GetCircuitData(buildID, energyCode, beginDate, endDate);

            NoWorkDayViewModel viewModel = new NoWorkDayViewModel();
            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }

        public NoWorkDayViewModel GetViewModel(string userName, string buildID, string energyCode, string beginDate, string endDate)
        {
            beginDate = beginDate + " 00:00:00";
            endDate = endDate + " 23:59:00";

            List<TreeViewModel> treeView = tvcontext.GetCircuitTreeListViewModel(buildID, energyCode);
            List<NoWorkDay> data = context.GetCircuitData(buildID, energyCode, beginDate, endDate);

            NoWorkDayViewModel viewModel = new NoWorkDayViewModel();
            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }

        public NoWorkDayViewModel GetViewModel(string userName, string buildID, string energyCode, string ids, string beginDate, string endDate)
        {
            string[] circuitArry = ids.Split(',');

            beginDate = beginDate + " 00:00:00";
            endDate = endDate + " 23:59:00";

            List<TreeViewModel> treeView = tvcontext.GetCircuitTreeListViewModel(buildID, energyCode);
            List<NoWorkDay> data = context.GetCircuitData(buildID, energyCode, circuitArry, beginDate, endDate);

            NoWorkDayViewModel viewModel = new NoWorkDayViewModel();
            viewModel.TreeView = treeView;
            viewModel.Data = data;

            return viewModel;
        }
    }
}
