using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class PlatformViewModel
    {
        public List<BuildMap> Builds { get; set; }
        public DeviceCount Device { get; set; }
        public int? RunningDay { get; set; }
        
        public List<PlatformItemValue> Standardcoal { get; set; }
        public List<PlatformItemValue> DayDate { get; set; }
        public List<PlatformItemValue> MonthDate { get; set; }
        public List<PlatformItemValue> YearDate { get; set; }

    }
}
