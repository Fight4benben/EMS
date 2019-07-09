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

        public int SetAlarmInfo(MeterAlarmSet setInfo)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",setInfo.BuildID),
                new SqlParameter("@MeterID",setInfo.MeterID),
                new SqlParameter("@ParamID",setInfo.ParamID),
                new SqlParameter("@ParamCode",setInfo.ParamCode),
                new SqlParameter("@State",setInfo.State),
                new SqlParameter("@Level",setInfo.Level),
                new SqlParameter("@Delay",setInfo.Delay),
                new SqlParameter("@Lowest",setInfo.Lowest),
                new SqlParameter("@Low",setInfo.Low),
                new SqlParameter("@High",setInfo.High),
                new SqlParameter("@Highest",setInfo.Highest)
            };

            return _db.Database.ExecuteSqlCommand(MeterAlarmSetResources.SELECT_SetAlarmInfo, sqlParameters);
        }

        public int DeleteParam(MeterAlarmSet setInfo)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",setInfo.BuildID),
                new SqlParameter("@MeterID",setInfo.MeterID),
                new SqlParameter("@ParamID",setInfo.ParamID)
            };

            return _db.Database.ExecuteSqlCommand(MeterAlarmSetResources.SELECT_DeleteOneParam, sqlParameters);
        }

        public int DeleteMeter(MeterAlarmSet setInfo)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",setInfo.BuildID),
                new SqlParameter("@MeterID",setInfo.MeterID)
            };

            return _db.Database.ExecuteSqlCommand(MeterAlarmSetResources.SELECT_DeleteOneMeter, sqlParameters);
        }
    }
}
