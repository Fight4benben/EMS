using EMS.DAL.Entities;
using EMS.DAL.StaticResources;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class HistoryDbContext
    {
        private EnergyDB _EMSdb = new EnergyDB();
        private HistoryDB _db = new HistoryDB();

        public List<HistoryValue> GetHistoryValues(string[] meterIds, string[] meterParamIds, DateTime time)
        {
            string month = time.Month.ToString("00");
            int day = time.Day;
            int hour = time.Hour;
            int minute = time.Minute;

            string meters = "('" + string.Join("','", meterIds) + "')";
            string meterparams = "('" + string.Join("','", meterParamIds) + "')";

            string sql = @"SELECT F_MeterID MeterID,F_MeterParamID MeterParamID, dbo.SelectBinarysToDoubleByDateOfFive(F_Month" + month +
                @"," + day + "," + hour + "," + minute + @") Value FROM HistoryData WITH(NOLOCK) WHERE F_Year = " + time.Year + " AND F_MeterID in" + meters + "" +
                " AND F_MeterParamID in" + meterparams + "";

            return _db.Database.SqlQuery<HistoryValue>(sql).ToList();
        }

        /// <summary>
        /// 获取指定EPI参数
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="circuitIDs"></param>
        /// <returns></returns>
        public List<CircuitMeterInfo> GetCircuitMeterInfoList(string buildId, string[] circuitIDs)
        {
            string sql = string.Format(CircuitCollectResources.CircuitInfoSQL, "'" + string.Join("','", circuitIDs) + "'");
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            return _EMSdb.Database.SqlQuery<CircuitMeterInfo>(sql, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取指定复费率参数
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="circuitIDs"></param>
        /// <returns></returns>
        public List<CircuitMeterInfo> GetMultiRateMeterInfoList(string buildId, string[] circuitIDs)
        {
            string sql = string.Format(CircuitCollectResources.MultiRateParamInfo, "'" + string.Join("','", circuitIDs) + "'");
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            return _EMSdb.Database.SqlQuery<CircuitMeterInfo>(sql, sqlParameters).ToList();
        }


        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _EMSdb.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _EMSdb.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }
    }
}
