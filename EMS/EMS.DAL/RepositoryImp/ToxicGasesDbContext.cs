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
    public class ToxicGasesDbContext : IToxicGasesDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<MeterList> GetMeterList(string buildID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildID)
            };

            return _db.Database.SqlQuery<MeterList>(ToxicGasesResources.SELECT_MeterList, sqlParameters).ToList();
        }

        public List<MeterValue> GetOneMeterValue(string meterID, string buildID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@MeterID",meterID),
                new SqlParameter("@BuildID",buildID)
            };

            return _db.Database.SqlQuery<MeterValue>(ToxicGasesResources.SELECT_MeterValue, sqlParameters).ToList();
        }

        public List<MeterValue> GetMeterParamByMeterID(string meterID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@MeterID",meterID)
            };

            return _db.Database.SqlQuery<MeterValue>(ToxicGasesResources.SELECT_MeterValue, sqlParameters).ToList();
        }

    }
}
