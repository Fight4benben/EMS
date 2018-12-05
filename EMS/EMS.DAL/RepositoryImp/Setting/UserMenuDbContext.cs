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
    public class UserMenuDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<UserSet> GetAllUserList()
        {
            return _db.Database.SqlQuery<UserSet>(UserMenuResources.GetAllUsers).ToList();
        }

        public List<UserMenus> GetAllUserMenu()
        {
            return _db.Database.SqlQuery<UserMenus>(UserMenuResources.GetAllUserMenu).ToList();
        }

        public List<UserMenus> GetOneUserMenu( int userID)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserID",userID)
            };
            return _db.Database.SqlQuery<UserMenus>(UserMenuResources.GetOneUserMenu, sqlParameters).ToList();
        }
        
        public List<UserMenus> GetAdminMenu()
        {
            return _db.Database.SqlQuery<UserMenus>(UserMenuResources.GetAdminMenu).ToList();
        }

        public List<UserMenus> GetAdminMenuByUserName(string userName)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserName",userName)
            };
            return _db.Database.SqlQuery<UserMenus>(UserMenuResources.GetAdminMenuByName, sqlParameters).ToList();
        }

        public List<MenuInfo> GetMenuInfo()
        {
            return _db.Database.SqlQuery<MenuInfo>(UserMenuResources.GetMenuInfo).ToList();
        }

        /// <summary>
        /// 设置用户关联菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="MenuIDs"></param>
        /// <returns></returns>
        public int SetUserMenu(int userID,string MenuIDs)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@UserID",userID),
                new SqlParameter("@MenuItems",MenuIDs)
            };
            return _db.Database.ExecuteSqlCommand(UserMenuResources.SetUserMenu, sqlParameters);
        }

    }
}
