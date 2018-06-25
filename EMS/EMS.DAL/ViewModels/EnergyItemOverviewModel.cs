using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class EnergyItemOverviewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<EnergyItemValue> EnergyItemMomDay { get; set; }
        public List<EnergyItemValue> EnergyItemRankByMonth { get; set; }
        public List<EnergyItemValue> EnergyItemLast31DayPieChart { get; set; }
        public List<EnergyItemValue> EnergyItemLast31Day { get; set; }
    }
}
