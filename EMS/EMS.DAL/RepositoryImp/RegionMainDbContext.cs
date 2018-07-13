using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.StaticResources;
using System.Data.SqlClient;
using EMS.DAL.ViewModels;

namespace EMS.DAL.RepositoryImp
{
    public class RegionMainDbContext : IRegionMainDbContext,ICommonContext
    {
        EnergyDB _db = new EnergyDB();

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }

        public BuildExtendInfo GetExtendInfoByBuildId(string buildId)
        {
            return _db.BuildExtendInfo.Find(buildId);
        }

        public List<EMSValue> GetRegionMainCompareValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(RegionMainResources.DayCompareSQL,filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@FDate",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<RankValue> GetRegionMainRankValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(RegionMainResources.RankSQL, filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@FDate",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<RankValue>(sql, sqlParameters).ToList();
        }

        public List<EMSValue> GetRegionPieValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(RegionMainResources.PieSQL, filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@FDate",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<EMSValue> GetRegionStackValueList(string buildId, string date, string energyCode, string filterType)
        {
            string sql = GetSQL(RegionMainResources.BarTrendSQL, filterType);
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@FDate",date),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        private string GetSQL(string sql,string filterType)
        {
            string sql1;
            switch (filterType)
            {
                case "Demo":
                    sql1 = string.Format(sql, @" (T_ST_Region.F_RegionParentID =(SELECT F_RegionID FROM T_ST_Region
                                            WHERE F_RegionParentID = '-1'
                                            AND F_BuildID = @BuildID))");
                    break;
                case "Publish":
                    sql1 = string.Format(sql, " (T_ST_Region.F_RegionParentID ='-1')");
                    break;
                default:
                    sql1 = string.Format(sql, " (T_ST_Region.F_RegionParentID ='-1')");
                    break;

            }

            return sql1;
        }
    }
}
