using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class HistoryParamViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemDict> Energys { get; set; }
        public List<TreeViewModel> TreeView { get; set; }
        public List<TreeViewInfo> TreeList { get; set; }
        public List<MeterParam> MeterParam { get; set; }
        public List<HistoryParameterValue> Data { get; set; }
    }
}
