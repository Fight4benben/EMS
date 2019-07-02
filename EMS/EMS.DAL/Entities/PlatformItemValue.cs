﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class PlatformItemValue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public decimal? LastValue { get; set; }
        public string Unit { get; set; }

    }
}
