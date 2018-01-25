using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.StaticResources;
using System.Data.SqlClient;

namespace EMS.DAL.RepositoryImp
{
    public class CircuitOverviewDbContext : ICircuitOverviewDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<CircuitValue> GetCircuitLoadValueList(string buildId, string circuitId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@CircuitID",circuitId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<CircuitValue>(CircuitOverviewResources.CircuitLoadSQL, sqlParameters).ToList();
        }

        public List<CircuitValue> GetCircuitMomValueList(string buildId, string circuitId, string date)
        {
            //throw new NotImplementedException();

            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EnergyItemCode",circuitId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<CircuitValue>(CircuitResources.CircuitSQL, sqlParameters).ToList();
        }

        public List<CircuitValue> GetCircuit48HoursValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }

        public List<CircuitValue> GetCircuit31DaysValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }

        public List<CircuitValue> GetCircuit12MonthValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }

        public List<CircuitValue> GetCircuit3YearValueList(string buildId, string circuitId, string date)
        {
            throw new NotImplementedException();
        }
    }
}
