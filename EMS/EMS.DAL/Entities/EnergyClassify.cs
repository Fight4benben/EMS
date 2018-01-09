using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyClassify
    {
        public string EnergyItemName { get; set; }
        public decimal? MonthValue { get; set; }
        public decimal? YearValue { get; set; }
        public string Unit { get; set; }
        public decimal? EnergyRate { get; set; }
    }
}
