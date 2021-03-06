﻿using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class RegionMainViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<EMSValue> CompareValues { get; set; }
        public List<RankValue> RankValues { get; set; }
        public List<EMSValue> PieValues { get; set; }
        public List<EMSValue> StackValues { get; set; }
    }
}
