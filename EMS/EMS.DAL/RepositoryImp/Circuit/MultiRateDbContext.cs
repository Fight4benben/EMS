using EMS.DAL.Entities;
using EMS.DAL.IRepository.Circuit;
using EMS.DAL.StaticResources;
using EMS.DAL.StaticResources.Circuit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp.Circuit
{
    public class MultiRateDbContext : IMultiRateDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<MultiRateData> GetReportValueList(string buildID, string code, string type, string date)
        {
            string sql;
            switch (type.ToUpper())
            {
                case "DD":
                    sql = MultiRateResources.MultiRateDaySQL + MultiRateResources.MultiRateDayGroup;
                    break;

                case "MM":
                    sql = MultiRateResources.MultiRateMonthSQL + MultiRateResources.MultiRateMonthGroup;
                    break;

                case "YY":
                    sql = MultiRateResources.MultiRateYearSQL + MultiRateResources.MultiRateYearGroup;
                    break;

                default:
                    sql = MultiRateResources.MultiRateMonthSQL + MultiRateResources.MultiRateMonthGroup;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@Code",code),
                new SqlParameter("@EndDate",date)
            };

            return _db.Database.SqlQuery<MultiRateData>(sql, sqlParameters).ToList();
        }

        public List<MultiRateData> GetReportValueList(string buildID, string code, string type, string date, string[] circuitIds)
        {
            string sql;
            string circuitIdsSql = string.Format(MultiRateResources.MultiRateIdsIN, "'" + string.Join("','", circuitIds) + "'");

            switch (type.ToUpper())
            {
                case "DD":
                    sql = MultiRateResources.MultiRateDaySQL + circuitIdsSql + MultiRateResources.MultiRateDayGroup;
                    break;

                case "MM":
                    sql = MultiRateResources.MultiRateMonthSQL + circuitIdsSql + MultiRateResources.MultiRateMonthGroup;
                    break;

                case "YY":
                    sql = MultiRateResources.MultiRateYearSQL + circuitIdsSql + MultiRateResources.MultiRateYearGroup;
                    break;

                default:
                    sql = MultiRateResources.MultiRateMonthSQL + circuitIdsSql + MultiRateResources.MultiRateMonthGroup;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@Code",code),
                new SqlParameter("@EndDate",date)
            };

            return _db.Database.SqlQuery<MultiRateData>(sql, sqlParameters).ToList();
        }


        public List<EMS.DAL.Entities.CircuitList> GetCircuitListByBIdAndEItemCode(string buildId, string energyItemCode)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EnergyItemCode",energyItemCode)
            };
            return _db.Database.SqlQuery<EMS.DAL.Entities.CircuitList>(CircuitResources.CircuitSQL, sqlParameters).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }
    }
}
