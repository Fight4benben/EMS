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
    public class CircuitReportService
    {
        private ICircuitReportDbContext context;

        public CircuitReportService()
        {
            context = new CircuitReportDbContext();
        }

        /// <summary>
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的回路，所有回路的日报表（时间是当日）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，树状结构，数据</returns>
        public CircuitReportViewModel GetViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);

            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode = energys.First().EnergyItemCode;
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId,energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId,energyCode);

            List<ReportValue> data = context.GetReportValueList(circuitIds,today.ToShortDateString(),"DD");

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.Builds = builds;
            circuitReportView.Energys = energys;
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = "DD";

            return circuitReportView;
        }
    
        /// <summary>
        /// 修改建筑名称返回对应数据
        /// </summary>
        /// <param name="buildId">传入建筑ID</param>
        /// <param name="type">传入报表类型</param>
        /// <param name="date">传入日期</param>
        /// <returns>返回信息不包含建筑列表（建筑列表已经填充）</returns>
        public CircuitReportViewModel GetViewModel(string buildId,string type,string date)
        {
            if (type == "MM")
            {
                date += "-01";
            }
            else if(type=="YY")
            {
                date += "-01-01";
            }
            DateTime now = Utils.Util.ConvertString2DateTime(date,"yyyy-MM-dd");
            List<EnergyItemDict> energys = context.GetEnergyItemDictByBuild(buildId);

            string energyCode = energys.First().EnergyItemCode;
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<ReportValue> data = context.GetReportValueList(circuitIds, now.ToShortDateString(), type);

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.Energys = energys;
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = type;

            return circuitReportView;
        }

        /// <summary>
        /// 建筑ID与能源类型，返回数据：建筑列表与能源分类按钮已经存在，不需要再次回传数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">能源类型</param>
        /// <param name="type">报表类型</param>
        /// <param name="date">日期</param>
        /// <returns>前端已有建筑列表与能源按钮列表，无需再次上传这些数据，只要上传树状结构数据，和报表数据</returns>
        public CircuitReportViewModel GetViewModel(string buildId,string energyCode, string type, string date)
        {
            if (type == "MM")
            {
                date += "-01";
            }
            else if (type == "YY")
            {
                date += "-01-01";
            }
            DateTime now = Utils.Util.ConvertString2DateTime(date, "yyyy-MM-dd");
           
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string[] circuitIds = GetCircuitIds(circuits);

            List<TreeViewModel> treeView = GetTreeListViewModel(buildId, energyCode);

            List<ReportValue> data = context.GetReportValueList(circuitIds, now.ToShortDateString(), type);

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.TreeView = treeView;
            circuitReportView.Data = data;
            circuitReportView.ReportType = type;

            return circuitReportView;
        }

        /// <summary>
        /// 传入建筑ID，能源类型，选中的回路编号
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">能源类型</param>
        /// <param name="circuits">一个或多个回路Id</param>
        /// <param name="type">报表类型</param>
        /// <param name="date">日期</param>
        /// <returns>只返回数据即可</returns>
        public CircuitReportViewModel GetViewModel(string buildId, string energyCode, string circuits,string type, string date)
        {
            if (type == "MM")
            {
                date += "-01";
            }
            else if (type == "YY")
            {
                date += "-01-01";
            }
            DateTime now = Utils.Util.ConvertString2DateTime(date, "yyyy-MM-dd");

            string[] circuitIds = circuits.Split(',');

            List<ReportValue> data = context.GetReportValueList(circuitIds, now.ToShortDateString(), type);

            CircuitReportViewModel circuitReportView = new CircuitReportViewModel();
            circuitReportView.Data = data;
            circuitReportView.ReportType = type;

            return circuitReportView;
        }

        //public CircuitReportViewModel GetViewModel(string buildId,string date,string type)
        //{
        //    DateTime today = Utils.Util.ConvertString2DateTime(date,"yyyy-MM-dd");
        //}
        /// <summary>
        /// 根据建筑ID，和能源类型获取树状结构
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyItemCode">能源类型</param>
        /// <returns>树状结构</returns>
        public List<TreeViewModel> GetTreeListViewModel(string buildId, string energyItemCode)
        {
            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode(buildId,energyItemCode);
            var parentCircuits = circuits.Where(c=>(c.ParentId=="-1"||string.IsNullOrEmpty(c.ParentId)));
            List<TreeViewModel> treeList = new List<TreeViewModel>();

            foreach (var item in parentCircuits)
            {
                TreeViewModel parentNode = new TreeViewModel();
                List<TreeViewModel>  children = GetChildrenNodes(circuits,item);
                parentNode.Id = item.CircuitId;
                parentNode.Text = item.CircuitName;
                if (children.Count != 0)
                    parentNode.Nodes = children;
                treeList.Add(parentNode);
            }

            return treeList;
        }
        
        /// <summary>
        /// 递归调用方式填充树状结构的子节点
        /// </summary>
        /// <param name="circuits"></param>
        /// <param name="circuit"></param>
        /// <returns></returns>
        List<TreeViewModel> GetChildrenNodes(List<Circuit> circuits, Circuit circuit)
        {
            string parentId = circuit.CircuitId;
            List<TreeViewModel> circuitList = new List<TreeViewModel>();
            var children = circuits.Where(c=>c.ParentId==parentId);

            foreach (var item in children)
            {
                TreeViewModel node = new TreeViewModel();
                node.Id = item.CircuitId;
                node.Text = item.CircuitName;
                if (GetChildrenNodes(circuits, item).Count != 0)
                    node.Nodes = GetChildrenNodes(circuits,item);

                circuitList.Add(node);
            }

            return circuitList;
        }

        string[] GetCircuitIds(List<Circuit> circuits)
        {
            List<string> list = new List<string>();
            foreach (var circuit in circuits)
            {
                list.Add(circuit.CircuitId);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 根据传入参数，将数据导出到Excel中（日报，月报，年报）
        /// </summary>
        /// <param name="basePath">App_Data</param>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="circuits"></param>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns>Excel实体类，包含Excel文件的byte数组与Excel文件的名称</returns>
        public Excel ExportCircuitReportToExcel(string basePath,string buildId, string energyCode, string[] circuits, string type, string date)
        {
            string templatePath=basePath +"/DayReportTemplate.xls";
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
            FileStream file = new FileStream(templatePath,FileMode.Open,FileAccess.Read);
            HSSFWorkbook book = new HSSFWorkbook(file);
            HSSFSheet sheet = (HSSFSheet)book.GetSheet("Sheet1");

            //创建单元格格式：数字型单元格默认取小数点后两位
            ICellStyle style = book.CreateCellStyle();
            style.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");


            //获取当前建筑名
            IHomeDbContext homeContext = new HomeDbContext();
            BuildInfo build = homeContext.GetBuildById(buildId);

            //获取能源类型
            EnergyItemDict energyItem = context.GetUnitByEnergyCode(energyCode);

            DateTime now = Utils.Util.ConvertString2DateTime(date, "yyyy-MM-dd");

            //string[] circuitIds = circuits.Split(',');

            List<ReportValue> data = context.GetReportValueList(circuits, now.ToShortDateString(), type);

            //设置Excel标题
            sheet.GetRow(0).GetCell(0).SetCellValue(build.BuildName+reportType);

            //设置Excel中报表的能源类别
            sheet.GetRow(1).GetCell(1).SetCellValue(energyItem.EnergyItemName);
            //设置Excel中报表的能源单位
            sheet.GetRow(1).GetCell(5).SetCellValue(energyItem.EnergyItemUnit);

            //根据传入circuitIds填充excel
            int rowId = 0;
            for (int i = 0; i < circuits.Length; i++)
            {
                //使用lamda表达式筛选List中Id与传入的Id对应的仪表：一次填充一行Excel
                List<ReportValue> current = data.FindAll(p=>p.Id== circuits[i]);
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
                                if(item.Time != null)
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

            excel.Name =build.BuildName+reportType+"["+Guid.NewGuid().ToString("N")+"].xls";

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
