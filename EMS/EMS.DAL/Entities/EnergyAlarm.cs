using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyAlarm
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public decimal? LimitValue { get; set; }
        public decimal? DiffValue { get; set; }
        public decimal? Rate { get; set; }
    }
}
