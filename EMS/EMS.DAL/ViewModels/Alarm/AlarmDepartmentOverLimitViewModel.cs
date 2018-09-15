﻿using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class AlarmDepartmentOverLimitViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<TreeViewInfo> UnsettingDept { get; set; }
        public List<AlarmLimitValue> AlarmLimitValues { get; set; }
        public List<EnergyAlarm> EnergyAlarmData { get; set; }
    }
}
