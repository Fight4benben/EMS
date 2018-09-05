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

        /// <summary>
        /// 最近31天用能数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<EMSValue> GetLast31DayList(string type, string keyWord, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitLast31DaySQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionLast31DaySQL;
                    break;

                default:
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }
        /// <summary>
        /// 本月每天用能数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<EMSValue> GetMonthList(string type, string keyWord, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitLast31DaySQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionLast31DaySQL;
                    break;

                default:
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }
        /// <summary>
        /// 环比数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<CompareData> GetMomMonthList(string type, string keyWord, string startDay, string endDay)
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
                    sql = OverAllSearchResources.DeptMomMonthSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(sql, sqlParameters).ToList();
        }
        /// <summary>
        /// 同比数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<CompareData> GetCompareMonthList(string type, string keyWord, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitCompareMonthSQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptCompareMonthSQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionCompareMonthSQL;
                    break;

                default:
                    sql = OverAllSearchResources.DeptCompareMonthSQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(sql, sqlParameters).ToList();
        }

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

    }
}
