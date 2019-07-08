using EMS.DAL.Entities;
using EMS.DAL.Entities.Setting;
using System.Collections.Generic;

namespace EMS.DAL.ViewModels
{
    public class MeterAlarmSetViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<TreeViewModel> TreeView { get; set; }
        public List<MeterAlarmSet> Data { get; set; }
    }
}
