using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class Circuit
    {
        public string CircuitId { get; set; }
        public string CircuitName { get; set; }
        public string MeterId { get; set; }
        public string ParentId { get; set; }
    }
}
