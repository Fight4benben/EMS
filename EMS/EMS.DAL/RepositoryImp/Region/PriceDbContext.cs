using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class PriceDbContext : IPriceDbContext
    {
        private readonly EnergyDB _db = new EnergyDB();

        public Price GetPrice(string buildId)
        {
            List<Price> list = _db.Database.SqlQuery<Price>(PriceResouces.PriceSQL, new SqlParameter("@BuildID", buildId)).ToList();

            if (list.Count == 0)
                return null;
            else
                return list.First();
        }

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }
    }
}
