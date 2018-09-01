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
    public class DepartmentCompareDbContext: IDepartmentCompareDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EMSValue> GetDepartmentCompareValueList(string buildId, string energyCode,string departmentID, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@DepartmentID",departmentID),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EMSValue>(DepartmentCompareResources.CompareSQL, sqlParameters).ToList();
        }
        
    }
}
