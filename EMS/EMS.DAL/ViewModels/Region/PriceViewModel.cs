using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class PriceViewModel
    {
        public List<EnergyPrice> EnergyPrice { get; set; }

        public RegionReportViewModel RegionReportModel { get; set; }
    }
}
