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
        public List<DepartmentValue> MomDay { get; set; }
        public List<DepartmentValue> RankByYear { get; set; }
        public List<DepartmentValue> PlanValue { get; set; }
        public List<DepartmentValue> Last31DayPieChart { get; set; }
        public List<DepartmentValue> Last31Day { get; set; }
    }
}
