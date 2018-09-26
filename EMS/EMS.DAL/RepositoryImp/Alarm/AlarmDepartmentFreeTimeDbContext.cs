using EMS.DAL.Entities;
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
    public class AlarmDepartmentFreeTimeDbContext
    {
        private EnergyDB _db = new EnergyDB();
        /// <summary>
        /// 获取部门 非工作时间用能越限数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<AlarmFreeTime> GetDeptOverLimitValueList(string buildId, string energyCode, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",date)
            };
            return _db.Database.SqlQuery<AlarmFreeTime>(AlarmDepartmentFreeTimeResources.GetAlarmDeptOverLimitFreeTimeSQL, sqlParameters).ToList();
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
