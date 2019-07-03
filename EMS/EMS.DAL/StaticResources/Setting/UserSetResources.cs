using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class UserSetResources
    {
        /// <summary>
        ///获取所有用户
        /// </summary>
        public static string GetAllUsers = @"
                                            SELECT F_UserID AS UserID,F_UserName AS UserName,NULL AS 'Password',F_UserGroupID AS UserGroupID
                                                FROM T_SYS_Users
                                            ";
        /// <summary>
        ///获取所有用户
        /// </summary>
        public static string GetUserByUserName = @"
                                            SELECT F_UserID AS UserID,F_UserName AS UserName,NULL AS 'Password',F_UserGroupID AS UserGroupID
                                                FROM T_SYS_Users
                                                WHERE F_UserName = @UserName
                                            ";
        /// <summary>
        /// 新增一个用户
        /// </summary>
        public static string AddUser = @"
                                        IF NOT EXISTS (SELECT 1 FROM T_SYS_Users WHERE F_UserName = @UserName ) 
	                                                INSERT INTO T_SYS_Users (F_UserName,F_Password,F_UserGroupID)
		                                                VALUES (@UserName,@Password,@UserGroupID)
                                            ";
        /// <summary>
        /// 修改一个用户
        /// </summary>
        public static string UpdateUser = @"
                                            IF EXISTS (SELECT 1 FROM T_SYS_Users WHERE F_UserID = @UserID AND F_Password=@OldPassword ) 
	                                                UPDATE T_SYS_Users SET F_UserName = @UserName,F_Password=@Password,F_UserGroupID=@UserGroupID
		                                            WHERE F_UserID = @UserID
                                            ";

        public static string UpdateUserByName = @"
                                            IF EXISTS (SELECT 1 FROM T_SYS_Users WHERE F_UserName = @UserName AND F_Password=@OldPassword ) 
	                                                UPDATE T_SYS_Users SET F_Password=@Password
		                                                WHERE F_UserName = @UserName
                                            ";

        /// <summary>
        /// 删除一个用户
        /// </summary>
        public static string DeleteUser = @"
                                            DELETE FROM T_SYS_User_Buildings WHERE F_UserName = @UserName
                                            DELETE FROM T_SYS_Users WHERE F_UserName = @UserName
                                           ";

        /// <summary>
        /// 
        /// </summary>
        public static string CheckOldPassword = @"
                                            SELECT  F_UserID AS UserID
                                                FROM T_SYS_Users WHERE F_UserID = @UserID AND F_Password=@OldPassword 
                                           ";

        public static string CheckOldPasswordByName = @"
                                            SELECT  F_UserID AS UserID
                                                FROM T_SYS_Users WHERE F_UserName = @UserName AND F_Password=@OldPassword 
                                           ";
    }
}
