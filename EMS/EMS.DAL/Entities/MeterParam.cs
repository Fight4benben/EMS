using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class MeterParam
    {
        public string ParamID { get; set; }
        public string ParamCode { get; set; }
        public string ParamName { get; set; }
        public int ParamOrder { get; set; }
        public string ParamType { get; set; }
        public string ParamUnit { get; set; }
    }
}
