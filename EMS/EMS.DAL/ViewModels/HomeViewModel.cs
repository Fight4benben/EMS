using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class EnergyClassifyViewModel
    {
        public string EnergyItemName { get; set; }
        public float? MonthValue { get; set; }
        public float? YearValue { get; set; }
        public string Unit { get; set; }
    }
}
