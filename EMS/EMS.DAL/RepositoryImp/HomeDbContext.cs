using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;

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
    }
}
