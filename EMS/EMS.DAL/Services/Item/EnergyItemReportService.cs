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
    public class EnergyItemReportService
    {
        private IEnergyItemReportDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();
        public EnergyItemReportService()
        {
            context = new EnergyItemReportDbContext();
        }

        /// <summary>
        /// 分项用能统计
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有分项当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，分项列表，以及第一分类的当日用能数据</returns>
        public EnergyItemReportViewModel GetEnergyItemReportViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, today.ToString(), "DD");

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Builds = builds;
            energyItemReportView.Energys = energys;
            energyItemReportView.TreeView = treeView;
            energyItemReportView.Data = reportValue;
            energyItemReportView.ReportType = "DD";

            return energyItemReportView;
        }

        public EnergyItemReportViewModel GetEnergyItemReportViewModelByBuild(string userName,string buildId)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);

            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, today.ToString(), "DD");

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Builds = builds;
            energyItemReportView.Energys = energys;
            energyItemReportView.TreeView = treeView;
            energyItemReportView.Data = reportValue;
            energyItemReportView.ReportType = "DD";

            return energyItemReportView;
        }

        /// <summary>
        /// 分项用能统计
        /// 根据建筑ID和日期，获取第一个分类对应的所有分项当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="date">时间</param>
        /// <returns>返回完整的数据：能源按钮列表，分项列表，以及第一分类的当日用能数据</returns>
        public EnergyItemReportViewModel GetEnergyItemReportViewModel(string buildId, string date)
        {
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, date, "DD");

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Energys = energys;
            energyItemReportView.TreeView = treeView;
            energyItemReportView.Data = reportValue;
            energyItemReportView.ReportType = "DD";

            return energyItemReportView;
        }
    
        /// <summary>
        /// 切换建筑时，根据建筑信息，报表类型，和日期传数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public EnergyItemReportViewModel GetEnergyItemReportViewModel(string buildId, string type, string date)
        {
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            IEnergyItemTreeViewDbContext energyItemtreeView = new EnergyItemTreeViewDbContext();
            List<TreeViewModel> treeView = energyItemtreeView.GetEnergyItemTreeViewList(buildId);

            List<EnergyItemInfo> EnergyItemInfos = energyItemtreeView.GetEnergyItemInfoList(buildId);
            string[] formulaIDs = GetEnergyItemCodes(EnergyItemInfos);
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, date, type);

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Energys = energys;
            energyItemReportView.TreeView = treeView;
            energyItemReportView.Data = reportValue;
            energyItemReportView.ReportType = type;

            return energyItemReportView;
        }

        /// <summary>
        /// 分项用能统计
        /// 根据建筑ID和日期，获取第一个分类对应的所有分项当日的用能概况
        /// </summary>
        /// <param name="formulaIDs">分项列表</param>
        /// <param name="date">时间</param>
        /// <param name="type">报表类型：DD:日报
        ///                            MM:月报
        ///                            YY:年报
        /// </param>
        /// <returns>返回完整的数据：能源按钮列表，分项列表，以及第一分类的当日用能数据</returns>
        public EnergyItemReportViewModel GetEnergyItemReportViewModel(string[] formulaIDs, string date, string type)
        {
            if (type.ToUpper() == "MM")
            {
                date += "-01";
            }
            else if (type.ToUpper() == "YY")
            {
                date += "-01-01";
            }
            List<ReportValue> reportValue = context.GetReportValueList(formulaIDs, date, type);

            EnergyItemReportViewModel energyItemReportView = new EnergyItemReportViewModel();
            energyItemReportView.Data = reportValue;
            energyItemReportView.ReportType = type.ToUpper();

            return energyItemReportView;
        }



        string[] GetEnergyItemCodes(List<EnergyItemInfo> EnergyItemInfos)
        {
            List<string> list = new List<string>();
            foreach (var Item in EnergyItemInfos)
            {
                list.Add(Item.FormulaID);
            }

            return list.ToArray();
        }

        public Excel ExportReportToExcel(string basePath, string buildId,string[] formulaIDs, string date, string type)
        {
            type = type.ToUpper();
            string templatePath = basePath + "/DayReportTemplate.xls";
            string reportType = " 日报(" + date + ")";
            if (type == "MM")
            {
                reportType = " 月报(" + date + ")";
                date += "-01";
                templatePath = basePath + "/MonthReportTemplate.xls";
            }
            else if (type == "YY")
            {
                reportType = " 年报(" + date + ")";
                date += "-01-01";
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

            //获取能源类型
            EnergyItemDict energyItem = reportContext.GetUnitByEnergyCode("01000");

            DateTime now = Utils.Util.ConvertString2DateTime(date, "yyyy-MM-dd");

            //string[] circuitIds = circuits.Split(',');

            List<ReportValue> data = context.GetReportValueList(formulaIDs, now.ToShortDateString(), type);

            //设置Excel标题
            sheet.GetRow(0).GetCell(0).SetCellValue(build.BuildName + "分项用能" + reportType);

            //设置Excel中报表的能源类别
            sheet.GetRow(1).GetCell(1).SetCellValue(energyItem.EnergyItemName);
            //设置Excel中报表的能源单位
            sheet.GetRow(1).GetCell(5).SetCellValue(energyItem.EnergyItemUnit);

            //根据传入circuitIds填充excel
            int rowId = 0;
            for (int i = 0; i < formulaIDs.Length; i++)
            {
                //使用lamda表达式筛选List中Id与传入的Id对应的仪表：一次填充一行Excel
                List<ReportValue> current = data.FindAll(p => p.Id == formulaIDs[i]);
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
                                    row.CreateCell(time.Hour + 1).SetCellValue((double)item.Value);
                                    row.GetCell(time.Hour + 1).CellStyle = style;
                                    total += Convert.ToDecimal(item.Value);
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
                                    row.CreateCell(time.Day).SetCellValue((double)item.Value);
                                    row.GetCell(time.Day).CellStyle = style;
                                    total += Convert.ToDecimal(item.Value);
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
                                    row.CreateCell(time.Month).SetCellValue((double)item.Value);
                                    row.GetCell(time.Month).CellStyle = style;
                                    total += Convert.ToDecimal(item.Value);
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

            excel.Name = build.BuildName+"分项用能" + reportType + "[" + Guid.NewGuid().ToString("N") + "].xls";

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
