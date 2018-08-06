using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using EMS.DAL.Utils;
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

        /// <summary>
        /// 获取每个参数的值
        /// </summary>
        /// <param name="circuitIDs">支路名称</param>
        /// <param name="meterParamIds">仪表参数</param>
        /// <param name="startTime">查询时间</param>
        /// <param name="step">时间间隔（分钟）</param>
        /// <returns></returns>
        public List<HistoryParameterValue> GetHistoryParamValue(string[] circuitIDs, string[] meterParamIds, DateTime startTime, int step)
        {
            List<HistoryParameterValue> historyValueList = new List<HistoryParameterValue>();

            List<HistoryBinarys> historyBinarys = GetHistoryBinaryString(circuitIDs, meterParamIds, startTime);

            foreach (var item in historyBinarys)
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    for (int minute = 0; minute < 56; minute = minute + step)
                    {
                        HistoryParameterValue historyValue = new HistoryParameterValue();
                        historyValue.ID = item.CircuitID;
                        historyValue.Name = item.CircuitName;
                        historyValue.ParamName = item.ParamName;

                        historyValue.Time = new DateTime(startTime.Year, startTime.Month, startTime.Day, hour, minute, 0);
                        double value = BinaryToDouble.GetParamValue(item.Value, startTime.Day, hour, minute);
                        if (-9999 != value)
                        {
                            historyValue.Value = value;
                            historyValueList.Add(historyValue);
                        }
                    }
                }
            }

            return historyValueList;
        }

        /// <summary>
        /// 获取原始数据
        /// </summary>
        /// <param name="meterIds"></param>
        /// <param name="meterParamIds"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<HistoryBinarys> GetHistoryBinaryString(string[] circuitIDs, string[] meterParamIds, DateTime time)
        {
            string month = time.Month.ToString("00");

            string circuits = "('" + string.Join("','", circuitIDs) + "')";
            string meterparams = "('" + string.Join("','", meterParamIds) + "')";

            string sql = @"SELECT Circuit.F_CircuitID AS CircuitID, F_CircuitName AS CircuitName
                                , ParamInfo.F_MeterParamName AS ParamName
                                , F_Month" + month +
                                @" AS Value FROM HistoryData WITH(NOLOCK)
                                INNER JOIN EMS.dbo.T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID=HistoryData.F_MeterID
	                            INNER JOIN EMS.dbo.T_ST_MeterParamInfo ParamInfo ON ParamInfo.F_MeterParamID= HistoryData.F_MeterParamID
                                WHERE F_Year = " + time.Year +
                                " AND Circuit.F_CircuitID in" + circuits + "" +
                                " AND HistoryData.F_MeterParamID in" + meterparams + " ORDER BY CircuitID ASC";

            return _db.Database.SqlQuery<HistoryBinarys>(sql).ToList();
        }

        /// <summary>
        /// 获取支路列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <returns></returns>
        public List<TreeViewInfo> GetTreeViewInfoList(string buildId, string energyCode)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            List<TreeViewInfo> treeViewInfos = _EMSdb.Database.SqlQuery<TreeViewInfo>(HistoryParamResources.TreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
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
