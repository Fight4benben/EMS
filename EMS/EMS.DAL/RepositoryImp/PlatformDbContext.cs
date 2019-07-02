using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class PlatformDbContext : IPlatformDbContext
    {

        private EnergyDB _db = new EnergyDB();

        public int GetRunningDay()
        {
            return _db.Database.SqlQuery<int>(PlatformResource.SELECT_RunningDay).First();
        }

        public DeviceCount GetDeviceCount(string userName)
        {
            DeviceCount count = new DeviceCount();

            //SqlParameter[] sqlParameters ={
            //    new SqlParameter("@UserName",userName)
            //};

            count.Build = _db.Database.SqlQuery<int>(PlatformResource.SELECT_CountBuildID, new SqlParameter("@UserName", userName)).First();
            count.Collector = _db.Database.SqlQuery<int>(PlatformResource.SELECT_CountCollector, new SqlParameter("@UserName", userName)).First();
            count.Meter = _db.Database.SqlQuery<int>(PlatformResource.SELECT_CountMeter, new SqlParameter("@UserName", userName)).First();

            return count;
        }

        public List<PlatformItemValue> GetStandardcoalMonthList(string userName, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<PlatformItemValue>(PlatformResource.SELECT_StandardcoalMonthValue, sqlParameters).ToList();
        }

        public List<PlatformItemValue> GetDayList(string userName, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<PlatformItemValue>(PlatformResource.SELECT_DayValue, sqlParameters).ToList();
        }

        public List<PlatformItemValue> GetMonthList(string userName, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<PlatformItemValue>(PlatformResource.SELECT_MonthValue, sqlParameters).ToList();
        }

        public List<PlatformItemValue> GetYearList(string userName, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@EndDate",endDate)
            };

            return _db.Database.SqlQuery<PlatformItemValue>(PlatformResource.SELECT_YearValue, sqlParameters).ToList();
        }
    }
}
