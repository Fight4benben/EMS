using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyItemInfo
    {
        public string FormulaID { get; set; }
        public string EnergyItemName { get; set; }
        public string EnergyItemCode { get; set; }
        public string ParentItemCode { get; set; }
    }
}
