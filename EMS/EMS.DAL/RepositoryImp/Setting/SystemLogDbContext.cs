using EMS.DAL.Entities;
using EMS.DAL.StaticResources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class SystemLogDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<LogInfo> GetSystemLogList(string startDay,string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };

            return _db.Database.SqlQuery<LogInfo>(SystemLogResources.GetSystemLog, sqlParameters).ToList();
        }
    }
}
