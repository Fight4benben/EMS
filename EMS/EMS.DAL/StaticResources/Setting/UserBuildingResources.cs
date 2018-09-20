using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources.Setting
{
    public class UserBuildingResources
    {
        public static string UserBuildingSQL = @"SELECT b1.F_BuildID BuildID,b1.F_BuildName BuildName,CASE  WHEN t.F_BuildID is NULL THEN 0 ELSE 1 END Binded FROM 
                                                (SELECT u_b.F_BuildID,b.F_BuildName FROM T_SYS_Users u
                                                INNER JOIN T_SYS_User_Buildings u_b ON u.F_UserName = u_b.F_UserName
                                                INNER JOIN T_BD_BuildBaseInfo b ON u_b.F_BuildID = b.F_BuildID
                                                WHERE u.F_UserName = @Name) t
                                                RIGHT JOIN T_BD_BuildBaseInfo b1 ON t.F_BuildID = b1.F_BuildID";

        public static string DeleteUserBuildSQL = @"DELETE FROM T_SYS_User_Buildings WHERE F_UserName = @UserName AND F_BuildID =@BuildId";

        public static string AllUsersSQL = @"SELECT F_UserID UserID,F_UserName UserName, NULL 'Password' FROM T_SYS_Users";

        public static string InsertBuildSQL = @"INSERT INTO T_SYS_User_Buildings Values(@UserName,@BuildId)";
    }
}
