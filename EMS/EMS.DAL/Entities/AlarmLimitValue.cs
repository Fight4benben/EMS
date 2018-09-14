using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class AlarmLimitValue
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? IsOverDay { get; set; }
        public string EnergyCode { get; set; }
        public decimal? LimitValue { get; set; }
    }
}
