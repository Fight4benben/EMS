using EMS.DAL.Entities;
using EMS.DAL.IRepository;
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
    public class HistoryParamDbContext : IHistoryParamDbContext
    {
        private EnergyDB _EMSdb = new EnergyDB();
        private HistoryDB _db = new HistoryDB();

        public List<HistoryBinarys> GetHistoryBinaryString(string[] meterIds, string[] meterParamIds, DateTime time)
        {
            string month = time.Month.ToString("00");

            string meters = "('" + string.Join("','", meterIds) + "')";
            string meterparams = "('" + string.Join("','", meterParamIds) + "')";

            string sql = @"SELECT HistoryData.F_MeterID AS MeterID, F_CircuitName AS CircuitName
                                , ParamInfo.F_MeterParamName AS ParamName
                                , F_Month" + month +
                                @" AS Value FROM HistoryData WITH(NOLOCK)
                                INNER JOIN EMS.dbo.T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID=HistoryData.F_MeterID
	                            INNER JOIN EMS.dbo.T_ST_MeterParamInfo ParamInfo ON ParamInfo.F_MeterParamID= HistoryData.F_MeterParamID
                                WHERE F_Year = " + time.Year +
                                " AND HistoryData.F_MeterID in" + meters + "" +
                                " AND HistoryData.F_MeterParamID in" + meterparams + "";

            return _db.Database.SqlQuery<HistoryBinarys>(sql).ToList();
        }

        /// <summary>
        /// 获取仪表参数信息
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="circuitIDs">支路ID</param>
        /// <returns>支路对应仪表的参数</returns>
        public List<MeterParam> GetMeterParamInfo(string buildId, string[] circuitIDs)
        {
            string sql = string.Format(HistoryParamResources.MeterParamInfoSQL, "'" + string.Join("','", circuitIDs) + "'");
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            return _EMSdb.Database.SqlQuery<MeterParam>(sql, sqlParameters).ToList();
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
