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
