using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IDepartmentAreaAvgRankDbContext
    {
        List<DeptAreaAvgRank> GetMonthRankList(string buildId, string energyCode, string startDay, string endDay);
        List<DeptAreaAvgRank> GetQuarterRankList(string buildId, string energyCode, string startDay, string endDay);
        List<DeptAreaAvgRank> GetYearRankList(string buildId, string energyCode, string startDay, string endDay);
    }
}
