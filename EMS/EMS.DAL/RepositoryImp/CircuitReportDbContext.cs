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
    public class CircuitReportDbContext : ICircuitReportDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<Circuit> GetCircuitListByBIdAndEItemCode(string buildId,string energyItemCode)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EnergyItemCode",energyItemCode)
            };
            return _db.Database.SqlQuery<Circuit>(CircuitResources.CircuitSQL,sqlParameters).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL,new SqlParameter("@BuildId",buildId)).ToList();
        }

        /// <summary>
        /// 获取报表
        /// </summary>
        /// <param name="circuits">查询的回路</param>
        /// <param name="date">传入的日期</param>
        /// <param name="type">年月日类型：（DD：日报表，MM：月报表，YY:年报表）</param>
        /// <returns>List<ReportValue></returns>
        public List<ReportValue> GetReportValueList(string[] circuits,string date,string type)
        {
            string sql;
            switch (type)
            {
                case "DD":
                    sql = string.Format(CircuitResources.CircuitsDayReportSQL, "'" + string.Join("','", circuits) + "'");
                    break;
                case "MM":
                    sql = string.Format(CircuitResources.CircuitMonthReportSQL, "'" + string.Join("','", circuits) + "'");
                    break;
                case "YY":
                    sql = string.Format(CircuitResources.CircuitYearReportSQL, "'" + string.Join("','", circuits) + "'");
                    break;
                default:
                    sql = string.Format(CircuitResources.CircuitsDayReportSQL, "'" + string.Join("','", circuits) + "'");
                    break;
            }
             

            return _db.Database.SqlQuery<ReportValue>(sql,new SqlParameter("@EndDate",date)).ToList();
        }
    }
}
