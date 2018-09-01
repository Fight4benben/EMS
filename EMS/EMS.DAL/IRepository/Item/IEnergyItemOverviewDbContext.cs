using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IEnergyItemOverviewDbContext
    {
        List<EnergyItemValue> GetEnergyItemMomDayValueList(string buildId, string date);
        List<EnergyItemValue> GetEnergyItemRankByMonthValueList(string buildId, string date);
        List<EnergyItemValue> GetEnergyItemLast31DayPieChartValueList(string buildId, string date);
        List<EnergyItemValue> GetEnergyItemLast31DayValueList(string buildId, string date);
    }
}
