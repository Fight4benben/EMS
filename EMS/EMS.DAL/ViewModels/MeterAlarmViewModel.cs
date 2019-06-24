using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class MeterAlarmViewModel
    {
        public PageInfo PageInfos { get; set; }
        public List<MeterAlarmInfo> Data { get; set; }
        public List<MeterAlarmLog> AlarmLogs { get; set; }

    }
}
