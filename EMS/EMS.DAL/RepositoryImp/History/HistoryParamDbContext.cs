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
        /// 获取单个支路的每个参数的值
        /// </summary>
        /// <param name="circuitID">支路编码</param>
        /// <param name="meterParamIds">参数编码</param>
        /// <param name="dateTime">查询时间（"yyyy-MM-dd"）</param>
        /// <param name="step">时间间隔（分钟）</param>
        /// <returns></returns>
        public List<HistoryParameterValue> GetParamValue(string circuitID, string[] meterParamIds, string dateTime, int step)
        {
            Acrel.HisDB.GetData getData = new Acrel.HisDB.GetData();
            List<HistoryParameterValue> historyValueList = new List<HistoryParameterValue>();
            DateTime nowTime = DateTime.Now;
            DateTime endTime;

            DateTime startTime = Util.ConvertString2DateTime(dateTime, "yyyy-MM-dd");

            //如果查询是的时间是今天，则结束时间为小于当前时间的为5的倍数的时间
            if (nowTime.Day == startTime.Day && nowTime.Month == startTime.Month && nowTime.Year == startTime.Year)
            {
                endTime = startTime.AddHours(nowTime.Hour).AddMinutes(nowTime.Minute - nowTime.Minute % 5 - 5);
            }
            else
            {
                //获取某天的23:55:00
                endTime = startTime.AddDays(1).AddMinutes(-5);
            }

            List<HistoryBinarys> historyBinarys = GetHistoryBinaryString(circuitID, meterParamIds, startTime);

            foreach (HistoryBinarys item in historyBinarys)
            {
                HistoryParameterValue historyValue = new HistoryParameterValue();
                historyValue.ID = item.CircuitID;
                historyValue.Name = item.CircuitName;
                historyValue.ParamName = item.ParamName;
                historyValue.Values = ConvertDicToList(getData.GetContinueBytesOfFive(item.Value, startTime, endTime, step));

                historyValueList.Add(historyValue);
            }

            return historyValueList;
        }

        public List<TimeValue> ConvertDicToList(Dictionary<DateTime, double> dic)
        {
            List<TimeValue> resultList = new List<TimeValue>();

            foreach (var item in dic)
            {
                TimeValue value = new TimeValue();
                value.Time = item.Key;
                value.Value = decimal.Round(Convert.ToDecimal(item.Value), 2);
                resultList.Add(value);
            }
            return resultList;
        }


        /// <summary>
        /// 获取原始数据
        /// </summary>
        /// <param name="circuitID"></param>
        /// <param name="meterParamIds"></param>
        /// <param name="time">时间格式："yyyy-MM"</param>
        /// <returns></returns>
        public List<HistoryBinarys> GetHistoryBinaryString(string circuitID, string[] meterParamIds, DateTime time)
        {
            string month = time.Month.ToString("00");


            string meterparams = "('" + string.Join("','", meterParamIds) + "')";

            string sql = @"SELECT Circuit.F_CircuitID AS CircuitID, F_CircuitName AS CircuitName
                                , ParamInfo.F_MeterParamName AS ParamName
                                , F_Month" + month +
                                @" AS Value FROM HistoryData WITH(NOLOCK)
                                INNER JOIN EMS.dbo.T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID=HistoryData.F_MeterID
	                            INNER JOIN EMS.dbo.T_ST_MeterParamInfo ParamInfo ON ParamInfo.F_MeterParamID= HistoryData.F_MeterParamID
                                WHERE F_Year = " + time.Year +
                                " AND Circuit.F_CircuitID = '" + circuitID + "' " +
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
        /// 获取仪表参数分类信息
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="circuitIDs">支路ID</param>
        /// <returns>支路对应仪表的参数分类</returns>
        public List<ParamClassify> GetMeterParamClassify(string buildId, string circuitID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@CircuitID",circuitID)
            };
            return _EMSdb.Database.SqlQuery<ParamClassify>(HistoryParamResources.ParamClassifySQL, sqlParameters).ToList();
        }
        /// <summary>
        /// 获取仪表所有参数信息
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="circuitIDs">支路ID</param>
        /// <returns>支路对应仪表的所有参数</returns>
        public List<MeterParam> GetMeterParam(string buildId, string circuitID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@CircuitID",circuitID)
            };
            return _EMSdb.Database.SqlQuery<MeterParam>(HistoryParamResources.MeterParamSQL, sqlParameters).ToList();
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
