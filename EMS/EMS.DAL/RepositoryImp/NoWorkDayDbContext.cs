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
    public class NoWorkDayDbContext : INoWorkDayDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<NoWorkDay> GetCircuitData(string buildID, string code, string beginDate, string endDate)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@Code",code),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };
            return _db.Database.SqlQuery<NoWorkDay>(NoWorkDayResources.SELECT_NoWorkDayByBuildID, sqlParameters).ToList();
        }

        public List<NoWorkDay> GetCircuitData(string buildID, string code, string[] ids, string beginDate, string endDate)
        {
            string sql = string.Format(NoWorkDayResources.SELECT_NoWorkDayByBuildIDCircuitID, "'" + string.Join("','", ids) + "'"); ;

            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildID),
                new SqlParameter("@Code",code),
                new SqlParameter("@BeginDate",beginDate),
                new SqlParameter("@EndDate",endDate)
            };
            return _db.Database.SqlQuery<NoWorkDay>(sql, sqlParameters).ToList();
        }

    }
}
