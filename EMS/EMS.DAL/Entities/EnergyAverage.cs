using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyAverage
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal? TotalArea { get; set; }
        public decimal? TotalPeople { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? AreaAvg { get; set; }
        public decimal? PeopleAvg { get; set; }
    }
}
