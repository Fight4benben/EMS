using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class AlarmDepartmentViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<BuildAlarmLevel> BuildAlarmLevels { get; set; }
        public List<CompareData> CompareDatas { get; set; }
    }
}
