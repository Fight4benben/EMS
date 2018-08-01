using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyPrice
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double? Price { get; set; }
    }
}
