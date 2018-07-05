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
        List<DepartmentValue> GetMomDayValueList(string buildId, string date);
        List<DepartmentValue> GetRankByYearValueList(string buildId, string date);
        List<DepartmentValue> GetPlanValueList(string buildId, string date);
        List<DepartmentValue> GetLast31DayPieChartValueList(string buildId, string date);
        List<DepartmentValue> GetLast31DayValueList(string buildId, string date);
    }
}
