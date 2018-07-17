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
    public class RegionReportDbContext : IRegionReportDbContext
    {

        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 获取报表
        /// </summary>
        /// <param name="RegionIDs">区域ID</param>
        /// <param name="energyCode">分项用能编码</param>
        /// <param name="date">传入的日期</param>
        /// <param name="type">年月日类型：（DD：日报表，MM：月报表，YY:年报表）</param>
        /// <returns>List<ReportValue></returns>
        public List<ReportValue> GetReportValueList(string[] RegionIDs, string energyCode, string date, string type)
        {
            string sql;
            switch (type)
            {
                case "DD":
                    sql = string.Format(RegionReportResources.DayReportSQL, "'" + string.Join("','", RegionIDs) + "'");
                    break;
                case "MM":
                    sql = string.Format(RegionReportResources.MonthReportSQL, "'" + string.Join("','", RegionIDs) + "'");
                    break;
                case "YY":
                    sql = string.Format(RegionReportResources.YearReportSQL, "'" + string.Join("','", RegionIDs) + "'");
                    break;
                default:
                    sql = string.Format(RegionReportResources.DayReportSQL, "'" + string.Join("','", RegionIDs) + "'");
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<ReportValue>(sql, sqlParameters).ToList();

        }

        public List<TreeViewInfo> GetTreeViewInfoList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(RegionReportResources.TreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
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
