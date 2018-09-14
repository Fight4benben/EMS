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
    public class AlarmDeviceOverLimitDbContext : IAlarmDeviceOverLimitDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EnergyAlarm> GetEnergyOverLimitValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",date)
            };
            return _db.Database.SqlQuery<EnergyAlarm>(AlarmDeviceOverLimitResources.OverLimitValueSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetDayMomValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",date)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDeviceOverLimitResources.DayCompareValueSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetMonthMomValueList(string buildId, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDeviceOverLimitResources.MonthMomValueSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetMonthCompareValueList(string buildId, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDeviceOverLimitResources.MonthCompareValueSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetDeptMomValueList(string buildId, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDeviceOverLimitResources.DeptMomValueSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetDeptCompareValueList(string buildId, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDeviceOverLimitResources.DeptCompareValueSQL, sqlParameters).ToList();
        }



        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
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
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(HistoryParamResources.TreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
        }
    }
}
