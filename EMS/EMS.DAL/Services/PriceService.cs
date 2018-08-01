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
    public class PriceService
    {
        private PriceDbContext context;
        RegionReportService service = new RegionReportService();

        public PriceService()
        {
            context = new PriceDbContext();
        }

        /// <summary>
        /// 区域用能费用报表
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有区域的用能天报表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，区域列表，以及用能数据天报表，分类能耗单价</returns>
        public PriceViewModel GetViewModelByUserName(string userName)
        {
            PriceViewModel viewModel = new PriceViewModel();

            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);
            string buildId;
            if (builds.Count > 0)
                buildId = builds.First().BuildID;
            else
                buildId = "";

            viewModel.EnergyPrice = GetEnergyPrice(buildId);
            viewModel.RegionReportModel = service.GetViewModelByUserName(userName);

            return viewModel;
        }

        /// <summary>
        ///  区域用能费用报表
        /// 根据建筑ID，日期和报表类型，获取能源按钮列表，区域列表，以及用能数据报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间</param>
        /// <param name="type">报表类型：DD:日报; MM:月报; YY:年报
        /// <returns>返回完整的数据：能源按钮列表，区域列表，以及用能数据报表，分类能耗单价</returns>
        public PriceViewModel GetViewModel(string buildId, string date, string type)
        {
            PriceViewModel viewModel = new PriceViewModel();

            viewModel.EnergyPrice = GetEnergyPrice(buildId);
            viewModel.RegionReportModel = service.GetViewModel(buildId, date, type);

            return viewModel;
        }

        public PriceViewModel GetViewModel(string buildId, string energyCode, string date, string type)
        {
            PriceViewModel viewModel = new PriceViewModel();

            viewModel.EnergyPrice = GetEnergyPrice(buildId);
            viewModel.RegionReportModel = service.GetViewModel(buildId, energyCode, date, type);

            return viewModel;
        }
        /// <summary>
        /// 区域用能统计报表
        /// 根据区域，时间，报表类型，获取指定的用能数据
        /// </summary>
        /// <param name="RegionIDs">区域ID</param>
        /// <param name="energyCode">能耗分类编码</param>
        /// <param name="date">时间</param>
        /// <param name="type">报表类型：DD:日报; MM:月报; YY:年报
        /// </param>
        /// <returns>返回：指定用能数据</returns>
        public PriceViewModel GetViewModel(string buildId, string energyCode, string[] RegionIDs, string date, string type)
        {
            PriceViewModel viewModel = new PriceViewModel();

            viewModel.EnergyPrice = GetEnergyPrice(buildId);
            viewModel.RegionReportModel = service.GetViewModel(buildId, energyCode, RegionIDs, date, type);

            return viewModel;
        }



        public List<EnergyPrice> GetEnergyPrice(string buildId)
        {
            Price price = context.GetPrice(buildId);
            List<EnergyPrice> energyPrice = new List<EnergyPrice>();

            EnergyPrice ePrice = new EnergyPrice();
            ePrice.Name = "电";
            ePrice.Code = "01000";
            ePrice.Price = price.ElectriPrice;
            energyPrice.Add(ePrice);

            EnergyPrice wPrice = new EnergyPrice();
            wPrice.Name = "水";
            wPrice.Code = "02000";
            wPrice.Price = price.WaterPrice;
            energyPrice.Add(wPrice);

            EnergyPrice gPrice = new EnergyPrice();
            gPrice.Name = "气";
            gPrice.Code = "03000";
            gPrice.Price = price.GasPrice;
            energyPrice.Add(gPrice);

            return energyPrice;
        }


        public PriceViewModel GetPriceViewModel(string buildId)
        {
            Price price = context.GetPrice(buildId);
            List<EnergyPrice> energyPrice = new List<EnergyPrice>();

            EnergyPrice ePrice = new EnergyPrice();
            ePrice.Name = "电";
            ePrice.Code = "01000";
            ePrice.Price = price.ElectriPrice;
            energyPrice.Add(ePrice);

            EnergyPrice wPrice = new EnergyPrice();
            wPrice.Name = "水";
            wPrice.Code = "02000";
            wPrice.Price = price.WaterPrice;
            energyPrice.Add(wPrice);

            EnergyPrice gPrice = new EnergyPrice();
            gPrice.Name = "水";
            gPrice.Code = "03000";
            gPrice.Price = price.GasPrice;
            energyPrice.Add(gPrice);

            PriceViewModel viewModel = new PriceViewModel();
            viewModel.EnergyPrice = energyPrice;

            return viewModel;
        }
    }
}
