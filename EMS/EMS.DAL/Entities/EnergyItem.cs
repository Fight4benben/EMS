using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyItem
    {
        public string BuildID { get; set; }
        public string EnergyItemCode { get; set; }
        public string EnergyItemName { get; set; }
        public decimal? Value { get; set; }
    }
}
