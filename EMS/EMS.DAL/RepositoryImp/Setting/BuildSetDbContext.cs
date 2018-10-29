using EMS.DAL.Entities;
using EMS.DAL.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class BuildSetDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<BuildInfoSet> GetBuildInfoList()
        {
            return _db.Database.SqlQuery<BuildInfoSet>(BuildSetResources.GetBuildInfo).ToList();
        }
    }
}
