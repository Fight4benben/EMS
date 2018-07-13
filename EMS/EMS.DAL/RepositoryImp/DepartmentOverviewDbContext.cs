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
        /// 获取部门用能日环比
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<EMSValue> GetMomDayValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EMSValue>(DepartmentOverviewResources.MomDayValueSQL, sqlParameters).ToList();
        }

        public List<EMSValue> GetRankByYearValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EMSValue>(DepartmentOverviewResources.YearValueSQL, sqlParameters).ToList();
        }

        public List<EMSValue> GetPlanValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EMSValue>(DepartmentOverviewResources.YearPlanValueSQL, sqlParameters).ToList();
        }

        public List<EMSValue> GetLast31DayPieChartValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EMSValue>(DepartmentOverviewResources.Last31DayPieChartValueSQL, sqlParameters).ToList();
        }

        public List<EMSValue> GetLast31DayValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EMSValue>(DepartmentOverviewResources.Last31DayValueSQL, sqlParameters).ToList();
        }

       
    }
}
