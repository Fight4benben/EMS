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
    class CircuitCompareDbContext : ICircuitCompareDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<CircuitValue> GetCircuitCompareValueList(string buildId, string circuitId, string date)
        {
            //throw new NotImplementedException();
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@CircuitID",circuitId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<CircuitValue>(CircuitCompareResources.CircuitCompareSQL, sqlParameters).ToList();
        }
    }
}
