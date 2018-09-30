using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IOverAllSearchDbContext
    {
        List<EMSValue> GetDayList(string type, string keyWord, string buildID, string energyCode, string endDay);
        List<EMSValue> GetMonthList(string type, string keyWord, string buildID, string energyCode, string endDay);
        List<EMSValue> GetQuarterList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay);

        List<CompareData> GetMomMonthList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay);
        List<CompareData> GetMomQuarterList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay);

        List<EnergyAverage> GetMonthAverageList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay);
        List<EnergyAverage> GetYearAverageList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay);
    }
}
