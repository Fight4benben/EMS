using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{

    public class DeptCompletionRate
    {
        public string BuildID { get; set; }
        public string EnergyCode { get; set; }
        public decimal? Rate { get; set; }
    }
}
