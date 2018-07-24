using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class CircuitMeterInfo
    {
        public string CircuitID { get; set; }
        public string CircuitName { get; set; }
        public string MeterID { get; set; }
        public string MeterParamID { get; set; }
    }
}
