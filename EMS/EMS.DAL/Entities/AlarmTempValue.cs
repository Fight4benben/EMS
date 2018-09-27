using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class AlarmTempValue
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TimePeriod { get; set; }
        public DateTime? Time { get; set; }
        public decimal? Value { get; set; }
    }
}
