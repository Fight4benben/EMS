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
    public class EnergyItemStatisticDbContext : IEnergyItemStatisticDbContext
    {
        private EnergyDB _db = new EnergyDB();
        
        /// <summary>
        /// 获取当月计划用能数据
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<ReportValue> GetMonthPlanValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<ReportValue>(EnergyItemStatisticResources.MonthPlanValueSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取当年计划用能数据
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<ReportValue> GetYearPlanValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<ReportValue>(EnergyItemStatisticResources.YearPlanValueSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取当月实际用能数据
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<ReportValue> GetMonthRealValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<ReportValue>(EnergyItemStatisticResources.MonthRealValueSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取当年实际用能数据
        /// </summary>
        /// <param name="buildId">建筑ID </param>
        /// <param name="date">结束时间</param>
        /// <returns></returns>
        public List<ReportValue> GetYearRealValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<ReportValue>(EnergyItemStatisticResources.YearRealValueSQL, sqlParameters).ToList();
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
