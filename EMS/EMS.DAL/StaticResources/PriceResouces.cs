using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class PriceResouces
    {
        /// <summary>
        /// 查询一个建筑中的各个用能单价
        /// </summary>
        public static string PriceSQL = @"SELECT CAST(F_ElectriPrice AS FLOAT) AS ElectriPrice
                                                ,CAST(F_WaterPrice AS FLOAT) AS WaterPrice
                                                ,CAST(F_GasPrice AS FLOAT) AS GasPrice
                                                ,CAST(F_HeatPrice AS FLOAT) AS HeatPrice
                                                ,CAST(F_OtherPrice AS FLOAT) AS OtherPrice
                                            FROM T_BD_BuildExInfo
                                            WHERE F_BuildID=@BuildID";
    }
}
