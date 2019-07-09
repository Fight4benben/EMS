using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities.Setting
{
    public class MeterAlarmSet
    {
        public string BuildID { get; set; }
        public string MeterID { get; set; }
        public string ParamID { get; set; }
        public string ParamCode { get; set; }
        public string ParamName { get; set; }
        public int? State { get; set; }
        public int? Level { get; set; }
        public int? Delay { get; set; }
        public decimal? Lowest { get; set; }
        public decimal? Low { get; set; }
        public decimal? High { get; set; }
        public decimal? Highest { get; set; }
    }
}
