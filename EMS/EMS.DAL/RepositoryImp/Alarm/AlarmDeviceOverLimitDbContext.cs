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
            return _db.Database.SqlQuery<EnergyAlarm>(AlarmDeviceOverLimitResources.GetDeviceOverLimitValueSQL, sqlParameters).ToList();
        }

        public List<AlarmLimitValue> GetAlarmLimitValueList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            List<AlarmLimitValue> alarmLimitValues = _db.Database.SqlQuery<AlarmLimitValue>(AlarmDeviceOverLimitResources.GetDeviceLimitValueListSQL, sqlParameters).ToList();
            return alarmLimitValues;
        }

        public int SetDeviceOverLimitValue(string buildId, string circuitID, string startTime, string endTime, int isOverDay, decimal limitValue)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@CircuitID",circuitID),
                new SqlParameter("@StartTime",startTime),
                new SqlParameter("@EndTime",endTime),
                new SqlParameter("@isOverDay",isOverDay),
                new SqlParameter("@LimitValue",limitValue)
            };
            return _db.Database.ExecuteSqlCommand(AlarmDeviceOverLimitResources.SetDeviceOverLimitValueSQL, sqlParameters);
        }

        public int DeleteDeviceOverLimitValue(string buildId,string circuitID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@CircuitID",circuitID)
            };
            return _db.Database.ExecuteSqlCommand(AlarmDeviceOverLimitResources.DeleteDeviceOverLimitValueSQL, sqlParameters);
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
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(AlarmDeviceOverLimitResources.UnSettingTreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
        }

        /// <summary>
        /// 获取支路列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <returns></returns>
        public List<TreeViewInfo> GetUnSettingDeviceList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
            };
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(AlarmDeviceOverLimitResources.UnSettingTreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
        }
    }
}
