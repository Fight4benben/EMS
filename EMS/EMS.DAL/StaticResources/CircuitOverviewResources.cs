using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class CircuitOverviewResources
    {
        /// <summary>
        /// 支路当日负荷曲线图
        /// </summary>
        public static string CircuitLoadSQL = @"SELECT MAX(Circuit.F_CircuitID) AS CircuitID, MAX(EnergyItemDict.F_EnergyItemCode) AS EnergyItemCode,
                                                FifteenResult.F_StartTime AS 'Time', FifteenResult.F_Value *4 AS Value
                                                FROM T_MC_MeterFifteenResult AS FifteenResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON FifteenResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict AS EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = Circuit.F_EnergyItemCode
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON ParamInfo.F_MeterParamID = FifteenResult.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND FifteenResult.F_StartTime BETWEEN CONVERT(VARCHAR(10),@EndTime,120)+' 00:00:00' AND @EndTime
                                                GROUP BY FifteenResult.F_StartTime ,FifteenResult.F_Value
                                                ORDER BY  FifteenResult.F_StartTime ASC";


        /// <summary>
        /// 今日环比数据
        /// </summary>
        public static string CircuitMomDaySQL = @"SELECT MAX(Circuit.F_CircuitID) AS CircuitID
                                                ,MAX(EnergyItemDict.F_EnergyItemCode) AS EnergyItemCode 
                                                ,MAX (HourResult.F_StartHour) AS 'Time'
                                                ,SUM (HourResult.F_Value) AS Value
                                                FROM T_MC_MeterHourResult AS HourResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict AS EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = Circuit.F_EnergyItemCode
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND HourResult.F_StartHour BETWEEN CONVERT(VARCHAR(10),@EndTime,120)+' 00:00:00' AND  @EndTime 
                                                UNION
                                                SELECT MAX(Circuit.F_CircuitID) AS CircuitID
                                                ,MAX(EnergyItemDict.F_EnergyItemCode) AS EnergyItemCode 
                                                ,MAX (HourResult.F_StartHour) AS 'Time'
                                                ,SUM (HourResult.F_Value) AS Value
                                                FROM T_MC_MeterHourResult AS HourResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict AS EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = Circuit.F_EnergyItemCode
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND HourResult.F_StartHour BETWEEN CONVERT(VARCHAR(10),DATEADD(DD,-1,@EndTime),120)+' 00:00:00' AND DATEADD(DD,-1,@EndTime)";


        /// <summary>
        /// 当月环比数据
        /// </summary>
        public static string CircuitMomMonthSQL = @"SELECT MAX(Circuit.F_CircuitID) AS CircuitID
                                                ,MAX(EnergyItemDict.F_EnergyItemCode) AS EnergyItemCode 
                                                ,MAX (DayResult.F_StartDay) AS 'Time'
                                                ,SUM (DayResult.F_Value) AS Value
                                                FROM T_MC_MeterDayResult AS DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict AS EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = Circuit.F_EnergyItemCode
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) AND  @EndTime
                                                UNION
                                                SELECT MAX(Circuit.F_CircuitID) AS CircuitID
                                                ,MAX(EnergyItemDict.F_EnergyItemCode) AS EnergyItemCode 
                                                ,MAX (DayResult.F_StartDay) AS 'Time'
                                                ,SUM (DayResult.F_Value) AS Value
                                                FROM T_MC_MeterDayResult AS DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict AS EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = Circuit.F_EnergyItemCode
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime)-1, 0) AND DATEADD(MM,-1,@EndTime)
                                                
                                                ";
    }
}
