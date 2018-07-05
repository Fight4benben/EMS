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
    public class DepartmentOverviewDbContext : IDepartmentOverviewDbContext
    {
        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 获取分项用能日环比
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<DepartmentValue> GetMomDayValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<DepartmentValue>(DepartmentOverviewResources.MomDayValueSQL, sqlParameters).ToList();
        }

        public List<DepartmentValue> GetRankByYearValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<DepartmentValue>(DepartmentOverviewResources.YearValueSQL, sqlParameters).ToList();
        }

        public List<DepartmentValue> GetPlanValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<DepartmentValue>(DepartmentOverviewResources.YearPlanValueSQL, sqlParameters).ToList();
        }

        public List<DepartmentValue> GetLast31DayPieChartValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<DepartmentValue>(DepartmentOverviewResources.Last31DayPieChartValueSQL, sqlParameters).ToList();
        }

        public List<DepartmentValue> GetLast31DayValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<DepartmentValue>(DepartmentOverviewResources.Last31DayValueSQL, sqlParameters).ToList();
        }

       
    }
}
