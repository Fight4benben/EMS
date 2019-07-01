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
    public class PlatformDbContext
    {

        private EnergyDB _db = new EnergyDB();

       
        public int GetList(string userName)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName)
            };

            return _db.Database.SqlQuery<int>(MeterAlarmResources.SELECT_IsAlarming, sqlParameters).First();
        }
    }
}
