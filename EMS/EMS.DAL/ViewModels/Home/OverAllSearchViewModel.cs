using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class OverAllSearchViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<EMSValue> TimeData { get; set; }
        public List<CompareData> MomData { get; set; }
        public List<EnergyAverage> MonthAverageData { get; set; }
        public List<EnergyAverage> YearAverageData { get; set; }
    }
}
