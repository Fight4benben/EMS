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
    public class MapDbContext : IMapDbContext
    {
        private readonly EnergyDB _db = new EnergyDB();
        public List<BuildMap> GetBuildsLocationByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildMap>(MapResources.MapInfoSQL, new SqlParameter("@UserName", userName)).ToList();
        }
    }
}
