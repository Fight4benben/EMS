using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class MeterAlarmSetResources
    {
        /// <summary>
        /// 根据支路ID获取对应的仪表
        /// </summary>
        public static string SELECT_MeterInfo =
          @"SELECT T_ST_MeterUseInfo.F_MeterID MeterID,T_ST_MeterParamInfo.F_MeterParamID ParamID,F_MeterParaCode ParaCode,
                F_MeterParamName ParamName,T_MA_MeterALarmInfo.F_Stat 'State',F_Level 'Level',F_Delay 'Delay',
	            F_Lowest Lowest,F_Low Low,F_High High,F_Highest Highest
                FROM T_ST_MeterUseInfo
                INNER JOIN T_ST_MeterProdInfo ON T_ST_MeterUseInfo.F_MeterProdID = T_ST_MeterProdInfo.F_MeterProdID
                INNER JOIN T_ST_CircuitMeterInfo ON T_ST_MeterUseInfo.F_MeterID = T_ST_CircuitMeterInfo.F_MeterID
                INNER JOIN T_ST_MeterParamInfo ON T_ST_MeterProdInfo.F_MeterProdID = T_ST_MeterParamInfo.F_MeterProdID
                LEFT JOIN T_MA_MeterALarmInfo ON T_ST_MeterUseInfo.F_BuildID = T_MA_MeterALarmInfo.F_BuildID
	            AND T_ST_MeterUseInfo.F_MeterID = T_MA_MeterALarmInfo.F_MeterID
	            AND T_ST_MeterParamInfo.F_MeterParamID = T_MA_MeterALarmInfo.F_MeterParamID
	            WHERE T_ST_CircuitMeterInfo.F_BuildID=@BuildID
	            AND T_ST_CircuitMeterInfo.F_CircuitID=@CircuitID ";
    }
}
