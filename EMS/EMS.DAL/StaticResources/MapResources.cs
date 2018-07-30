using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class MapResources
    {
        /// <summary>
        /// 查询一个用户名下的所有建筑信息
        /// </summary>
        public static string MapInfoSQL = @"SELECT DISTINCT(Build.F_BuildID) BuildID, F_BuildName BuildName
	                                               ,F_BuildLong BuildLong, F_BuildLat BuildLat
		                                            FROM T_BD_BuildBaseInfo Build 
                                                    INNER JOIN T_SYS_User_Buildings UserBuildings ON Build.F_BuildID = UserBuildings.F_BuildID
                                                    WHERE F_UserName = @UserName
                                                    ORDER BY Build.F_BuildID ASC";
    }
}
