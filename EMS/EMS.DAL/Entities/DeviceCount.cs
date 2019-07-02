using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class DeviceCount
    {
        public int? Build { get; set; }
        public int? Collector { get; set; }
        public int? Meter { get; set; }
    }
}
