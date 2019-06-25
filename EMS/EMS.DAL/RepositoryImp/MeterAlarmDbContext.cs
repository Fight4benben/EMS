using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using EMS.DAL.StaticResources.Circuit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class MeterAlarmDbContext : IMeterAlarmDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public int GetIsAlarming(string userName)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName)
            };

            return _db.Database.SqlQuery<int>(MeterAlarmResources.SELECT_IsAlarming, sqlParameters).First();
        }

        public PageInfo GetPageInfoList(string userName, int pageSize)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@PageSize",pageSize)
            };

            return _db.Database.SqlQuery<PageInfo>(MeterAlarmResources.SELECT_AlarmingMeterTotalPage, sqlParameters).First();
        }

        public List<MeterAlarmInfo> GetMeterAlarmingList(string userName, int pageIndex, int pageSize)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@PageIndex",pageIndex),
                new SqlParameter("@PageSize",pageSize)
            };

            return _db.Database.SqlQuery<MeterAlarmInfo>(MeterAlarmResources.SELECT_AlarmingMeter, sqlParameters).ToList();
        }

        /// <summary>
        /// 根据 用户，获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageSize"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public PageInfo GetAlarmLogPageInfo(string userName, int pageSize, string beginDate, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<PageInfo>(MeterAlarmResources.SELECT_AlarmLogByUserTotalPage, sqlParameters).First();
        }

        public List<MeterAlarmLog> GetAlarmLogList(string userName, int pageIndex, int pageSize, string beginDate, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@PageIndex",pageIndex),
                new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<MeterAlarmLog>(MeterAlarmResources.SELECT_AlarmLogByUser, sqlParameters).ToList();
        }

        /// <summary>
        /// 根据 用户，建筑ID，获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="buildID"></param>
        /// <param name="pageSize"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public PageInfo GetAlarmLogPageInfo(string userName, string buildID, int pageSize, string beginDate, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<PageInfo>(MeterAlarmResources.SELECT_AlarmLogByBuildIDTotalPage, sqlParameters).First();
        }

        public List<MeterAlarmLog> GetAlarmLogList(string userName, string buildID, int pageIndex, int pageSize, string beginDate, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@PageIndex",pageIndex),
                new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<MeterAlarmLog>(MeterAlarmResources.SELECT_AlarmLogByBuildID, sqlParameters).ToList();
        }


        /// <summary>
        /// 根据 用户，建筑ID，仪表ID,获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="buildID"></param>
        /// <param name="meterID"></param>
        /// <param name="pageSize"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public PageInfo GetAlarmLogPageInfo(string userName, string buildID, string meterID, int pageSize, string beginDate, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@MeterID",meterID),
                new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<PageInfo>(MeterAlarmResources.SELECT_AlarmLogByMeterIDTotalPage, sqlParameters).First();
        }

        public List<MeterAlarmLog> GetAlarmLogList(string userName, string buildID, string meterID, int pageIndex, int pageSize, string beginDate, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@MeterID",meterID),
                new SqlParameter("@PageIndex",pageIndex),
                new SqlParameter("@PageSize",pageSize),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<MeterAlarmLog>(MeterAlarmResources.SELECT_AlarmLogByMeterID, sqlParameters).ToList();
        }





        /// <summary>
        /// 报警确认
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="describe"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int SetConfirmMeterAlarm(string userName, string describe, string[] ids)
        {
            string sql = string.Format(MeterAlarmResources.UPDATE_ConfirmOne, "'" + string.Join("','", ids) + "'");

            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Describe",describe)
            };

            return _db.Database.ExecuteSqlCommand(sql, sqlParameters);
        }

        public int SetConfirmAllMeterAlarm(string userName, string describe)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Describe",describe)
            };

            return _db.Database.ExecuteSqlCommand(MeterAlarmResources.UPDATE_ConfirmAll, sqlParameters);
        }



    }
}
