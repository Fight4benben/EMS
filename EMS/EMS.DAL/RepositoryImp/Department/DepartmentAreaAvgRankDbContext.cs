using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class DepartmentAreaAvgRankDbContext : IDepartmentAreaAvgRankDbContext
    {
        private EnergyDB _db = new EnergyDB();
        
        /// <summary>
        /// 部门-月份用能平均值
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<DeptAreaAvgRank> GetMonthRankList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<DeptAreaAvgRank>(DepartmentAreaAvgResources.AreaAvgMonthSQL, sqlParameters).ToList();

        }

        public List<DeptAreaAvgRank> GetQuarterRankList(string buildId, string energyCode, string startDay, string endDay)
        {
            throw new NotImplementedException();
        }

        public List<DeptAreaAvgRank> GetYearRankList(string buildId, string energyCode, string startDay, string endDay)
        {
            throw new NotImplementedException();
        }
    }
}
