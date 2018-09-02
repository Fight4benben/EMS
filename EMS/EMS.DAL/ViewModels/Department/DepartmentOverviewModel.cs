using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class DepartmentOverviewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<EMSValue> MomDay { get; set; }
        public List<EMSValue> RankByYear { get; set; }
        public List<EMSValue> PlanValue { get; set; }
        public List<EMSValue> Last31DayPieChart { get; set; }
        public List<EMSValue> Last31Day { get; set; }

    }
}
