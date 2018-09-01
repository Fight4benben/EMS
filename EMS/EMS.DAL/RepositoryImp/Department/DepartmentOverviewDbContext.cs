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
    public class DepartmentOverviewDbContext : IDepartmentOverviewDbContext,ICommonContext
    {
        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 获取部门用能日环比
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<EMSValue> GetMomDayValueList(string buildId, string date,string energyCode,string filterType)
        {
            string sql = GetSQL(DepartmentOverviewResources.MomDayValueSQL,filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<EMSValue> GetRankByYearValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(DepartmentOverviewResources.YearValueSQL, filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<EMSValue> GetPlanValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(DepartmentOverviewResources.YearPlanValueSQL, filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<EMSValue> GetLast31DayPieChartValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(DepartmentOverviewResources.Last31DayPieChartValueSQL, filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<EMSValue> GetLast31DayValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(DepartmentOverviewResources.Last31DayValueSQL, filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        private string GetSQL(string sql, string filter)
        {
            string sql1;
            switch (filter)
            {
                case "Demo":
                    sql1 = string.Format(sql, @" (DepartmentInfo.F_DepartParentID IN (
	                                                SELECT F_DepartmentID FROM T_ST_DepartmentInfo
	                                                WHERE F_BuildID = @BuildID
	                                                AND F_DepartParentID = '-1'
                                                ))");
                    break;
                case "Publish":
                    sql1 = string.Format(sql, " (DepartmentInfo.F_DepartParentID ='-1')");
                    break;
                default:
                    sql1 = string.Format(sql, " (DepartmentInfo.F_DepartParentID ='-1')");
                    break;

            }

            return sql1;
        }

        public BuildExtendInfo GetExtendInfoByBuildId(string buildId)
        {
            return _db.BuildExtendInfo.Find(buildId);
        }

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
