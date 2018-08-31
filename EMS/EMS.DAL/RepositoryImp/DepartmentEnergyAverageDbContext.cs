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
    public class DepartmentEnergyAverageDbContext : IDepartmentEnergyAverageDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EnergyAverage> GetDeptMonthEnergyAverageList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EnergyAverage>(DepartmentEnergyAverageResources.DeptMonthAvgSQL, sqlParameters).ToList();
        }

        public List<EnergyAverage> GetDeptQuarterEnergyAverageList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EnergyAverage>(DepartmentEnergyAverageResources.DeptQuarterAvgSQL, sqlParameters).ToList();
        }

        public List<EnergyAverage> GetDeptYearEnergyAverageList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EnergyAverage>(DepartmentEnergyAverageResources.DeptYearAvgSQL, sqlParameters).ToList();
        }



        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }

        public List<TreeViewInfo> GetTreeViewInfoList(string buildId, string energyCode)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(DepartmentEnergyAverageResources.TreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
        }
    }
}
