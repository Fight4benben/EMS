using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class NoWorkDay
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public decimal? Work { get; set; }
        public decimal? NoWork { get; set; }
    }
}
