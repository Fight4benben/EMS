using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class Price
    {
        public double? ElectriPrice { get; set; }
        public double? WaterPrice { get; set; }
        public double? GasPrice { get; set; }
        public double? HeatPrice { get; set; }
        public double? OtherPrice { get; set; }
        
    }
}
