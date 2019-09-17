using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class PriceService
    {
        private PriceDbContext context;
        private RegionReportDbContext regioncontext = new RegionReportDbContext();
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();
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
            if (price != null)
            {
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
            }
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

        public Excel ExportReportToExcel(string basePath, string buildId, string energyCode, string[] regionIds, string date, string type)
        {
            string templatePath = basePath + "/DayReportTemplate.xls";
            string reportType = " 日报(" + date + ")";
            if (type == "MM")
            {
                reportType = " 月报(" + date + ")";
                templatePath = basePath + "/MonthReportTemplate.xls";
            }
            else if (type == "YY")
            {
                reportType = " 年报(" + date + ")";
                templatePath = basePath + "/YearReportTemplate.xls";
            }
            else if (type == "DD")
            {
                reportType = " 日报(" + date + ")";
                templatePath = basePath + "/DayReportTemplate.xls";
            }

            //根据模板生成Excel
            FileStream file = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
            HSSFWorkbook book = new HSSFWorkbook(file);
            HSSFSheet sheet = (HSSFSheet)book.GetSheet("Sheet1");

            //创建单元格格式：数字型单元格默认取小数点后两位
            ICellStyle style = book.CreateCellStyle();
            style.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");


            //获取当前建筑名
            IHomeDbContext homeContext = new HomeDbContext();
            BuildInfo build = homeContext.GetBuildById(buildId);

            Price price = context.GetPrice(buildId);
            double finalPrice;
            if (price != null)
                switch (energyCode)
                {
                    case "01000":
                        if (price.ElectriPrice != null)
                            finalPrice = Convert.ToDouble(price.ElectriPrice);
                        else
                            finalPrice = 1;
                        break;
                    case "02000":
                        if (price.WaterPrice != null)
                            finalPrice = Convert.ToDouble(price.WaterPrice);
                        else
                            finalPrice = 1;
                        break;
                    case "03000":
                        if (price.GasPrice != null)
                            finalPrice = Convert.ToDouble(price.GasPrice);
                        else
                            finalPrice = 1;
                        break;
                    default:
                        finalPrice = 1;
                        break;
                }
            else
                finalPrice = 1.0;

            //获取能源类型
            EnergyItemDict energyItem = reportContext.GetUnitByEnergyCode(energyCode);

            //string[] circuitIds = circuits.Split(',');

            List<ReportValue> data = regioncontext.GetReportValueList(energyCode, regionIds, date, type);
            var totalList = data.GroupBy(d => new { d.Time })
            .Select(group => new ReportValue
            {
                Id = "Total",
                Name = "总计",
                Time = group.Key.Time,
                Value = group.Sum(p => p.Value)
            }).ToList();

                data.AddRange(totalList);
            //设置Excel标题
            sheet.GetRow(0).GetCell(0).SetCellValue(build.BuildName + " 区域费用 " + reportType);

            //设置Excel中报表的能源类别
            sheet.GetRow(1).GetCell(1).SetCellValue(energyItem.EnergyItemName);
            //设置Excel中报表的能源单位
            sheet.GetRow(1).GetCell(5).SetCellValue("元");

            //根据传入circuitIds填充excel
            int rowId = 0;
            for (int i = 0; i < regionIds.Length+1; i++)
            {
                //使用lamda表达式筛选List中Id与传入的Id对应的仪表：一次填充一行Excel
                //List<ReportValue> current = data.FindAll(p => p.Id == regionIds[i]);
                List<ReportValue> current;
                if (i == regionIds.Length)
                    current = data.FindAll(p => p.Id == "Total");
                else
                    current = data.FindAll(p => p.Id == regionIds[i]);
                if (current.Count > 0)
                {
                    IRow row = sheet.CreateRow(rowId + 3);
                    row.CreateCell(0).SetCellValue(current[0].Name);
                    decimal total = 0;
                    switch (type)
                    {
                        case "DD":
                            foreach (var item in current)
                            {//遍历筛选出列表中当前回路中每个小时的数据，填充到Excel中
                                //当前行的第一列已经填充回路名称，根据时间设置向右偏移一位，将对应数据写入到当前单元格，并设置格式
                                if (item.Time != null)
                                {
                                    DateTime time = Convert.ToDateTime(item.Time);
                                    row.CreateCell(time.Hour + 1).SetCellValue((double)item.Value * finalPrice);
                                    row.GetCell(time.Hour + 1).CellStyle = style;
                                    total += Convert.ToDecimal(item.Value * (decimal?)finalPrice);
                                }

                            }
                            row.CreateCell(25).SetCellValue((double)total);
                            row.GetCell(25).CellStyle = style;
                            break;
                        case "MM":
                            foreach (var item in current)
                            {
                                if (item.Time != null)
                                {
                                    DateTime time = Convert.ToDateTime(item.Time);
                                    row.CreateCell(time.Day).SetCellValue((double)item.Value * finalPrice);
                                    row.GetCell(time.Day).CellStyle = style;
                                    total += Convert.ToDecimal(item.Value * (decimal?)finalPrice);
                                }

                            }
                            row.CreateCell(32).SetCellValue((double)total);
                            row.GetCell(32).CellStyle = style;
                            break;
                        case "YY":
                            foreach (var item in current)
                            {
                                if (item.Time != null)
                                {
                                    DateTime time = Convert.ToDateTime(item.Time);
                                    row.CreateCell(time.Month).SetCellValue((double)item.Value * finalPrice);
                                    row.GetCell(time.Month).CellStyle = style;
                                    total += Convert.ToDecimal(item.Value * (decimal?)finalPrice);
                                }
                            }
                            row.CreateCell(13).SetCellValue((double)total);
                            row.GetCell(13).CellStyle = style;
                            break;
                    }

                    rowId++;
                }
            }

            Excel excel = new Excel();

            excel.Name = build.BuildName + "区域费用" + reportType + "[" + Guid.NewGuid().ToString("N") + "].xls";

            using (MemoryStream stream = new MemoryStream())
            {
                book.Write(stream);
                stream.Seek(0, SeekOrigin.Begin);
                excel.Data = stream.ToArray();

            }

            return excel;
        }
    }
}
