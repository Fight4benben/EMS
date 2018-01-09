using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Entities;

namespace EMS.DAL.IRepository
{
    public interface IHomeDbContext
    {
        IQueryable<BuildInfo> GetBuilds();

        BuildInfo GetBuildById(string buildId);

        List<EnergyClassify> GetEnergyClassifyValues(string buildId,string date);
    }
}
