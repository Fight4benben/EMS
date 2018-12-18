﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class DeptAreaAvgRank
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal? TotalArea { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? AreaAvg { get; set; }
    }
}
