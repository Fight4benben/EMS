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

        /// <summary>
        /// 获取系统中全部的建筑
        /// </summary>
        /// <returns></returns>
        public BuildSetViewModel GetAllBuilds()
        {
            BuildSetViewModel viewModel = new BuildSetViewModel();

            List<BuildViewModel> builds = context.GetBuildList();
            viewModel.Builds = builds;

            return viewModel;
        }

        /// <summary>
        /// 获取用户关联的建筑
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public BuildSetViewModel GetViewModelByUserName(string userName)
        {
            List<BuildInfoSet> buildInfoList = context.GetBuildInfoAll(userName);
            
            BuildSetViewModel viewModel = new BuildSetViewModel();
            
            viewModel.BuildInfo = buildInfoList;

            return viewModel;
        }

        /// <summary>
        /// 获取建筑信息
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public BuildSetViewModel GetViewModel(string buildId)
        {
            List<BuildInfoSet> buildInfo = context.GetBuildInfoList(buildId);
            BuildSetViewModel viewModel = new BuildSetViewModel();
            viewModel.BuildInfo = buildInfo;

            return viewModel;
        }

        /// <summary>
        /// 新增建筑
        /// </summary>
        /// <param name="buildInfoSet"></param>
        /// <returns></returns>
        public BuildSetViewModel AddBuild(BuildInfoSet inputBuildInfoSet)
        {
            try
            {
                BuildSetViewModel viewModel = new BuildSetViewModel();
                ResultState resultState = new ResultState();

                List<BuildViewModel> builds = context.GetBuildList();
                //最后一个建筑ID
                BuildViewModel lastBuilD = builds.Last();
                string lastBID = lastBuilD.BuildID;

                //新插入的建筑ID
                string newBuildID;
                int rightID = Convert.ToInt16(lastBID.Substring(lastBID.Length - 3));

                if (rightID + 1 < 10)
                {
                    newBuildID = lastBID.Substring(0, lastBID.Length - 1) + (rightID + 1).ToString();
                }
                else if (rightID + 1 >= 10 && rightID + 1 < 100)
                {
                    newBuildID = lastBID.Substring(0, lastBID.Length - 2) + (rightID + 1).ToString();
                }
                else
                {
                    newBuildID = lastBID.Substring(0, lastBID.Length - 3) + (rightID + 1).ToString();
                }

                BuildInfoSet buildInfoSet = new BuildInfoSet();

                buildInfoSet.BuildID = newBuildID;
                buildInfoSet.DataCenterID = "000001";
                buildInfoSet.BuildName = inputBuildInfoSet.BuildName;
                buildInfoSet.AliasName = string.IsNullOrEmpty(inputBuildInfoSet.AliasName) ? "A" : inputBuildInfoSet.AliasName.Length > 16 ? inputBuildInfoSet.AliasName.Substring(0, 16) : inputBuildInfoSet.AliasName;
                buildInfoSet.BuildOwner = string.IsNullOrEmpty(inputBuildInfoSet.BuildOwner) ? "无" : inputBuildInfoSet.BuildOwner;

                buildInfoSet.DistrictCode = "310000";
                buildInfoSet.BuildAddr = string.IsNullOrEmpty(inputBuildInfoSet.BuildAddr) ? "无" : inputBuildInfoSet.BuildAddr;
                buildInfoSet.BuildLong = inputBuildInfoSet.BuildLong > 0 ? inputBuildInfoSet.BuildLong : 121;
                //默认上海市坐标121.506267,31.243709
                buildInfoSet.BuildLat = inputBuildInfoSet.BuildLat > 0 ? inputBuildInfoSet.BuildLat : 31;
                buildInfoSet.BuildYear = DateTime.Now.Year;

                buildInfoSet.UpFloor = inputBuildInfoSet.UpFloor > 0 ? inputBuildInfoSet.UpFloor : 0;
                buildInfoSet.DownFloor = inputBuildInfoSet.DownFloor > 0 ? inputBuildInfoSet.DownFloor : 0;
                buildInfoSet.BuildFunc = "G";
                buildInfoSet.TotalArea = inputBuildInfoSet.TotalArea > 0 ? inputBuildInfoSet.TotalArea : 0;
                buildInfoSet.AirArea = inputBuildInfoSet.AirArea > 0 ? inputBuildInfoSet.AirArea : 0;

                buildInfoSet.DesignDept = string.IsNullOrEmpty(inputBuildInfoSet.DesignDept) ? "无" : inputBuildInfoSet.DesignDept;
                buildInfoSet.WorkDept = string.IsNullOrEmpty(inputBuildInfoSet.WorkDept) ? "无" : inputBuildInfoSet.WorkDept;
                buildInfoSet.CreateTime = DateTime.Now;
                buildInfoSet.CreateUser = string.IsNullOrEmpty(inputBuildInfoSet.CreateUser) ? "无" : inputBuildInfoSet.CreateUser;
                buildInfoSet.MonitorDate = DateTime.Now;

                buildInfoSet.AcceptDate = DateTime.Now;
                buildInfoSet.NumberOfPeople = inputBuildInfoSet.NumberOfPeople > 0 ? inputBuildInfoSet.NumberOfPeople : 0;
                buildInfoSet.SPArea = 0;
                buildInfoSet.TransCount = inputBuildInfoSet.TransCount > 0 ? inputBuildInfoSet.TransCount : 0;
                //安装变压器容量，运行容量，监控仪表数量，联系电话
                buildInfoSet.InstallCapacity = inputBuildInfoSet.InstallCapacity > 0 ? inputBuildInfoSet.InstallCapacity : 0;
                buildInfoSet.OperateCapacity = inputBuildInfoSet.OperateCapacity > 0 ? inputBuildInfoSet.OperateCapacity : 0;
                buildInfoSet.DesignMeters = inputBuildInfoSet.DesignMeters > 0 ? inputBuildInfoSet.DesignMeters : 0;
                buildInfoSet.Mobiles = string.IsNullOrEmpty(inputBuildInfoSet.Mobiles) ? "无" : inputBuildInfoSet.Mobiles;

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

        /// <summary>
        /// 修改建筑信息
        /// </summary>
        /// <param name="buildInfoSet"></param>
        /// <returns></returns>
        public BuildSetViewModel UpdateBuild(BuildInfoSet inputBuildInfoSet)
        {
            BuildSetViewModel viewModel = new BuildSetViewModel();
            ResultState resultState = new ResultState();

            BuildInfoSet buildInfoSet = new BuildInfoSet();

            if (string.IsNullOrEmpty(inputBuildInfoSet.BuildID))
            {
                resultState.State = 1;
                resultState.Details = "建筑ID不能为空，请输入正确的建筑ID。";
                viewModel.ResultState = resultState;
                return viewModel;
            }

            buildInfoSet.BuildID = inputBuildInfoSet.BuildID;
            buildInfoSet.DataCenterID = "000001";
            buildInfoSet.BuildName = inputBuildInfoSet.BuildName;
            buildInfoSet.AliasName = string.IsNullOrEmpty(inputBuildInfoSet.AliasName) ? "A" : inputBuildInfoSet.AliasName.Length > 16 ? inputBuildInfoSet.AliasName.Substring(0, 16) : inputBuildInfoSet.AliasName;
            buildInfoSet.BuildOwner = string.IsNullOrEmpty(inputBuildInfoSet.BuildOwner) ? "无" : inputBuildInfoSet.BuildOwner;

            buildInfoSet.DistrictCode = "310000";
            buildInfoSet.BuildAddr = string.IsNullOrEmpty(inputBuildInfoSet.BuildAddr) ? "无" : inputBuildInfoSet.BuildAddr;
            buildInfoSet.BuildLong = inputBuildInfoSet.BuildLong > 0 ? inputBuildInfoSet.BuildLong : 121;
            //默认上海市坐标121.506267,31.243709
            buildInfoSet.BuildLat = inputBuildInfoSet.BuildLat > 0 ? inputBuildInfoSet.BuildLat : 31;
            buildInfoSet.BuildYear = DateTime.Now.Year;

            buildInfoSet.UpFloor = inputBuildInfoSet.UpFloor > 0 ? inputBuildInfoSet.UpFloor : 0;
            buildInfoSet.DownFloor = inputBuildInfoSet.DownFloor > 0 ? inputBuildInfoSet.DownFloor : 0;
            buildInfoSet.BuildFunc = "G";
            buildInfoSet.TotalArea = inputBuildInfoSet.TotalArea > 0 ? inputBuildInfoSet.TotalArea : 0;
            buildInfoSet.AirArea = inputBuildInfoSet.AirArea > 0 ? inputBuildInfoSet.AirArea : 0;

            buildInfoSet.DesignDept = string.IsNullOrEmpty(inputBuildInfoSet.DesignDept) ? "无" : inputBuildInfoSet.DesignDept;
            buildInfoSet.WorkDept = string.IsNullOrEmpty(inputBuildInfoSet.WorkDept) ? "无" : inputBuildInfoSet.WorkDept;
            buildInfoSet.CreateTime = DateTime.Now;
            buildInfoSet.CreateUser = string.IsNullOrEmpty(inputBuildInfoSet.CreateUser) ? "无" : inputBuildInfoSet.CreateUser;
            buildInfoSet.MonitorDate = DateTime.Now;

            buildInfoSet.AcceptDate = DateTime.Now;
            buildInfoSet.NumberOfPeople = inputBuildInfoSet.NumberOfPeople > 0 ? inputBuildInfoSet.NumberOfPeople : 0;
            buildInfoSet.SPArea = 0;
            buildInfoSet.TransCount = inputBuildInfoSet.TransCount > 0 ? inputBuildInfoSet.TransCount : 0;
            //安装变压器容量，运行容量，监控仪表数量，联系电话
            buildInfoSet.InstallCapacity = inputBuildInfoSet.InstallCapacity > 0 ? inputBuildInfoSet.InstallCapacity : 0;
            buildInfoSet.OperateCapacity = inputBuildInfoSet.OperateCapacity > 0 ? inputBuildInfoSet.OperateCapacity : 0;
            buildInfoSet.DesignMeters = inputBuildInfoSet.DesignMeters > 0 ? inputBuildInfoSet.DesignMeters : 0;
            buildInfoSet.Mobiles = string.IsNullOrEmpty(inputBuildInfoSet.Mobiles) ? "无" : inputBuildInfoSet.Mobiles;


            int result = context.UpdateBuildInfo(buildInfoSet);


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

        /// <summary>
        /// 更新建筑信息
        /// </summary>
        /// <param name="buildID"></param>
        /// <param name="buildName"></param>
        /// <param name="buildAddr"></param>
        /// <param name="buildLong"></param>
        /// <param name="buildLat"></param>
        /// <param name="totalArea"></param>
        /// <param name="numberOfPeople"></param>
        /// <param name="transCount"></param>
        /// <param name="installCapacity"></param>
        /// <param name="operateCapacity"></param>
        /// <param name="designMeters"></param>
        /// <returns></returns>
        public BuildSetViewModel UpdatePartBuildInfo(string buildID, string buildName, string buildAddr,
            decimal buildLong, decimal buildLat, decimal totalArea, int numberOfPeople,
            int transCount, int installCapacity, int operateCapacity, int designMeters)
        {
            BuildSetViewModel viewModel = new BuildSetViewModel();
            ResultState resultState = new ResultState();

            BuildInfoSet buildInfoSet = new BuildInfoSet();

            if (string.IsNullOrEmpty(buildID))
            {
                resultState.State = 1;
                resultState.Details = "建筑ID不能为空，请输入正确的建筑ID。";
                viewModel.ResultState = resultState;
                return viewModel;
            }

            List<BuildInfoSet> buildInfo = context.GetBuildByNmaeList(buildID, buildName);
            if (buildInfo.Count > 0)
            {
                resultState.State = 1;
                resultState.Details = "建筑名称重复，请输入其他名称。";
                viewModel.ResultState = resultState;
                return viewModel;
            }


            buildInfoSet.BuildID = buildID;
            buildInfoSet.BuildName = buildName;

            buildInfoSet.BuildAddr = string.IsNullOrEmpty(buildAddr) ? "无" : buildAddr;
            buildInfoSet.BuildLong = buildLong > 0 ? buildLong : 121.5062m;
            //默认上海市坐标121.506267,31.243709
            buildInfoSet.BuildLat = buildLat > 0 ? buildLat : 31.2437m;
            buildInfoSet.TotalArea = totalArea > 0 ? totalArea : 0;
            buildInfoSet.NumberOfPeople = numberOfPeople > 0 ? numberOfPeople : 0;

            //变压器数量，安装变压器容量，运行容量，监控仪表数量
            buildInfoSet.TransCount = transCount > 0 ? transCount : 0;
            buildInfoSet.InstallCapacity = installCapacity > 0 ? installCapacity : 0;
            buildInfoSet.OperateCapacity = operateCapacity > 0 ? operateCapacity : 0;
            buildInfoSet.DesignMeters = designMeters > 0 ? designMeters : 0;

            int result = context.UpdatePartBuildInfo(buildInfoSet);
            if (result == 1)
            {
                resultState.State = 0;
                resultState.Details = "OK";
                viewModel.ResultState = resultState;
            }
            else
            {
                resultState.State = 1;
                resultState.Details = "NG";
                viewModel.ResultState = resultState;
            }

            return viewModel;
        }

        /// <summary>
        /// 删除建筑，删除建筑前，须先删除与建筑关联的仪表和支路；
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public BuildSetViewModel DeleteBuild(string buildId)
        {
            BuildSetViewModel viewModel = new BuildSetViewModel();
            ResultState resultState = new ResultState();
            if (string.IsNullOrEmpty(buildId))
            {
                resultState.State = 1;
                resultState.Details = "建筑ID不能为空，请输入正确的建筑ID。";
                viewModel.ResultState = resultState;
                return viewModel;
            }

            int result = context.DeleteBuild(buildId);

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
