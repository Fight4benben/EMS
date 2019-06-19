using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class MultiRateData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParamName { get; set; }
        public DateTime Time { get; set; }
        public decimal Value { get; set; }
        public decimal Cost { get; set; }
    }
}
