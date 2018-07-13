using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class RankValue
    {
        public string ClassifyID { get; set; }
        public string ClassifyName { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
    }
}
