using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{

    public class DeptCompletionRate
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string BuildID { get; set; }
        public decimal? People { get; set; }
        public decimal? Area { get; set; }
        public decimal? Value { get; set; }
        public decimal? Rate { get; set; }
    }
}
