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
    public class CircuitOverviewService
    {
        private ICircuitOverviewDbContext context;
        private ICircuitReportDbContext reportContext = new CircuitReportDbContext();

        public CircuitOverviewService()
        {
            context = new CircuitOverviewDbContext();
        }


        /// <summary>
        /// 当日的用能概况
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的回路当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表，以及第一支路数据</returns>
        public CircuitOverviewViewModel GetCircuitOverviewViewModel(string userName)
        {
            DateTime today = DateTime.Now;
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(userName);
            string buildId = builds.First().BuildID;
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;
            List<CircuitList> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> loadData = context.GetCircuitLoadValueList(buildId, circuitId, today.ToString());
            List<CircuitValue> dayData = context.GetCircuitMomDayValueList(buildId, circuitId, today.ToString());
            List<CircuitValue> monthData = context.GetCircuitMomMonthValueList(buildId, circuitId, today.ToString());
            List<CircuitValue> last48HoursData = context.GetCircuit48HoursValueList(buildId, circuitId, today.ToString());
            List<CircuitValue> last31DayData = context.GetCircuit31DaysValueList(buildId, circuitId, today.ToString());
            List<CircuitValue> last12MonthData = context.GetCircuit12MonthValueList(buildId, circuitId, today.ToString());
            List<CircuitValue> last3YearData = context.GetCircuit3YearValueList(buildId, circuitId, today.ToString());

            CircuitOverviewViewModel circuitOverviewView = new CircuitOverviewViewModel();
            circuitOverviewView.Builds = builds;
            circuitOverviewView.Energys = energys;
            circuitOverviewView.Circuits = circuits;
            circuitOverviewView.LoadData = loadData;
            circuitOverviewView.MomDayData = dayData;
            circuitOverviewView.MomMonthData = monthData;
            circuitOverviewView.Last48HoursData = last48HoursData;
            circuitOverviewView.Last31DaysData = last31DayData;
            circuitOverviewView.Last12MonthData = last12MonthData;
            circuitOverviewView.Last3YearData = last3YearData;

            return circuitOverviewView;
        }


        /// <summary>
        /// 支路用能概况：
        /// 根据用户传入的建筑ID，查找该建筑包含的分类能耗，所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：包含能源按钮列表，回路列表，以及第一支路数据</returns>
        public CircuitOverviewViewModel GetCircuitOverviewViewModel(string buildId, string date)
        {
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;
            List<CircuitList> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> loadData = context.GetCircuitLoadValueList(buildId, circuitId, date);
            List<CircuitValue> dayData = context.GetCircuitMomDayValueList(buildId, circuitId, date);
            List<CircuitValue> monthData = context.GetCircuitMomMonthValueList(buildId, circuitId, date);
            List<CircuitValue> last48HoursData = context.GetCircuit48HoursValueList(buildId, circuitId, date);
            List<CircuitValue> last31DayData = context.GetCircuit31DaysValueList(buildId, circuitId, date);
            List<CircuitValue> last12MonthData = context.GetCircuit12MonthValueList(buildId, circuitId, date);
            List<CircuitValue> last3YearData = context.GetCircuit3YearValueList(buildId, circuitId, date);

            CircuitOverviewViewModel circuitOverviewView = new CircuitOverviewViewModel();
            circuitOverviewView.Energys = energys;
            circuitOverviewView.Circuits = circuits;
            circuitOverviewView.LoadData = loadData;
            circuitOverviewView.MomDayData = dayData;
            circuitOverviewView.MomMonthData = monthData;
            circuitOverviewView.Last48HoursData = last48HoursData;
            circuitOverviewView.Last31DaysData = last31DayData;
            circuitOverviewView.Last12MonthData = last12MonthData;
            circuitOverviewView.Last3YearData = last3YearData;

            return circuitOverviewView;
        }

        public CircuitOverviewViewModel GetCircuitOverviewViewModelByName(string buildId, string date,string name)
        {
            IHomeDbContext homeContext = new HomeDbContext();
            List<BuildViewModel> builds = homeContext.GetBuildsByUserName(name);
            List<EnergyItemDict> energys = reportContext.GetEnergyItemDictByBuild(buildId);
            string energyCode = energys.First().EnergyItemCode;
            List<CircuitList> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> loadData = context.GetCircuitLoadValueList(buildId, circuitId, date);
            List<CircuitValue> dayData = context.GetCircuitMomDayValueList(buildId, circuitId, date);
            List<CircuitValue> monthData = context.GetCircuitMomMonthValueList(buildId, circuitId, date);
            List<CircuitValue> last48HoursData = context.GetCircuit48HoursValueList(buildId, circuitId, date);
            List<CircuitValue> last31DayData = context.GetCircuit31DaysValueList(buildId, circuitId, date);
            List<CircuitValue> last12MonthData = context.GetCircuit12MonthValueList(buildId, circuitId, date);
            List<CircuitValue> last3YearData = context.GetCircuit3YearValueList(buildId, circuitId, date);

            CircuitOverviewViewModel circuitOverviewView = new CircuitOverviewViewModel();
            circuitOverviewView.Builds = builds;
            circuitOverviewView.Energys = energys;
            circuitOverviewView.Circuits = circuits;
            circuitOverviewView.LoadData = loadData;
            circuitOverviewView.MomDayData = dayData;
            circuitOverviewView.MomMonthData = monthData;
            circuitOverviewView.Last48HoursData = last48HoursData;
            circuitOverviewView.Last31DaysData = last31DayData;
            circuitOverviewView.Last12MonthData = last12MonthData;
            circuitOverviewView.Last3YearData = last3YearData;

            return circuitOverviewView;
        }


        /// <summary>
        /// 根据用户传入的建筑ID和分类能耗代码，
        /// 获取该建筑的分类能耗包含的所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗代码</param>
        /// <param name="date">用户名</param>
        /// <returns>返回完整的数据：包含回路列表，以及第一支路数据</returns>
        public CircuitOverviewViewModel GetCircuitOverviewViewModel(string buildId, string energyCode, string date)
        {
            
            List<CircuitList> circuits = reportContext.GetCircuitListByBIdAndEItemCode(buildId, energyCode);

            string circuitId = circuits.First().CircuitId;
            List<CircuitValue> loadData = context.GetCircuitLoadValueList(buildId, circuitId, date);
            List<CircuitValue> dayData = context.GetCircuitMomDayValueList(buildId, circuitId, date);
            List<CircuitValue> monthData = context.GetCircuitMomMonthValueList(buildId, circuitId, date);
            List<CircuitValue> last48HoursData = context.GetCircuit48HoursValueList(buildId, circuitId, date);
            List<CircuitValue> last31DayData = context.GetCircuit31DaysValueList(buildId, circuitId, date);
            List<CircuitValue> last12MonthData = context.GetCircuit12MonthValueList(buildId, circuitId, date);
            List<CircuitValue> last3YearData = context.GetCircuit3YearValueList(buildId, circuitId, date);

            CircuitOverviewViewModel circuitOverviewView = new CircuitOverviewViewModel();
            circuitOverviewView.Circuits = circuits;
            circuitOverviewView.LoadData = loadData;
            circuitOverviewView.MomDayData = dayData;
            circuitOverviewView.MomMonthData = monthData;
            circuitOverviewView.Last48HoursData = last48HoursData;
            circuitOverviewView.Last31DaysData = last31DayData;
            circuitOverviewView.Last12MonthData = last12MonthData;
            circuitOverviewView.Last3YearData = last3YearData;

            return circuitOverviewView;
        }



        /// <summary>
        /// 根据用户传入的建筑ID，分类能耗代码，支路编码
        /// 获取传入支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗代码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：包含传入支路编码的数据</returns>
        public CircuitOverviewViewModel GetCircuitOverviewViewModel(string buildId, string energyCode, string circuitId, string date)
        {
            List<CircuitValue> loadData = context.GetCircuitLoadValueList(buildId, circuitId, date);
            List<CircuitValue> dayData = context.GetCircuitMomDayValueList(buildId, circuitId, date);
            List<CircuitValue> monthData = context.GetCircuitMomMonthValueList(buildId, circuitId, date);
            List<CircuitValue> last48HoursData = context.GetCircuit48HoursValueList(buildId, circuitId, date);
            List<CircuitValue> last31DayData = context.GetCircuit31DaysValueList(buildId, circuitId, date);
            List<CircuitValue> last12MonthData = context.GetCircuit12MonthValueList(buildId, circuitId, date);
            List<CircuitValue> last3YearData = context.GetCircuit3YearValueList(buildId, circuitId, date);

            CircuitOverviewViewModel circuitOverviewView = new CircuitOverviewViewModel();
            circuitOverviewView.LoadData = loadData;
            circuitOverviewView.MomDayData = dayData;
            circuitOverviewView.MomMonthData = monthData;
            circuitOverviewView.Last48HoursData = last48HoursData;
            circuitOverviewView.Last31DaysData = last31DayData;
            circuitOverviewView.Last12MonthData = last12MonthData;
            circuitOverviewView.Last3YearData = last3YearData;

            return circuitOverviewView;
        }
    }
}
