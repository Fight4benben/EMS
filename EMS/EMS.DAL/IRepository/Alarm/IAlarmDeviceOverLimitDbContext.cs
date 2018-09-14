using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IAlarmDeviceOverLimitDbContext
    {
        List<EnergyAlarm> GetEnergyOverLimitValueList(string buildId, string date);
        //List<CompareData> GetDayMomValueList(string buildId, string startDay);
        //List<CompareData> GetMonthCompareValueList(string buildId, string startDay, string endDay);
        //List<CompareData> GetDeptCompareValueList(string buildId, string startDay, string endDay);
    }
}
