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
    public class AlarmDeviceFreeTimeDbContext
    {
        private EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 获取当前时间的设备用能越限数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<AlarmFreeTime> GetOverLimitValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",date)
            };
            return _db.Database.SqlQuery<AlarmFreeTime>(AlarmDeviceFreeTimeResources.GetAlarmDeviceOverLimitFreeTimeSQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取非工作时间 用能报警值临时表1
        /// </summary>
        public List<AlarmTempValue> GetOverLimitValueT1List(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",date)
            };
            return _db.Database.SqlQuery<AlarmTempValue>(AlarmDeviceFreeTimeResources.GetAlarmDeviceT1SQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取非工作时间 用能报警值临时表1
        /// </summary>
        public List<AlarmTempValue> GetOverLimitValueT2List(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@StartDay",date)
            };
            return _db.Database.SqlQuery<AlarmTempValue>(AlarmDeviceFreeTimeResources.GetAlarmDeviceT2SQL, sqlParameters).ToList();
        }

        /// <summary>
        /// 获取已设置的非工作时间，告警百分率 的设备
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public List<AlarmLimitValue> GetAlarmLimitValueList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            List<AlarmLimitValue> alarmLimitValues = _db.Database.SqlQuery<AlarmLimitValue>(AlarmDeviceFreeTimeResources.GetDeviceLimitValueListSQL, sqlParameters).ToList();
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
            return _db.Database.ExecuteSqlCommand(AlarmDeviceFreeTimeResources.SetDeviceOverLimitValueSQL, sqlParameters);
        }

        public int DeleteDeviceOverLimitValue(string buildId, string circuitID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@CircuitID",circuitID)
            };
            return _db.Database.ExecuteSqlCommand(AlarmDeviceFreeTimeResources.DeleteDeviceOverLimitValueSQL, sqlParameters);
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
        /// 获取未设置报警的支路列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public List<TreeViewInfo> GetUnSettingDeviceList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
            };
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(AlarmDeviceFreeTimeResources.UnSettingTreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
        }
    }
}
