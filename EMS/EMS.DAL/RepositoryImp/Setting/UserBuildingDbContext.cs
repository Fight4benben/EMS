using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources.Setting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class UserBuildingDbContext : IUserBuildingDbContext
    {
        EnergyDB _db = new EnergyDB();

        public int AddBuild(string userName, string buildId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName",userName),
                new SqlParameter("@BuildId",buildId)
            };

            return _db.Database.ExecuteSqlCommand(UserBuildingResources.InsertBuildSQL, parameters);
        }

        public int DeleteBuild(string userName, string buildId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName",userName),
                new SqlParameter("@BuildId",buildId)
            };

            return _db.Database.ExecuteSqlCommand(UserBuildingResources.DeleteUserBuildSQL, parameters);
        }

        public List<UserBuilding> GetUserBuildings(string userName)
        {
            return _db.Database.SqlQuery<UserBuilding>(UserBuildingResources.UserBuildingSQL, new SqlParameter("@Name", userName)).ToList() ;
        }

        public List<User> GetUsers()
        {
            return _db.Database.SqlQuery<User>(UserBuildingResources.AllUsersSQL).ToList();
        }

    }
}
