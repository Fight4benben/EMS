using EMS.DAL.Entities;
using EMS.DAL.StaticResources;
using EMS.DAL.StaticResources.Alarm;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class AlarmDepartmentOverLimitDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EnergyAlarm> GetDeptOverLimitValueList(string buildId, string energyCode, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",date)
            };
            return _db.Database.SqlQuery<EnergyAlarm>(AlarmDepartmentOverLimitResources.DeptOverLimitValueSQL, sqlParameters).ToList();
        }

        public int SetDeptOverLimitValue(string buildId, string energyCode, string departmentID, string startTime, string endTime, int isOverDay, decimal limitValue)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@DepartmentID",departmentID),
                new SqlParameter("@StartTime",startTime),
                new SqlParameter("@EndTime",endTime),
                new SqlParameter("@isOverDay",isOverDay),
                new SqlParameter("@LimitValue",limitValue)
            };
            return _db.Database.ExecuteSqlCommand(AlarmDepartmentOverLimitResources.SetDeptOverLimitValueSQL, sqlParameters);
        }

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }
    }
}
