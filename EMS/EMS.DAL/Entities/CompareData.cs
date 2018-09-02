﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class CompareData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime? Time { get; set; }
        public decimal? Value { get; set; }
        public decimal? LastValue { get; set; }
        public decimal? DiffValue { get; set; }
        public decimal? Rate { get; set; }
    }
}
