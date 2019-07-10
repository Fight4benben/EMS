using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class MeterValue
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ParamCode { get; set; }
        public string ParamName { get; set; }
        public string ParamUnit { get; set; }
        public decimal? Value { get; set; }
    }
}
