using EMS.DAL.Entities;
using EMS.DAL.StaticResources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class UserSetDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<UserSet> GetAllUserList()
        {
            return _db.Database.SqlQuery<UserSet>(UserSetResources.GetAllUsers).ToList();
        }

        public List<UserSet> GetUserByUserName(string userName)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName)
            };
            return _db.Database.SqlQuery<UserSet>(UserSetResources.GetUserByUserName, sqlParameters).ToList();
        }

        public List<UserSet> GetCheckOldPassword(int userID,string oldPassword)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserID",userID),
                new SqlParameter("@OldPassword",oldPassword)
            };
            return _db.Database.SqlQuery<UserSet>(UserSetResources.CheckOldPassword, sqlParameters).ToList();
        }

        public List<UserSet> GetCheckOldPasswordByName(string userName, string oldPassword)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@OldPassword",oldPassword)
            };
            return _db.Database.SqlQuery<UserSet>(UserSetResources.CheckOldPasswordByName, sqlParameters).ToList();
        }

        public int  AddUser(string userName, string password, string userGroupID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@UserGroupID",userGroupID)
            };
            return _db.Database.ExecuteSqlCommand(UserSetResources.AddUser, sqlParameters);
        }

        public int UpdataUser(string userID, string userName, string password, string oldPassword, string userGroupID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserID",userID),
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@OldPassword",oldPassword),
                new SqlParameter("@UserGroupID",userGroupID)
            };
            return _db.Database.ExecuteSqlCommand(UserSetResources.UpdateUser, sqlParameters);
        }

        public int UpdataUserByName(string userName, string password, string oldPassword)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@OldPassword",oldPassword)
            };
            return _db.Database.ExecuteSqlCommand(UserSetResources.UpdateUserByName, sqlParameters);
        }

        public int DeleteUser(string userName)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName)
            };
            return _db.Database.ExecuteSqlCommand(UserSetResources.DeleteUser, sqlParameters);
        }
    }
}
