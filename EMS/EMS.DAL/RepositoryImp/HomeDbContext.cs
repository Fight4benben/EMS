using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;

namespace EMS.DAL.RepositoryImpl
{
    public class HomeDbContext:IHomeDbContext
    {
        private readonly EnergyDB _db = new EnergyDB();

        public BuildInfo GetBuildById(string buildId)
        {
            return _db.BuildInfo.Find(buildId);
        }

        public IQueryable<BuildInfo> GetBuilds()
        {
            return _db.BuildInfo;
        }

        public List<EnergyClassify> GetEnergyClassifyValues(string buildId, string date)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EndDate",date)
            };
            return _db.Database.SqlQuery<EnergyClassify>(HomeResources.EnergyClassifySQL,sqlParameters).ToList();
        }
    }
}
