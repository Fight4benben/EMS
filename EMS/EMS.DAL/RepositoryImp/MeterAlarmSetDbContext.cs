using EMS.DAL.Entities;
using EMS.DAL.Entities.Setting;
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
    public class MeterAlarmSetDbContext: IMeterAlarmSetDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<MeterAlarmSet> GetMeterParamList(string buildID, string circuitID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@CircuitID",circuitID)
            };

            return _db.Database.SqlQuery<MeterAlarmSet>(MeterAlarmSetResources.SELECT_MeterInfo, sqlParameters).ToList();
        }
    }
}
