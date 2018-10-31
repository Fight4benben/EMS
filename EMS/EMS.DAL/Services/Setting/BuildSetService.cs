﻿using EMS.DAL.Entities;
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

        public BuildSetViewModel SetBuild(BuildInfoSet buildInfoSet)
        {
            int result = context.SetBuildInfo(buildInfoSet);

            BuildSetViewModel viewModel = new BuildSetViewModel();
            if (result == 1)
            {
                viewModel.ResultState.State = 0;
            }
            else
            {
                viewModel.ResultState.State = 1;
            }

            return viewModel;
        }

        public BuildSetViewModel DeleteBuild(string buildId)
        {
            int result = context.DeleteBuild(buildId);

            BuildSetViewModel viewModel = new BuildSetViewModel();
            if (result == 1)
            {
                viewModel.ResultState.State = 0;
            }
            else
            {
                viewModel.ResultState.State = 1;
            }

            return viewModel;
        }
    }
}
