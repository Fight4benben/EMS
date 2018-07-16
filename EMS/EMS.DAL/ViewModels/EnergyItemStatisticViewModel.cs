using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class EnergyItemStatisticViewModel
    {
        public List<ReportValue> MonthPlanData { get; set; }
        public List<ReportValue> YearPlanData { get; set; }
        public List<ReportValue> MonthRealData { get; set; }
        public List<ReportValue> YearRealData { get; set; }
        public List<EnergyItemDict> EnergyUnit { get; set; }
    }
}
