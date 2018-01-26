using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.StaticResources;
using System.Data.SqlClient;

namespace EMS.DAL.RepositoryImp
{
    public class CircuitOverviewDbContext : ICircuitOverviewDbContext
    {
        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 支路负荷曲线
        /// </summary>
        /// <param name="buildId">建筑编码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns></returns>
        public List<CircuitValue> GetCircuitLoadValueList(string buildId, string circuitId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@CircuitID",circuitId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<CircuitValue>(CircuitOverviewResources.CircuitLoadSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 支路当日环比数据
        /// </summary>
        /// <param name="buildId">建筑编码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns></returns>
        public List<CircuitValue> GetCircuitMomDayValueList(string buildId, string circuitId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@CircuitID",circuitId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<CircuitValue>(CircuitOverviewResources.CircuitMomDaySQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 支路当月环比数据
        /// </summary>
        /// <param name="buildId">建筑编码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns></returns>
        public List<CircuitValue> GetCircuitMomMonthValueList(string buildId, string circuitId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@CircuitID",circuitId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<CircuitValue>(CircuitOverviewResources.CircuitMomMonthSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 支路最近48小时用能数据
        /// </summary>
        /// <param name="buildId">建筑编码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns></returns>
        public List<CircuitValue> GetCircuit48HoursValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }

        public List<CircuitValue> GetCircuit31DaysValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }

        public List<CircuitValue> GetCircuit12MonthValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }

        public List<CircuitValue> GetCircuit3YearValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }
    }
}
