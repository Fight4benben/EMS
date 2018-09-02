using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class BuildAlarmLevel
    {
        public string BuildID { get; set; }
        public string EnergyCode { get; set; }
        public decimal? Level1 { get; set; }
        public decimal? Level2 { get; set; }
    }
}
