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
        /// 新增一个用户
        /// </summary>
        public static string AddUser = @"
                                        IF NOT EXISTS (SELECT 1 FROM T_SYS_Users WHERE F_UserName = @UserName ) 
	                                            INSERT INTO T_SYS_Users (F_UserName,F_Password,F_UserGroupID)
		                                            VALUES (@UserName,@Password,@UserGroupID)
                                            ";
        /// <summary>
        /// 新增一个用户
        /// </summary>
        public static string UpdateUser = @"
                                            
                                            ";

        /// <summary>
        /// 新增一个用户
        /// </summary>
        public static string DeleteUser = @"
                                           
                                            ";
    }
}
