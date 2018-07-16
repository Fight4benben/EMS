using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IEnergyItemStatisticDbContext 
    {
        List<ReportValue> GetMonthPlanValueList(string buildId, string date);
        List<ReportValue> GetYearPlanValueList(string buildId, string date);
        List<ReportValue> GetMonthRealValueList(string buildId, string date);
        List<ReportValue> GetYearRealValueList(string buildId, string date);
    }
}
