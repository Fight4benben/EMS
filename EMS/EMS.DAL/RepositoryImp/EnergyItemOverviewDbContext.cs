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
    public class EnergyItemOverviewDbContext:IEnergyItemOverviewDbContext
    {
        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 获取分项用能日环比
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<EnergyItemValue> GetEnergyItemMomDayValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EnergyItemValue>(EnergyItemOverviewResources.EnergyItemMomDaySQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取分项用能排名
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<EnergyItemValue> GetEnergyItemRankByMonthValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EnergyItemValue>(EnergyItemOverviewResources.EnergyItemRankByMonthSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取分项用能最近31天饼图
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<EnergyItemValue> GetEnergyItemLast31DayPieChartValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EnergyItemValue>(EnergyItemOverviewResources.EnergyItemLast31DayPieChartSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取分项用能最近31天用能值
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<EnergyItemValue> GetEnergyItemLast31DayValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EnergyItemValue>(EnergyItemOverviewResources.EnergyItemLast31DayValueSQL, sqlParameters).ToList();
        }
    }
}
