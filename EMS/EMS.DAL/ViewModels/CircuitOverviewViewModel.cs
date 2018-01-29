using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class CircuitOverviewViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<TreeViewModel> TreeView { get; set; }
        public List<CircuitValue> LoadData { get; set; }
        public List<CircuitValue> MomDayData { get; set; }
        public List<CircuitValue> MomMonthData { get; set; }
        public List<CircuitValue> Last48HoursData { get; set; }
        public List<CircuitValue> Last31DaysData { get; set; }
        public List<CircuitValue> Last12MonthData { get; set; }
        public List<CircuitValue> Last3YearData { get; set; }
    }
}
