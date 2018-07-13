using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IDepartmentOverviewDbContext
    {
        List<EMSValue> GetMomDayValueList(string buildId, string date);
        List<EMSValue> GetRankByYearValueList(string buildId, string date);
        List<EMSValue> GetPlanValueList(string buildId, string date);
        List<EMSValue> GetLast31DayPieChartValueList(string buildId, string date);
        List<EMSValue> GetLast31DayValueList(string buildId, string date);
    }
}
