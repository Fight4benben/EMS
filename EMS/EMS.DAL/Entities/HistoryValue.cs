using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class HistoryValue
    {
        public string MeterID { get; set; }
        public string MeterParamID { get; set; }
        public double Value { get; set; }
    }
}
