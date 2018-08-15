using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class MenuDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public UserMenus GetMenus(string userName)
        {
           List<User> users =_db.Database.SqlQuery<User>(string.Format("SELECT F_UserID UserID,F_UserName UserName, F_Password Password FROM T_SYS_Users WHERE F_UserName='{0}'",userName)).ToList();
            User user;
            if (users.Count == 0)
                user = null;
            else
                user = users.First();

            if (user != null)
                return _db.Database.SqlQuery<UserMenus>(string.Format("SELECT F_UserID Id,F_MenuItems Menus FROM T_SYS_User_Menus WHERE F_UserID='{0}'", user.UserId)).First();
            else
                return new UserMenus()
                {
                    Id = 9999,
                    Menus = "all"
                };
        }
    }
}
