using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class HourValue
    {
        public string EnergyItemCode { get; set; }
        public DateTime ValueTime { get; set; }
        public decimal? Value { get; set; }
    }
}
