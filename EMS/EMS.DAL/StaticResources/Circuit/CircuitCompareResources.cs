﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class CircuitCompareResources
    {
        /// <summary>
        /// 支路用能同比分析
        /// </summary>
        public static string CircuitCompareSQL = @"SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0) AS 'Time'
                                                ,SUM (DayResult.F_Value) AS Value
                                                FROM T_MC_MeterDayResult DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime)-1, 0) AND  DATEADD(SS,-3,DATEADD(YY, DATEDIFF(YY,0,@EndTime)+1, 0))
                                                GROUP BY Circuit.F_CircuitID,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
                                                ORDER BY 'Time' ASC
                                                ";

        public static string CircuitDayCompareSQL = @"SELECT Circuit.F_CircuitID AS CircuitID,
                                                MAX(Circuit.F_CircuitName) AS Name 
                                                ,HourResult.F_StartHour AS 'Time'
                                                ,SUM (HourResult.F_Value) AS Value
                                                FROM T_MC_MeterHourResult HourResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND HourResult.F_StartHour BETWEEN DATEADD(DD, -1,CONVERT(VARCHAR(10),@EndTime,120)+' 00:00:00') AND  CONVERT(VARCHAR(10),@EndTime,120)+' 23:00:00'
                                                GROUP BY Circuit.F_CircuitID,HourResult.F_StartHour
                                                ORDER BY 'Time' ASC";

    }
}
