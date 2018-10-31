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
    public class BuildSetDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<BuildInfoSet> GetBuildInfoList(string buildID)
        {
            return _db.Database.SqlQuery<BuildInfoSet>(BuildSetResources.GetBuildInfo, new SqlParameter("@BuildID", buildID)).ToList();
        }

        public int SetBuildInfo(BuildInfoSet buildInfoSet)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildInfoSet.BuildID),
                new SqlParameter("@DataCenterID",buildInfoSet.DataCenterID),
                new SqlParameter("@BuildName",buildInfoSet.BuildName),
                new SqlParameter("@AliasName",buildInfoSet.AliasName),
                new SqlParameter("@BuildOwner",buildInfoSet.BuildOwner),

                new SqlParameter("@DistrictCode",buildInfoSet.DistrictCode),
                new SqlParameter("@BuildAddr",buildInfoSet.BuildAddr),
                new SqlParameter("@BuildLong",buildInfoSet.BuildLong),
                new SqlParameter("@BuildLat",buildInfoSet.BuildLat),
                new SqlParameter("@BuildYear",buildInfoSet.BuildYear),

                new SqlParameter("@UpFloor",buildInfoSet.UpFloor),
                new SqlParameter("@DownFloor",buildInfoSet.DownFloor),
                new SqlParameter("@BuildFunc",buildInfoSet.BuildFunc),
                new SqlParameter("@TotalArea",buildInfoSet.TotalArea),
                new SqlParameter("@AirArea",buildInfoSet.AirArea),

                new SqlParameter("@DesignDept",buildInfoSet.DesignDept),
                new SqlParameter("@WorkDept",buildInfoSet.WorkDept),
                new SqlParameter("@CreateTime",buildInfoSet.CreateTime),
                new SqlParameter("@CreateUser",buildInfoSet.CreateUser),
                new SqlParameter("@MonitorDate",buildInfoSet.MonitorDate),

                new SqlParameter("@AcceptDate",buildInfoSet.AcceptDate),
                new SqlParameter("@NumberOfPeople",buildInfoSet.NumberOfPeople),
                new SqlParameter("@SPArea",buildInfoSet.SPArea),
                new SqlParameter("@Image",buildInfoSet.Image),
                new SqlParameter("@TransCount",buildInfoSet.TransCount),

                new SqlParameter("@InstallCapacity",buildInfoSet.InstallCapacity),
                new SqlParameter("@OperateCapacity",buildInfoSet.OperateCapacity),
                new SqlParameter("@DesignMeters",buildInfoSet.DesignMeters),
                new SqlParameter("@Mobiles",buildInfoSet.Mobiles)

               
            };
            return _db.Database.ExecuteSqlCommand(BuildSetResources.SetBuildInfo, sqlParameters);
        }

        public int DeleteBuild(string buildId, string circuitID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
            };
            return _db.Database.ExecuteSqlCommand(BuildSetResources.DeleteBuildSQL, sqlParameters);
        }

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }
    }
}
