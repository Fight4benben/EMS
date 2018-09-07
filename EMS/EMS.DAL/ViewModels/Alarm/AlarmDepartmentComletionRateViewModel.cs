using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class AlarmDepartmentComletionRateViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<DeptCompletionRate> DeptCompletionRate { get; set; }
        public List<CompareData> TotalCompareData { get; set; }
        public List<CompareData> AreaAvgCompareData { get; set; }

    }
}
