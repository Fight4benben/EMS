using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class UserMenuResources
    {
        /// <summary>
        ///获取所有用户
        /// </summary>
        public static string GetAllUsers = @"
                                            SELECT F_UserID AS UserID,F_UserName AS UserName
                                                FROM T_SYS_Users
                                            ";

        /// <summary>
        ///获取管理关联菜单
        /// </summary>
        public static string GetAdminMenu = @"
                                             SELECT Users.F_UserID AS Id
                                                      ,F_MenuItems AS Menus
                                                  FROM T_SYS_User_Menus UserMenus
                                                  INNER JOIN T_SYS_Users Users ON Users.F_UserID=UserMenus.F_UserID
                                                  Where F_UserName ='admin'
                                            ";
        /// <summary>
        ///获取管理关联菜单
        /// </summary>
        public static string GetAdminMenuByName = @"
                                             SELECT Users.F_UserID AS Id
                                                      ,F_MenuItems AS Menus
                                                  FROM T_SYS_User_Menus UserMenus
                                                  INNER JOIN T_SYS_Users Users ON Users.F_UserID=UserMenus.F_UserID
                                                  Where F_UserName =@UserName
                                            ";

        /// <summary>
        ///获取全部用户及每个用户关联的菜单
        /// </summary>
        public static string GetAllUserMenu = @"
                                             SELECT F_UserID AS Id,F_MenuItems AS Menus
                                                 FROM T_SYS_User_Menus
                                            ";

        /// <summary>
        ///获取全部用户及每个用户关联的菜单
        /// </summary>
        public static string GetOneUserMenu = @"
                                             SELECT F_UserID AS Id,F_MenuItems AS Menus
                                                 FROM T_SYS_User_Menus
                                                 WHERE F_UserID=@UserID
                                            ";

        /// <summary>
        ///获取所菜单
        /// </summary>
        public static string GetMenuInfo = @"
                                             SELECT F_MenuID AS MenuID,F_MenuName AS MenuName
                                                 FROM T_SYS_MenusInfo
                                            ";
        /// <summary>
        ///设置用户关联菜单
        /// </summary>
        public static string SetUserMenu = @"
                                               IF EXISTS (SELECT 1 FROM T_SYS_User_Menus WHERE F_UserID = @UserID) 
	                                                        UPDATE T_SYS_User_Menus set F_MenuItems=@MenuItems
		                                                        WHERE F_UserID = @UserID
	                                                ELSE
	                                                    INSERT INTO T_SYS_User_Menus (F_UserID,F_MenuItems)
                                                                               VALUES (@UserID,@MenuItems)
                                            ";
    }
}
