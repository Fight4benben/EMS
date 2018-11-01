using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class BuildSetService
    {
        private BuildSetDbContext context;

        public BuildSetService()
        {
            context = new BuildSetDbContext();
        }

        public BuildSetViewModel GetAllBuilds()
        {
            BuildSetViewModel viewModel = new BuildSetViewModel();

            List<BuildViewModel> builds = context.GetBuildList();
            viewModel.Builds = builds;

            return viewModel;
        }

        public BuildSetViewModel GetViewModelByUserName(string userName)
        {
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId;
            if (builds.Count > 0)
                buildId = builds.First().BuildID;
            else
                buildId = "";

            List<BuildInfoSet> buildInfo = context.GetBuildInfoList(buildId);
            BuildSetViewModel viewModel = new BuildSetViewModel();
            viewModel.Builds = builds;
            viewModel.BuildInfo = buildInfo;

            return viewModel;
        }

        public BuildSetViewModel GetViewModel(string buildId)
        {
            List<BuildInfoSet> buildInfo = context.GetBuildInfoList(buildId);
            BuildSetViewModel viewModel = new BuildSetViewModel();
            viewModel.BuildInfo = buildInfo;

            return viewModel;
        }

        public BuildSetViewModel AddBuild(BuildInfoSet buildInfoSet)
        {
            try
            {
                BuildSetViewModel viewModel = new BuildSetViewModel();
                ResultState resultState = new ResultState();

                int result = context.AddBuildInfo(buildInfoSet);

                if (result == 1)
                {

                    resultState.State = 0;
                    viewModel.ResultState = resultState;
                }
                else
                {
                    resultState.State = 1;
                    viewModel.ResultState = resultState;
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                BuildSetViewModel viewModel = new BuildSetViewModel();
                ResultState resultState = new ResultState();
                resultState.State = 1;
                resultState.Details = ex.Message;
                viewModel.ResultState = resultState;
                return viewModel;
            }

        }

        public BuildSetViewModel UpdateBuild(BuildInfoSet buildInfoSet)
        {
            int result = context.UpdateBuildInfo(buildInfoSet);

            BuildSetViewModel viewModel = new BuildSetViewModel();
            ResultState resultState = new ResultState();
            if (result == 1)
            {

                resultState.State = 0;
                viewModel.ResultState = resultState;
            }
            else
            {
                resultState.State = 1;
                viewModel.ResultState = resultState;
            }

            return viewModel;
        }

        public BuildSetViewModel DeleteBuild(string buildId)
        {
            int result = context.DeleteBuild(buildId);

            BuildSetViewModel viewModel = new BuildSetViewModel();
            ResultState resultState = new ResultState();
            if (result == 1)
            {

                resultState.State = 0;
                viewModel.ResultState = resultState;
            }
            else
            {
                resultState.State = 1;
                viewModel.ResultState = resultState;
            }

            return viewModel;
        }
    }
}
