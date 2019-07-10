using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class ToxicGasesViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<MeterList> Devices { get; set; }
        public List<MeterValue> Data { get; set; }

    }
}
