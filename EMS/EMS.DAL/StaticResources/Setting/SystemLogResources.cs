using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class SystemLogResources
    {
        /// <summary>
        ///获取操作日志
        /// </summary>
        public static string GetSystemLog = @"
                                               SELECT F_UserName AS UserName ,F_LogTime AS 'Time' ,F_Distription AS Distription
                                                      FROM T_SYS_Log
                                                      WHERE F_LogTime BETWEEN @StartDay AND @EndDay
                                            ";
    }
}
