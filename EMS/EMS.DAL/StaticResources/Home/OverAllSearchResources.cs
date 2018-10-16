using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class OverAllSearchResources
    {
        #region 用能趋势
        /// <summary>
        /// 支路-天-每个小时
        /// </summary>
        public static string CircuitDaySQL = @" 
                                            SELECT Circuit.F_CircuitID AS ID
		                                            ,Circuit.F_CircuitName AS Name 
		                                            ,HourResult.F_StartHour AS 'Time'
		                                            ,HourResult.F_Value AS Value
		                                            FROM T_MC_MeterHourResult AS HourResult
		                                            INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            WHERE Circuit.F_BuildID=@BuildID
                                                    AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                            AND ParamInfo.F_IsEnergyValue = 1
		                                            AND Circuit.F_CircuitName =
			                                            (SELECT TOP 1 T_ST_CircuitMeterInfo.F_CircuitName FROM T_ST_CircuitMeterInfo 
					                                            WHERE F_BuildID=@BuildID AND T_ST_CircuitMeterInfo.F_CircuitName LIKE '%'+ @KeyWord +'%')
		                                            AND HourResult.F_StartHour BETWEEN CONVERT(VARCHAR(10), @EndDay,120) AND  @EndDay+' 23:00'
		                                            GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName,HourResult.F_Value,HourResult.F_StartHour
		                                            ORDER BY ID,HourResult.F_StartHour 
                                                    ";

        /// <summary>
        /// 支路-月份-每天用能
        /// </summary>
        public static string CircuitMonthSQL = @" 
                                                SELECT Circuit.F_CircuitID AS ID
		                                            ,Circuit.F_CircuitName AS Name 
		                                            ,DayResult.F_StartDay AS 'Time'
		                                            ,DayResult.F_Value AS Value
		                                            FROM T_MC_MeterDayResult AS DayResult
		                                            INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            WHERE Circuit.F_BuildID=@BuildID
                                                    AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                            AND ParamInfo.F_IsEnergyValue = 1
		                                            AND Circuit.F_CircuitName =
			                                            ( SELECT TOP 1 T_ST_CircuitMeterInfo.F_CircuitName FROM T_ST_CircuitMeterInfo 
					                                            WHERE F_BuildID=@BuildID AND T_ST_CircuitMeterInfo.F_CircuitName LIKE '%'+ @KeyWord +'%')
		                                            AND DayResult.F_StartDay BETWEEN CONVERT(VARCHAR(7), @EndDay,120)+'-01 00:00' AND  @EndDay
		                                            GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName,DayResult.F_Value,DayResult.F_StartDay
		                                            ORDER BY ID,DayResult.F_StartDay 
                                                    ";

        /// <summary>
        /// 支路-季度-每月用能
        /// </summary>
        public static string CircuitQuarterSQL = @" 
                                                SELECT Circuit.F_CircuitID AS ID
		                                            ,Circuit.F_CircuitName AS Name 
		                                            ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0) AS 'Time'
		                                            ,SUM(DayResult.F_Value) AS Value
		                                            FROM T_MC_MeterDayResult AS DayResult
		                                            INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            WHERE Circuit.F_BuildID=@BuildID
                                                    AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                            AND ParamInfo.F_IsEnergyValue = 1
		                                            AND Circuit.F_CircuitName =
			                                            (SELECT TOP 1 T_ST_CircuitMeterInfo.F_CircuitName FROM T_ST_CircuitMeterInfo 
					                                            WHERE F_BuildID=@BuildID AND T_ST_CircuitMeterInfo.F_CircuitName LIKE '%'+ @KeyWord +'%')
		                                            AND DayResult.F_StartDay BETWEEN @StartDay AND  @EndDay
		                                            GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
		                                            ORDER BY ID
                                                    ";

        /// <summary>
        /// 部门-天-每小时用能
        /// </summary>
        public static string DeptDaySQL = @" 
                                            SELECT DepartmentInfo.F_DepartmentID AS ID
		                                            ,DepartmentInfo.F_DepartmentName AS Name
		                                            ,HourResult.F_StartHour AS 'Time'
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
		                                            FROM T_MC_MeterHourResult AS HourResult
		                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON HourResult.F_MeterID = DepartmentMeter.F_MeterID
		                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                            WHERE DepartmentInfo.F_BuildID=@BuildID 
		                                            AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
		                                            AND ParamInfo.F_IsEnergyValue = 1
		                                            AND DepartmentInfo.F_DepartmentName =
			                                            (SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo 
					                                            WHERE F_BuildID=@BuildID AND T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
		                                            AND HourResult.F_StartHour BETWEEN CONVERT(VARCHAR(10), @EndDay,120) AND  @EndDay+' 23:00'
		                                            GROUP BY DepartmentInfo.F_DepartmentID, DepartmentInfo.F_DepartmentName, HourResult.F_StartHour
                                                    ";

        /// <summary>
        /// 部门-月份-每天用能
        /// </summary>
        public static string DeptMonthSQL = @" 
                                            SELECT DepartmentInfo.F_DepartmentID AS ID
		                                        ,DepartmentInfo.F_DepartmentName AS Name
		                                        ,DayResult.F_StartDay AS 'Time'
		                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
		                                        FROM T_MC_MeterDayResult AS DayResult
		                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                        WHERE DepartmentInfo.F_BuildID=@BuildID
		                                        AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                AND ParamInfo.F_IsEnergyValue = 1
		                                        AND DepartmentInfo.F_DepartmentName =
			                                        (SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo 
					                                        WHERE F_BuildID=@BuildID AND T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
		                                        AND DayResult.F_StartDay BETWEEN CONVERT(VARCHAR(7), @EndDay,120)+'-01 00:00'  AND  @EndDay
		                                        GROUP BY DepartmentInfo.F_DepartmentID, DepartmentInfo.F_DepartmentName, DayResult.F_StartDay
		                                        ORDER BY DayResult.F_StartDay  ASC 
                                                ";

        /// <summary>
        /// 部门-季度-每月用能
        /// </summary>
        public static string DeptQuarterSQL = @" 
                                            SELECT DepartmentInfo.F_DepartmentID AS ID
		                                        ,DepartmentInfo.F_DepartmentName AS Name
		                                        ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0) AS 'Time'
		                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
		                                        FROM T_MC_MeterDayResult AS DayResult
		                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                        WHERE DepartmentInfo.F_BuildID=@BuildID
		                                        AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                AND ParamInfo.F_IsEnergyValue = 1
		                                        AND DepartmentInfo.F_DepartmentName =
			                                        (SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo 
					                                        WHERE F_BuildID=@BuildID AND T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
		                                        AND DayResult.F_StartDay BETWEEN @StartDay  AND  @EndDay
		                                        GROUP BY DepartmentInfo.F_DepartmentID, DepartmentInfo.F_DepartmentName, DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
                                                ";


        /// <summary>
        /// 区域-天-每小时
        /// </summary>
        public static string RegionDaySQL = @"
                                            SELECT Region.F_RegionID AS ID,Region.F_RegionName AS Name 
		                                            ,HourResult.F_StartHour AS 'Time'
		                                            ,SUM( (CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * RegionMeter.F_Rate/100) AS Value
		                                            FROM T_MC_MeterHourResult AS HourResult
		                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
		                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            INNER JOIN T_ST_RegionMeter RegionMeter ON HourResult.F_MeterID = RegionMeter.F_MeterID
		                                            INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
		                                            WHERE Region.F_BuildID=@BuildID
		                                            AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                    AND ParamInfo.F_IsEnergyValue = 1
		                                            AND Region.F_RegionName =
			                                            (SELECT TOP 1  T_ST_Region.F_RegionName FROM T_ST_Region 
					                                            WHERE F_BuildID=@BuildID AND T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
		                                            AND HourResult.F_StartHour BETWEEN CONVERT(VARCHAR(10), @EndDay,120) AND  @EndDay+' 23:00'
		                                            GROUP BY Region.F_RegionID,Region.F_RegionName ,HourResult.F_StartHour
		                                            ORDER BY Region.F_RegionID, 'Time' ASC
                                                ";

        /// <summary>
        /// 区域-月份-每天
        /// </summary>
        public static string RegionMonthSQL = @"
                                            SELECT Region.F_RegionID AS ID,Region.F_RegionName AS Name 
		                                        ,DayResult.F_StartDay AS 'Time'
		                                        ,SUM( (CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS Value
		                                        FROM T_MC_MeterDayResult AS DayResult
		                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                        INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                        INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
		                                        INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
		                                        WHERE Region.F_BuildID=@BuildID
		                                        AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                AND ParamInfo.F_IsEnergyValue = 1
		                                        AND Region.F_RegionName =
			                                        (SELECT TOP 1  T_ST_Region.F_RegionName FROM T_ST_Region 
					                                        WHERE F_BuildID=@BuildID AND T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
		                                        AND DayResult.F_StartDay BETWEEN CONVERT(VARCHAR(7), @EndDay,120)+'-01 00:00'  AND  @EndDay
		                                        GROUP BY Region.F_RegionID,Region.F_RegionName ,DayResult.F_StartDay
		                                        ORDER BY Region.F_RegionID, 'Time' ASC
                                                ";

        /// <summary>
        /// 区域-季度-每月
        /// </summary>
        public static string RegionQuarterSQL = @"
                                            SELECT Region.F_RegionID AS ID,Region.F_RegionName AS Name 
                                                ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0) AS 'Time'
                                                ,SUM( (CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS Value
                                                FROM T_MC_MeterDayResult AS DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
                                                INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
                                                WHERE Region.F_BuildID=@BuildID
                                                AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND Region.F_RegionName =
                                                (SELECT TOP 1  T_ST_Region.F_RegionName FROM T_ST_Region 
		                                                WHERE F_BuildID=@BuildID AND T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
                                                AND DayResult.F_StartDay BETWEEN @StartDay  AND  @EndDay
                                                GROUP BY Region.F_RegionID,Region.F_RegionName ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
                                                ";

        #endregion

        #region 用能环比
        /// <summary>
        /// 支路-月份-环比
        /// </summary>
        public static string CircuitMomMonthSQL = @" 
                                                SELECT T1.F_CircuitID AS ID,T1.F_CircuitName AS Name,T1.F_Value AS Value,T2.F_Value AS LastValue ,
	                                                    (T1.F_Value - T2.F_Value) AS DiffValue
	                                                    ,CASE WHEN T2.F_Value = 0 THEN NULL ELSE (T1.F_Value - T2.F_Value)/T2.F_Value*100 END AS Rate
                                                    FROM (SELECT Circuit.F_CircuitID,Circuit.F_CircuitName,SUM(DayResult.F_Value) AS F_Value 
		                                                    FROM T_MC_MeterDayResult DayResult
		                                                    INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID = Meter.F_MeterID
		                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                    WHERE ParamInfo.F_IsEnergyValue = 1
                                                            AND Circuit.F_BuildID=@BuildID
		                                                    AND Circuit.F_CircuitName = 
				                                                (SELECT TOP 1 T_ST_CircuitMeterInfo.F_CircuitName FROM T_ST_CircuitMeterInfo 
						                                                WHERE F_BuildID=@BuildID AND T_ST_CircuitMeterInfo.F_CircuitName LIKE '%'+ @KeyWord +'%')
		                                                    AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                    GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
		                                                    ) T1
                                                    INNER JOIN 
		                                                    (SELECT Circuit.F_CircuitID,SUM(DayResult.F_Value) AS F_Value 
			                                                    FROM T_MC_MeterDayResult DayResult
			                                                    INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
				                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID = Meter.F_MeterID
			                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                    WHERE ParamInfo.F_IsEnergyValue = 1
			                                                    AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay) 
			                                                    GROUP BY Circuit.F_CircuitID,Meter.F_MeterName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
				                                                ) T2 ON T1.F_CircuitID = T2.F_CircuitID   
	                                                ORDER BY ID
                                                    ";

        /// <summary>
        /// 支路-季度-环比
        /// </summary>
        public static string CircuitMomQuarterSQL = @" 
                                                   SELECT T1.F_CircuitID AS ID,T1.F_CircuitName AS Name,T1.F_Value AS Value,T2.F_Value AS LastValue ,
	                                                    (T1.F_Value - T2.F_Value) AS DiffValue
	                                                    ,CASE WHEN T2.F_Value = 0 THEN NULL ELSE (T1.F_Value - T2.F_Value)/T2.F_Value*100 END AS Rate
                                                    FROM (SELECT Circuit.F_CircuitID,Circuit.F_CircuitName,SUM(DayResult.F_Value) AS F_Value 
		                                                    FROM T_MC_MeterDayResult DayResult
		                                                    INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID = Meter.F_MeterID
		                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                    WHERE ParamInfo.F_IsEnergyValue = 1
                                                            AND Circuit.F_BuildID=@BuildID
		                                                    AND Circuit.F_CircuitName = 
				                                                (SELECT TOP 1 T_ST_CircuitMeterInfo.F_CircuitName FROM T_ST_CircuitMeterInfo 
						                                                WHERE F_BuildID=@BuildID AND T_ST_CircuitMeterInfo.F_CircuitName LIKE '%'+ @KeyWord +'%')
		                                                    AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                    GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
		                                                    ) T1
                                                    INNER JOIN 
		                                                    (SELECT Circuit.F_CircuitID,SUM(DayResult.F_Value) AS F_Value 
			                                                    FROM T_MC_MeterDayResult DayResult
			                                                    INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
				                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID = Meter.F_MeterID
			                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                    WHERE ParamInfo.F_IsEnergyValue = 1
			                                                    AND DayResult.F_StartDay BETWEEN DATEADD(QUARTER, -1,  @StartDay) AND DATEADD(QUARTER, -1,  @EndDay) 
			                                                    GROUP BY Circuit.F_CircuitID,Meter.F_MeterName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
				                                                ) T2 ON T1.F_CircuitID = T2.F_CircuitID    
	                                                ORDER BY ID
                                                    ";

        /// <summary>
        /// 部门-月份-环比
        /// </summary>
        public static string DeptMomMonthSQL = @" 
                                              SELECT T1.ID AS ID,T1.Name AS Name,T1.F_Value AS Value,T2.F_Value AS LastValue
		                                            ,(T1.F_Value - T2.F_Value) AS DiffValue
		                                            ,CASE WHEN T2.F_Value = 0 THEN NULL ELSE (T1.F_Value - T2.F_Value)/T2.F_Value*100 END AS Rate
	                                            FROM (SELECT DepartmentInfo.F_DepartmentID AS ID
			                                            ,DepartmentInfo.F_DepartmentName AS Name 
			                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
			                                            FROM T_MC_MeterDayResult AS DayResult
			                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                            WHERE DepartmentInfo.F_BuildID=@BuildID 
			                                            AND Circuit.F_EnergyItemCode=@EnergyItemCode
			                                            AND ParamInfo.F_IsEnergyValue = 1
			                                            AND DepartmentInfo.F_DepartmentName =
				                                            (SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo 
						                                            WHERE F_BuildID=@BuildID AND T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
			                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
			                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
			                                            ) T1
	                                            INNER JOIN 
			                                            (SELECT DepartmentInfo.F_DepartmentID AS ID
				                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
				                                            FROM T_MC_MeterDayResult DayResult
				                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
				                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
				                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
				                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
				                                            WHERE DepartmentInfo.F_BuildID=@BuildID 
				                                            AND Circuit.F_EnergyItemCode=@EnergyItemCode
				                                            AND ParamInfo.F_IsEnergyValue = 1
				                                            AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay)
				                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
				                                            ) T2 ON T1.ID = T2.ID
		                                            ORDER BY ID
                                                    ";

        /// <summary>
        /// 部门-季度-环比
        /// </summary>
        public static string DeptMomQuarterSQL = @" 
                                               SELECT T1.ID AS ID,T1.Name AS Name,T1.F_Value AS Value,T2.F_Value AS LastValue
		                                            ,(T1.F_Value - T2.F_Value) AS DiffValue
		                                            ,CASE WHEN T2.F_Value = 0 THEN NULL ELSE (T1.F_Value - T2.F_Value)/T2.F_Value*100 END AS Rate
	                                            FROM (SELECT DepartmentInfo.F_DepartmentID AS ID
			                                            ,DepartmentInfo.F_DepartmentName AS Name 
			                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
			                                            FROM T_MC_MeterDayResult AS DayResult
			                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                            WHERE DepartmentInfo.F_BuildID=@BuildID 
			                                            AND Circuit.F_EnergyItemCode=@EnergyItemCode
			                                            AND ParamInfo.F_IsEnergyValue = 1
			                                            AND DepartmentInfo.F_DepartmentName =
				                                            (SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo 
						                                            WHERE F_BuildID=@BuildID AND T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
			                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
			                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
			                                            ) T1
	                                            INNER JOIN 
			                                            (SELECT DepartmentInfo.F_DepartmentID AS ID
				                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
				                                            FROM T_MC_MeterDayResult DayResult
				                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
				                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
				                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
				                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
				                                            WHERE DepartmentInfo.F_BuildID=@BuildID 
				                                            AND Circuit.F_EnergyItemCode=@EnergyItemCode
				                                            AND ParamInfo.F_IsEnergyValue = 1
				                                            AND DayResult.F_StartDay BETWEEN DATEADD(QUARTER, -1,  @StartDay) AND DATEADD(QUARTER, -1,  @EndDay)
				                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
				                                            ) T2 ON T1.ID = T2.ID
		                                            ORDER BY ID
                                                    ";

        /// <summary>
        /// 区域-月份-环比
        /// </summary>
        public static string RegionMomMonthSQL = @" 
                                                SELECT T1.ID AS ID,T1.Name AS Name,T1.F_Value AS Value,T2.F_Value AS LastValue
		                                                ,(T1.F_Value - T2.F_Value) AS DiffValue
		                                                ,CASE WHEN T2.F_Value = 0 THEN NULL ELSE (T1.F_Value - T2.F_Value)/T2.F_Value*100 END AS Rate
	                                                FROM (SELECT Region.F_RegionID AS ID
			                                                ,Region.F_RegionName AS Name 
			                                                ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
			                                                FROM T_MC_MeterDayResult DayResult
			                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
			                                                INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
			                                                WHERE Region.F_BuildID=@BuildID 
			                                                AND ParamInfo.F_IsEnergyValue = 1 
			                                                AND Circuit.F_EnergyItemCode=@EnergyItemCode
			                                                AND Region.F_RegionName =
				                                                (SELECT TOP 1  T_ST_Region.F_RegionName FROM T_ST_Region 
						                                                WHERE F_BuildID=@BuildID AND T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
			                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
			                                                GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
			                                                ) T1
	                                                INNER JOIN 
		                                                (SELECT Region.F_RegionID AS ID
			                                                ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
			                                                FROM T_MC_MeterDayResult DayResult
			                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
			                                                INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
			                                                WHERE Region.F_BuildID=@BuildID 
			                                                AND ParamInfo.F_IsEnergyValue = 1 
			                                                AND Circuit.F_EnergyItemCode=@EnergyItemCode
			                                                AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay)
			                                                GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
			                                                ) T2 ON T1.ID = T2.ID
	                                                ORDER BY ID	
                                                    ";

        /// <summary>
        /// 区域-季度-环比
        /// </summary>
        public static string RegionMomQuarterSQL = @" 
                                                 SELECT T1.ID AS ID,T1.Name AS Name,T1.F_Value AS Value,T2.F_Value AS LastValue
		                                                ,(T1.F_Value - T2.F_Value) AS DiffValue
		                                                ,CASE WHEN T2.F_Value = 0 THEN NULL ELSE (T1.F_Value - T2.F_Value)/T2.F_Value*100 END AS Rate
	                                                FROM (SELECT Region.F_RegionID AS ID
			                                                ,Region.F_RegionName AS Name 
			                                                ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
			                                                FROM T_MC_MeterDayResult DayResult
			                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
			                                                INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
			                                                WHERE Region.F_BuildID=@BuildID 
			                                                AND ParamInfo.F_IsEnergyValue = 1 
			                                                AND Circuit.F_EnergyItemCode=@EnergyItemCode
			                                                AND Region.F_RegionName =
				                                                (SELECT TOP 1  T_ST_Region.F_RegionName FROM T_ST_Region 
						                                                WHERE F_BuildID=@BuildID AND T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
			                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
			                                                GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
			                                                ) T1
	                                                INNER JOIN 
		                                                (SELECT Region.F_RegionID AS ID
			                                                ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
			                                                FROM T_MC_MeterDayResult DayResult
			                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
			                                                INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
			                                                WHERE Region.F_BuildID=@BuildID 
			                                                AND ParamInfo.F_IsEnergyValue = 1 
			                                                AND Circuit.F_EnergyItemCode=@EnergyItemCode
			                                                AND DayResult.F_StartDay BETWEEN DATEADD(QUARTER, -1,  @StartDay) AND DATEADD(QUARTER, -1,  @EndDay)
			                                                GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
			                                                ) T2 ON T1.ID = T2.ID
	                                                ORDER BY ID
                                                    ";


        #endregion

        #region 单位面积用能-人均用能

        /// <summary>
        /// 支路-月度-单位面积用能-人均用能
        /// </summary>
        public static string CircuitMonthAvgSQL = @"
                                                SELECT BuildInfo.F_BuildID AS ID,BuildInfo.F_BuildName AS Name 
	                                                ,BuildInfo.F_TotalArea AS TotalArea ,BuildInfo.F_NumberOfPeople AS TotalPeople
	                                                ,Convert(DECIMAL(18,2),SUM(DayResult.F_Value)) AS TotalValue
	                                                ,CASE WHEN BuildInfo.F_TotalArea = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM(DayResult.F_Value)/BuildInfo.F_TotalArea) END AS AreaAvg
	                                                ,CASE WHEN BuildInfo.F_NumberOfPeople = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM(DayResult.F_Value)/BuildInfo.F_NumberOfPeople) END AS PeopleAvg
	                                                FROM T_MC_MeterDayResult AS DayResult
	                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
	                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
	                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
	                                                INNER JOIN T_BD_BuildBaseInfo BuildInfo ON Circuit.F_BuildID = BuildInfo.F_BuildID
	                                                WHERE Circuit.F_BuildID=@BuildID
	                                                AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
	                                                AND ParamInfo.F_IsEnergyValue = 1
	                                                AND Circuit.F_MainCircuit=1
	                                                AND DayResult.F_StartDay BETWEEN @StartDay AND  @EndDay
	                                                GROUP BY BuildInfo.F_BuildID,BuildInfo.F_BuildName,BuildInfo.F_TotalArea,BuildInfo.F_NumberOfPeople
		                                                ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0) 
                                                    ";

        /// <summary>
        /// 支路-年度-单位面积用能-人均用能
        /// </summary>
        public static string CircuitYearAvgSQL = @"
                                                SELECT BuildInfo.F_BuildID AS ID,BuildInfo.F_BuildName AS Name 
	                                                ,BuildInfo.F_TotalArea AS TotalArea ,BuildInfo.F_NumberOfPeople AS TotalPeople
	                                                ,Convert(DECIMAL(18,2),SUM(DayResult.F_Value)) AS TotalValue
	                                                ,CASE WHEN BuildInfo.F_TotalArea = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM(DayResult.F_Value)/BuildInfo.F_TotalArea) END AS AreaAvg
	                                                ,CASE WHEN BuildInfo.F_NumberOfPeople = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM(DayResult.F_Value)/BuildInfo.F_NumberOfPeople) END AS PeopleAvg
	                                                FROM T_MC_MeterDayResult AS DayResult
	                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
	                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
	                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
	                                                INNER JOIN T_BD_BuildBaseInfo BuildInfo ON Circuit.F_BuildID = BuildInfo.F_BuildID
	                                                WHERE Circuit.F_BuildID=@BuildID
	                                                AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
	                                                AND ParamInfo.F_IsEnergyValue = 1
	                                                AND Circuit.F_MainCircuit=1
	                                                AND DayResult.F_StartDay BETWEEN @StartDay AND  @EndDay
	                                                GROUP BY BuildInfo.F_BuildID,BuildInfo.F_BuildName,BuildInfo.F_TotalArea,BuildInfo.F_NumberOfPeople
		                                                ,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0) 
                                                    ";

        /// <summary>
        /// 部门-月度-单位面积用能-人均用能
        /// </summary>
        public static string DeptMonthAvgSQL = @"
                                                SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
	                                                ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_People AS TotalPeople
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100)) AS TotalValue
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area) AS  AreaAvg
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_People) AS  PeopleAvg
	                                                FROM T_MC_MeterDayResult DayResult
	                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
	                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
	                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
	                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
	                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
	                                                INNER JOIN T_ST_DepartmentInfo_ExInfo DepartmentExInfo ON DepartmentExInfo.F_DepartmentID = DepartmentInfo.F_DepartmentID
	                                                WHERE ParamInfo.F_IsEnergyValue = 1 
	                                                AND DepartmentInfo.F_BuildID=@BuildID
	                                                AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
	                                                AND DepartmentInfo.F_DepartmentName =
			                                                (SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo 
					                                                WHERE F_BuildID=@BuildID AND T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
	                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
	                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_People,DepartmentExInfo.F_Area
			                                                ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
                                                    ";

        /// <summary>
        /// 部门-年度-单位面积用能-人均用能
        /// </summary>
        public static string DeptYearAvgSQL = @"
                                                SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
	                                                ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_People AS TotalPeople
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100)) AS TotalValue
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area) AS  AreaAvg
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_People) AS  PeopleAvg
	                                                FROM T_MC_MeterDayResult DayResult
	                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
	                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
	                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
	                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
	                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
	                                                INNER JOIN T_ST_DepartmentInfo_ExInfo DepartmentExInfo ON DepartmentExInfo.F_DepartmentID = DepartmentInfo.F_DepartmentID
	                                                WHERE ParamInfo.F_IsEnergyValue = 1 
	                                                AND DepartmentInfo.F_BuildID=@BuildID
	                                                AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
	                                                AND DepartmentInfo.F_DepartmentName =
			                                                (SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo 
					                                                WHERE F_BuildID=@BuildID AND T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
	                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
	                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_People,DepartmentExInfo.F_Area
			                                                ,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0)
                                                    ";

        /// <summary>
        /// 区域-月度-单位面积用能-人均用能
        /// </summary>
        public static string RegionMonthAvgSQL = @"
                                                SELECT Region.F_RegionID AS ID ,Region.F_RegionName AS Name 
	                                                ,BuildInfo.F_TotalArea AS TotalArea ,BuildInfo.F_NumberOfPeople AS TotalPeople
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100)) AS TotalValue
	                                                ,CASE WHEN BuildInfo.F_TotalArea = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100)/BuildInfo.F_TotalArea) END AS AreaAvg
	                                                ,CASE WHEN BuildInfo.F_NumberOfPeople = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100)/BuildInfo.F_NumberOfPeople) END AS PeopleAvg
	                                                FROM T_MC_MeterDayResult DayResult
	                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
	                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
	                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
	                                                INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
	                                                INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
	                                                INNER JOIN T_BD_BuildBaseInfo BuildInfo ON Region.F_BuildID = BuildInfo.F_BuildID
	                                                WHERE ParamInfo.F_IsEnergyValue = 1 
	                                                AND Region.F_BuildID=@BuildID
	                                                AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
	                                                AND Region.F_RegionName =
			                                                (SELECT TOP 1 T_ST_Region.F_RegionName FROM T_ST_Region 
						                                                WHERE F_BuildID=@BuildID AND T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
	                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
	                                                GROUP BY Region.F_RegionID,Region.F_RegionName,BuildInfo.F_TotalArea,BuildInfo.F_NumberOfPeople
			                                                ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
                                                    ";

        /// <summary>
        /// 区域-年度-单位面积用能-人均用能
        /// </summary>
        public static string RegionYearAvgSQL = @"
                                                SELECT Region.F_RegionID AS ID ,Region.F_RegionName AS Name 
	                                                ,BuildInfo.F_TotalArea AS TotalArea ,BuildInfo.F_NumberOfPeople AS TotalPeople
	                                                ,Convert(DECIMAL(18,2),SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100)) AS TotalValue
	                                                ,CASE WHEN BuildInfo.F_TotalArea = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100)/BuildInfo.F_TotalArea) END AS AreaAvg
	                                                ,CASE WHEN BuildInfo.F_NumberOfPeople = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100)/BuildInfo.F_NumberOfPeople) END AS PeopleAvg
	                                                FROM T_MC_MeterDayResult DayResult
	                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
	                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
	                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
	                                                INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
	                                                INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
	                                                INNER JOIN T_BD_BuildBaseInfo BuildInfo ON Region.F_BuildID = BuildInfo.F_BuildID
	                                                WHERE ParamInfo.F_IsEnergyValue = 1 
	                                                AND Region.F_BuildID=@BuildID
	                                                AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
	                                                AND Region.F_RegionName =
			                                                (SELECT TOP 1 T_ST_Region.F_RegionName FROM T_ST_Region 
						                                                WHERE F_BuildID=@BuildID AND T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
	                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
	                                                GROUP BY Region.F_RegionID,Region.F_RegionName,BuildInfo.F_TotalArea,BuildInfo.F_NumberOfPeople
			                                                ,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0)
                                                    ";
        #endregion
    }
}
