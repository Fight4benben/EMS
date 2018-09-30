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
    public class OverAllSearchDbContext : IOverAllSearchDbContext
    {
        private EnergyDB _db = new EnergyDB();

        #region 用能趋势
        /// <summary>
        /// 获取 一天的 每个小时用能趋势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="buildID"></param>
        /// <param name="energyCode"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<EMSValue> GetDayList(string type, string keyWord, string buildID, string energyCode, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitDaySQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptDaySQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionDaySQL;
                    break;

                default:
                    sql = OverAllSearchResources.CircuitDaySQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取 一个月的 每天用能趋势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="buildID"></param>
        /// <param name="energyCode"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<EMSValue> GetMonthList(string type, string keyWord, string buildID, string energyCode, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitMonthSQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptMonthSQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionMonthSQL;
                    break;

                default:
                    sql = OverAllSearchResources.CircuitMonthSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取 一个季度的 每月用能趋势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="buildID"></param>
        /// <param name="energyCode"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<EMSValue> GetQuarterList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitQuarterSQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptQuarterSQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionQuarterSQL;
                    break;

                default:
                    sql = OverAllSearchResources.CircuitQuarterSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        #endregion

        #region 用能环比
        /// <summary>
        /// 获取 月份-环比
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="buildID"></param>
        /// <param name="energyCode"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<CompareData> GetMomMonthList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitMomMonthSQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptMomMonthSQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionMomMonthSQL;
                    break;

                default:
                    sql = OverAllSearchResources.CircuitMomMonthSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(sql, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取 季度-环比
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="buildID"></param>
        /// <param name="energyCode"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<CompareData> GetMomQuarterList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitMomQuarterSQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptMomQuarterSQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionMomMonthSQL;
                    break;

                default:
                    sql = OverAllSearchResources.CircuitMomQuarterSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(sql, sqlParameters).ToList();
        }

        #endregion

        #region 单位面积能耗-人均能耗

        /// <summary>
        /// 获取 月份-环比
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="buildID"></param>
        /// <param name="energyCode"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<EnergyAverage> GetMonthAverageList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitMonthAvgSQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptMonthAvgSQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionMonthAvgSQL;
                    break;

                default:
                    sql = OverAllSearchResources.CircuitMonthAvgSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EnergyAverage>(sql, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取 月份-环比
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="buildID"></param>
        /// <param name="energyCode"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<EnergyAverage> GetYearAverageList(string type, string keyWord, string buildID, string energyCode, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitYearAvgSQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptYearAvgSQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionYearAvgSQL;
                    break;

                default:
                    sql = OverAllSearchResources.CircuitYearAvgSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EnergyAverage>(sql, sqlParameters).ToList();
        }

        #endregion

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }
        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }
    }
}
