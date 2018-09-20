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

       

       

        public int  AddUser(string UserName, string Password, string UserGroupID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@Password",Password),
                new SqlParameter("@UserGroupID",UserGroupID)
            };
            return _db.Database.ExecuteSqlCommand(UserSetResources.AddUser, sqlParameters);
        }
    }
}
