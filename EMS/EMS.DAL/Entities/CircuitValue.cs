using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class CircuitValue
    {
        public string CircuitId { get; set; }
        public string EnergyItemCode { get; set; }
        public DateTime Time { get; set; }
        public decimal? Value { get; set; }
    }
}
