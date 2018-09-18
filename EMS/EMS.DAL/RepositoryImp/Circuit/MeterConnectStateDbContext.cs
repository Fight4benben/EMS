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
    public class MeterConnectStateDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<ConnectState> GetMeterConnectStateList(string buildId, string energyCode)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@CircuitID",energyCode)
            };
            return _db.Database.SqlQuery<ConnectState>(MeterConnectStateResources.MeterAllStateSQL, sqlParameters).ToList();
        }
    }
}
