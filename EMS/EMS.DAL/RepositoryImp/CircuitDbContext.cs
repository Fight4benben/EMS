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
    public class CircuitDbContext : ICircuitDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<Circuit> GetCircuitListByBIdAndEItemCode(string buildId,string energyItemCode)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EnergyItemCode",energyItemCode)
            };
            return _db.Database.SqlQuery<Circuit>(CircuitResources.CircuitSQL,sqlParameters).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL,new SqlParameter("@BuildId",buildId)).ToList();
        }
    }
}
