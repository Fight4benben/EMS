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
    public class MeterConnectStateService
    {
        private MeterConnectStateDbContext context;

        public MeterConnectStateService()
        {
            context = new MeterConnectStateDbContext();
        }

        /// <summary>
        /// 默认获取 建筑ID，分类能耗，所有仪表状态
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>建筑ID，分类能耗，第一栋建筑的所有仪表状态</returns>
        public MeterConnectStateViewModel GetViewModelByUserName(string userName)
        {
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

            List<ConnectState> connectStates = context.GetMeterConnectStateList(buildId, energyCode);

            MeterConnectStateViewModel viewModel = new MeterConnectStateViewModel();
            viewModel.Builds = builds;
            viewModel.Energys = energys;
            viewModel.ConnectStates = connectStates;

            return viewModel;
        }

        public MeterConnectStateViewModel GetViewModel(string buildId)
        {
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);
            string energyCode;
            if (energys.Count > 0)
                energyCode = energys.First().EnergyItemCode;
            else
                energyCode = "";

            List<ConnectState> connectStates = context.GetMeterConnectStateList(buildId, energyCode);

            MeterConnectStateViewModel viewModel = new MeterConnectStateViewModel();
            viewModel.Energys = energys;
            viewModel.ConnectStates = connectStates;

            return viewModel;
        }

        public MeterConnectStateViewModel GetViewModel(string buildId,string energyCode)
        {

            List<ConnectState> connectStates = context.GetMeterConnectStateList(buildId, energyCode);

            MeterConnectStateViewModel viewModel = new MeterConnectStateViewModel();
            viewModel.ConnectStates = connectStates;

            return viewModel;
        }



        /// <summary>
        /// 获取仪表通讯状态
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗</param>
        /// <param name="type">"type"=0 在线 ；"type"=1 离线 </param>
        /// <returns>累计中断时间 "DiffDate"格式为 "0:00:04" 表示 为0天0小时4分钟</returns>
        public MeterConnectStateViewModel GetViewModel(string buildId, string energyCode, string type)
        {
            List<ConnectState> connectStates;
            if(type=="1")
                connectStates = context.GetMeterConnectStateList(buildId, energyCode,type);
            else
                connectStates = context.GetMeterConnectStateList(buildId, energyCode);

            MeterConnectStateViewModel viewModel = new MeterConnectStateViewModel();
            viewModel.ConnectStates = connectStates;

            return viewModel;
        }
    }
}
