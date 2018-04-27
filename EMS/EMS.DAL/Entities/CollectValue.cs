using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class CollectValue
    {
        public string Name { get; set; }
        public double StartValue { get; set; }
        public double EndValue { get; set; }
        public double DiffValue { get; set; }
    }
}
