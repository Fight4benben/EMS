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
    public class DepartmentRankDbContext : IDepartmentRankDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EMSValue> GetRankList(string buildId, string startDate, string endDate, string energyCode)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@BegDate",startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@EnergyItemCode",energyCode)
            };

            return _db.Database.SqlQuery<EMSValue>(DepartmentRankResources.DepartmentRankSQL, parameters).ToList();
        }
    }
}
