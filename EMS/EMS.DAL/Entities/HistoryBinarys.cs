using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class HistoryBinarys
    {
        public string MeterID { get; set; }
        public string CircuitName { get; set; }
        public string ParamName { get; set; }
        public byte[] Value { get; set; }
    }
}
