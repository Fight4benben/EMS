﻿using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class DepartmentEnergyAverageViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<TreeViewModel> TreeView { get; set; }
        public List<EnergyAverage> AverageData { get; set; }
        public List<CompareData> CompareData { get; set; }
    }
}
