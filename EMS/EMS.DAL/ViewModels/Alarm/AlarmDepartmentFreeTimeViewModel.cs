using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class AlarmDepartmentFreeTimeViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<TreeViewInfo> UnSettingDevices { get; set; }
        public List<AlarmLimitValue> AlarmLimitValues { get; set; }
        public List<AlarmFreeTime> EnergyAlarmData { get; set; }
    }
}
