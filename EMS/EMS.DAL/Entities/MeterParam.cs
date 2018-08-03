using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class MeterParam
    {
        public string CircuitID { get; set; }
        public string ParamID { get; set; }
        public string ParamName { get; set; }
        public string ParaCode { get; set; }
        public string ParamUnit { get; set; }
        public string ParamType { get; set; }
    }
}
