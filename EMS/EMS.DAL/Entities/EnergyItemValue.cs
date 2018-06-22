using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyItemValue
    {
        public string EnergyItemCode { get; set; }
        public string EnergyItemName { get; set; }
        public DateTime Time { get; set; }
        public decimal? Value { get; set; }
    }
}
