using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class BuildMap
    {
        public string BuildID { get; set; }
        public string BuildName { get; set; }
        public decimal? BuildLong { get; set; }
        public decimal? BuildLat { get; set; }
    }
}
