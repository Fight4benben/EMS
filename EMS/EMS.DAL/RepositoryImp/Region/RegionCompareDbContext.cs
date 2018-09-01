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
    public class RegionCompareDbContext : IRegionCompareDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EMSValue> GetCompareValueList(string energyCode, string regionID, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@energyItemCode",energyCode),
                new SqlParameter("@RegionID",regionID),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EMSValue>(RegionCompareResources.CompareSQL, sqlParameters).ToList();
        }

        public List<TreeViewInfo> GetTreeViewInfoList(string buildId, string energyCode)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode)
            };
            List<TreeViewInfo> treeViewInfos = _db.Database.SqlQuery<TreeViewInfo>(RegionReportResources.TreeViewInfoSQL, sqlParameters).ToList();
            return treeViewInfos;
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
