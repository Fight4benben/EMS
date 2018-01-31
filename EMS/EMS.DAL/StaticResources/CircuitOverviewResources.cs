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
        public static string CircuitLoadSQL = @"SELECT Circuit.F_CircuitID AS CircuitID 
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,FifteenResult.F_StartTime AS 'Time'
                                                ,FifteenResult.F_Value *4 AS Value
                                                 FROM T_ST_CircuitMeterInfo Circuit
                                                INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                INNER JOIN T_MC_MeterFifteenResult FifteenResult ON Meter.F_MeterID = FifteenResult.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON FifteenResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND FifteenResult.F_StartTime BETWEEN CONVERT(VARCHAR(10),@EndTime,120)+' 00:00:00' AND @EndTime
                                                GROUP BY Circuit.F_CircuitID,FifteenResult.F_StartTime ,FifteenResult.F_Value
                                                ORDER BY  FifteenResult.F_StartTime ASC
                                                ";

        /// <summary>
        /// 今日环比数据
        /// </summary>
        public static string CircuitMomDaySQL = @"SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name
                                                ,MAX (HourResult.F_StartHour) AS 'Time'
                                                ,SUM (HourResult.F_Value) AS Value
                                                FROM T_MC_MeterHourResult AS HourResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND HourResult.F_StartHour BETWEEN CONVERT(VARCHAR(10),@EndTime,120)+' 00:00:00' AND  @EndTime 
                                                GROUP BY Circuit.F_CircuitID 
                                                UNION
                                                SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,MAX (HourResult.F_StartHour) AS 'Time'
                                                ,SUM (HourResult.F_Value) AS Value
                                                FROM T_MC_MeterHourResult AS HourResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND HourResult.F_StartHour BETWEEN CONVERT(VARCHAR(10),DATEADD(DD,-1,@EndTime),120)+' 00:00:00' AND DATEADD(DD,-1,@EndTime)
                                                GROUP BY Circuit.F_CircuitID 
                                                ";

        /// <summary>
        /// 当月环比数据
        /// </summary>
        public static string CircuitMomMonthSQL = @"SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,MAX (DayResult.F_StartDay) AS 'Time'
                                                ,SUM (DayResult.F_Value) AS Value
                                                FROM T_MC_MeterDayResult AS DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) AND  @EndTime
                                                GROUP BY Circuit.F_CircuitID                                                
                                                UNION
                                                SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,MAX (HourResult.F_StartHour) AS 'Time'
                                                ,SUM (HourResult.F_Value) AS Value
                                                FROM T_MC_MeterHourResult AS HourResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND HourResult.F_StartHour BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime)-1, 0) AND DATEADD(MM,-1,@EndTime)
                                                GROUP BY Circuit.F_CircuitID
                                                ";

        /// <summary>
        /// 最近48小时用能数据
        /// </summary>
        public static string Circuit48HoursSQL = @"SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,MAX (HourResult.F_StartHour) AS 'Time'
                                                ,HourResult.F_Value AS Value
                                                FROM T_MC_MeterHourResult AS HourResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND HourResult.F_StartHour BETWEEN DATEADD(HH,-49,@EndTime) AND  @EndTime 
                                                GROUP BY Circuit.F_CircuitID, HourResult.F_Value, HourResult.F_StartHour
                                                ORDER BY HourResult.F_StartHour ASC
                                                ";

        /// <summary>
        /// 最近31天用能数据
        /// </summary>
        public static string Circuit31DaysSQL = @"SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,DayResult.F_StartDay AS 'Time'
                                                ,DayResult.F_Value AS Value
                                                FROM T_MC_MeterDayResult AS DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(DD,-32,@EndTime) AND  @EndTime
                                                GROUP BY Circuit.F_CircuitID, DayResult.F_Value,DayResult.F_StartDay
                                                ORDER BY DayResult.F_StartDay
                                                ";

        /// <summary>
        /// 最近12个月天用能数据
        /// </summary>
        public static string Circuit12MonthSQL = @"SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0) AS 'Time'
                                                ,SUM (DayResult.F_Value) AS Value
                                                FROM T_MC_MeterDayResult DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime)-12, 0) AND  @EndTime 
                                                GROUP BY Circuit.F_CircuitID, DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
                                                ORDER BY 'Time' ASC
                                                ";

        /// <summary>
        /// 最近3年用能数据
        /// </summary>
        public static string Circuit3YearSQL = @"SELECT Circuit.F_CircuitID AS CircuitID
                                                ,MAX(Circuit.F_CircuitName) AS Name 
                                                ,DATEADD(YY,DATEDIFF(YY,0,DayResult.F_StartDay),0) AS 'Time'
                                                ,SUM (DayResult.F_Value) AS Value
                                                FROM T_MC_MeterDayResult DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime)-2, 0) AND  @EndTime 
                                                GROUP BY Circuit.F_CircuitID, DATEADD(YY,DATEDIFF(YY,0,DayResult.F_StartDay),0)
                                                ORDER BY 'Time' ASC
                                                ";
    }
}
