using EMS.DAL.Entities;
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
    public class AlarmDepartmentCompletionRateDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<DeptCompletionRate> GetDeptCompletionRateList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            return _db.Database.SqlQuery<DeptCompletionRate>(AlarmDepartmentCompletionRateResources.GetDeptCompletionRateSQL, sqlParameters).ToList();
        }

        public int SetDeptCompletionRateValue(string buildId, string energyCode, decimal completeRate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyCode",energyCode),
                new SqlParameter("@CompleteRate",completeRate)
            };
            return _db.Database.ExecuteSqlCommand(AlarmDepartmentCompletionRateResources.SetDeptCompletionRateSQL, sqlParameters);
        }

        /// <summary>
        /// 部门-月度 能耗总量同比大于 -2%（下降小于2%）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<CompareData> GetDeptTotalValueCompareMonthList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentCompletionRateResources.DeptCompareMonthRateSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetDeptTotalValueCompareQuarterList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentCompletionRateResources.DeptCompareQuarterRateSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetDeptTotalValueCompareYearList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentCompletionRateResources.DeptCompareYearRateSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 部门-月度 单位面积能耗 同比大于 -2%（下降小于2%）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public List<CompareData> GetDeptAreaAvgCompareMonthList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentCompletionRateResources.DeptAreaAvgCompareMonthRateSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetDeptAreaAvgCompareQuarterList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentCompletionRateResources.DeptAreaAvgCompareQuarterRateSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetDeptAreaAvgCompareYearList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentCompletionRateResources.DeptAreaAvgCompareYearRateSQL, sqlParameters).ToList();
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
