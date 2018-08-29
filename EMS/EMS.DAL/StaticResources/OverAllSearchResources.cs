using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class OverAllSearchResources
    {
        /// <summary>
        /// 支路最近31天用能
        /// </summary>
        public static string CircuitLast31DaySQL = @" SELECT Circuit.F_CircuitID AS ID
		                                                    ,Circuit.F_CircuitName AS Name 
		                                                    ,DayResult.F_StartDay AS 'Time'
		                                                    ,DayResult.F_Value AS Value
		                                                    FROM T_MC_MeterDayResult AS DayResult
		                                                    INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                    INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                    WHERE Circuit.F_CircuitName =
                                                                ( select top 1 T_ST_CircuitMeterInfo.F_CircuitName from T_ST_CircuitMeterInfo where T_ST_CircuitMeterInfo.F_CircuitName like '%'+ @KeyWord +'%')
		                                                    AND ParamInfo.F_IsEnergyValue = 1
		                                                    AND DayResult.F_StartDay BETWEEN DATEADD(DD,-31,@EndDay) AND  @EndDay
		                                                    GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName,DayResult.F_Value,DayResult.F_StartDay
		                                                    ORDER BY Name ,DayResult.F_StartDay
                                                    ";
        /// <summary>
        /// 部门最近31天用能
        /// </summary>
        public static string DeptLast31DaySQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID
		                                                ,DepartmentInfo.F_DepartmentName AS Name
		                                                ,DayResult.F_StartDay AS 'Time'
		                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
		                                                FROM T_MC_MeterDayResult DayResult
		                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                                WHERE DepartmentInfo.F_DepartmentName =
				                                              ( SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo WHERE T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
		                                                AND ParamInfo.F_IsEnergyValue = 1
		                                                AND DayResult.F_StartDay BETWEEN DATEADD(DD,-31,@EndDay)  AND  @EndDay
		                                                GROUP BY DepartmentInfo.F_DepartmentID, DepartmentInfo.F_DepartmentName, DayResult.F_StartDay
		                                                ORDER BY DayResult.F_StartDay  ASC 
                                                    ";

        /// <summary>
        /// 区域最近31天用能
        /// </summary>
        public static string RegionLast31DaySQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID
		                                                ,DepartmentInfo.F_DepartmentName AS Name
		                                                ,DayResult.F_StartDay AS 'Time'
		                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
		                                                FROM T_MC_MeterDayResult DayResult
		                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                                WHERE DepartmentInfo.F_DepartmentName =
				                                              ( SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo WHERE T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
		                                                AND ParamInfo.F_IsEnergyValue = 1
		                                                AND DayResult.F_StartDay BETWEEN DATEADD(DD,-31,@EndDay)  AND  @EndDay
		                                                GROUP BY DepartmentInfo.F_DepartmentID, DepartmentInfo.F_DepartmentName, DayResult.F_StartDay
		                                                ORDER BY DayResult.F_StartDay  ASC 
                                                    ";

        /// <summary>
        /// 支路-月份环比
        /// </summary>
        public static string CircuitMomMonthSQL = @" SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                        (t1.F_Value - t2.F_Value) AS DiffValue
	                                                        ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                        FROM (
		                                                        SELECT DayResult.F_MeterID,Meter.F_MeterName,SUM(DayResult.F_Value) AS F_Value 
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
				                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID = Meter.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1
		                                                        AND Circuit.F_CircuitName  = ( SELECT TOP 1 T_ST_CircuitMeterInfo.F_CircuitName FROM T_ST_CircuitMeterInfo WHERE T_ST_CircuitMeterInfo.F_CircuitName LIKE '%'+ @KeyWord +'%')
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                        (SELECT DayResult.F_MeterID,SUM(DayResult.F_Value) AS F_Value 
			                                                        FROM T_MC_MeterDayResult DayResult
			                                                        INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                        WHERE ParamInfo.F_IsEnergyValue = 1
			                                                        AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay) 
			                                                        GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
			                                                        ) T2 ON T1.F_MeterID = T2.F_MeterID   
	                                                    ORDER BY ID
                                                    ";

        /// <summary>
        /// 支路-月份同比
        /// </summary>
        public static string CircuitCompareMonthSQL = @" SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                        (t1.F_Value - t2.F_Value) AS DiffValue
	                                                        ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                        FROM (
		                                                        SELECT DayResult.F_MeterID,Meter.F_MeterName,SUM(DayResult.F_Value) AS F_Value 
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
				                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID = Meter.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1
		                                                        AND Circuit.F_CircuitName  = ( SELECT TOP 1 T_ST_CircuitMeterInfo.F_CircuitName FROM T_ST_CircuitMeterInfo WHERE T_ST_CircuitMeterInfo.F_CircuitName LIKE '%'+ @KeyWord +'%')
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                        (SELECT DayResult.F_MeterID,SUM(DayResult.F_Value) AS F_Value 
			                                                        FROM T_MC_MeterDayResult DayResult
			                                                        INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                        WHERE ParamInfo.F_IsEnergyValue = 1
			                                                        AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay) 
			                                                        GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
			                                                        ) T2 ON T1.F_MeterID = T2.F_MeterID   
	                                                    ORDER BY ID
                                                    ";

        /// <summary>
        /// 部门-月份环比
        /// </summary>
        public static string DeptMomMonthSQL = @" SELECT t1.ID AS ID,T1.Name AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                        (t1.F_Value - t2.F_Value) AS DiffValue
	                                                        ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                        FROM (
		                                                        SELECT DepartmentInfo.F_DepartmentID AS ID
		                                                        ,DepartmentInfo.F_DepartmentName AS Name 
		                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1 
		                                                        AND DepartmentInfo.F_DepartmentName =
					                                                ( SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo WHERE T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                       (
			                                                    SELECT DepartmentInfo.F_DepartmentID AS ID
			                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
			                                                    FROM T_MC_MeterDayResult DayResult
			                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                    WHERE ParamInfo.F_IsEnergyValue = 1
			                                                    AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay)
			                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                       ) T2 ON T1.ID = T2.ID
	                                                      ORDER BY ID
                                                    ";

        /// <summary>
        /// 部门-月份同比
        /// </summary>
        public static string DeptCompareMonthSQL = @" SELECT t1.ID AS ID,T1.Name AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                        (t1.F_Value - t2.F_Value) AS DiffValue
	                                                        ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                        FROM (
		                                                        SELECT DepartmentInfo.F_DepartmentID AS ID
		                                                        ,DepartmentInfo.F_DepartmentName AS Name 
		                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1 
		                                                        AND DepartmentInfo.F_DepartmentName =
					                                                ( SELECT TOP 1  T_ST_DepartmentInfo.F_DepartmentName FROM T_ST_DepartmentInfo WHERE T_ST_DepartmentInfo.F_DepartmentName LIKE '%'+ @KeyWord +'%')
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                       (
			                                                    SELECT DepartmentInfo.F_DepartmentID AS ID
			                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
			                                                    FROM T_MC_MeterDayResult DayResult
			                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                    WHERE ParamInfo.F_IsEnergyValue = 1
			                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
			                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                       ) T2 ON T1.ID = T2.ID
	                                                      ORDER BY ID
                                                    ";
        /// <summary>
        /// 区域-月份环比
        /// </summary>
        public static string RegionMomMonthSQL = @" SELECT t1.ID AS ID,T1.Name AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                        (t1.F_Value - t2.F_Value) AS DiffValue
	                                                        ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                        FROM (
		                                                        SELECT Region.F_RegionID AS ID
		                                                        ,Region.F_RegionName AS Name 
		                                                        ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
	                                                            INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1 
		                                                        AND Region.F_RegionName =
					                                                ( SELECT TOP 1  T_ST_Region.F_RegionName FROM T_ST_Region WHERE T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                       (
			                                                    SELECT Region.F_RegionID AS ID
		                                                        ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
	                                                            INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1 
		                                                         AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay)
		                                                        GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                       ) T2 ON T1.ID = T2.ID
	                                                      ORDER BY ID
                                                    ";

        /// <summary>
        /// 区域-月份同比
        /// </summary>
        public static string RegionCompareMonthSQL = @" SELECT t1.ID AS ID,T1.Name AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                        (t1.F_Value - t2.F_Value) AS DiffValue
	                                                        ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                        FROM (
		                                                        SELECT Region.F_RegionID AS ID
		                                                        ,Region.F_RegionName AS Name 
		                                                        ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
	                                                            INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1 
		                                                        AND Region.F_RegionName =
					                                                ( SELECT TOP 1  T_ST_Region.F_RegionName FROM T_ST_Region WHERE T_ST_Region.F_RegionName LIKE '%'+ @KeyWord +'%')
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                       (
			                                                    SELECT Region.F_RegionID AS ID
		                                                        ,SUM((CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS F_Value
		                                                        FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
	                                                            INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1 
		                                                         AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
		                                                        GROUP BY Region.F_RegionID,Region.F_RegionName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                       ) T2 ON T1.ID = T2.ID
	                                                      ORDER BY ID
                                                    ";
    }
}
