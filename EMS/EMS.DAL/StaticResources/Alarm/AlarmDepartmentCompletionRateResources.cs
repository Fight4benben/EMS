using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class AlarmDepartmentCompletionRateResources
    {
        /// <summary>
        /// 获取当前设定部门用能同比下降百分率
        /// </summary>
        public static string GetDeptCompletionRateSQL = @" SELECT F_BuildID AS BuildID
                                                                  ,F_EnergyItemCode AS EnergyCode
                                                                  ,F_DepartmentCompleteRate AS Rate
                                                             FROM T_ST_BuildAlarmLevel 
                                                             WHERE F_BuildID=@BuildID
                                                         ";

        /// <summary>
        /// 设置部门告警等级值
        /// </summary>
        public static string SetDeptCompletionRateSQL = @"
                                                           IF EXISTS (SELECT 1 FROM T_ST_BuildAlarmLevel WHERE F_BuildID=@BuildID AND F_EnergyItemCode=@EnergyCode  ) 
		                                                            UPDATE T_ST_BuildAlarmLevel SET F_DepartmentCompleteRate = @CompleteRate 
                                                                            WHERE F_BuildID=@BuildID AND F_EnergyItemCode=@EnergyCode			
	                                                            ELSE
		                                                            INSERT INTO T_ST_BuildAlarmLevel
			                                                            (F_BuildID, F_EnergyItemCode, F_Level1,F_Level2,F_DepartmentCompleteRate) VALUES
			                                                            (@BuildID,@EnergyCode,0.2,0.5,@CompleteRate )
                                                         ";

        /// <summary>
        /// 部门-月度用能总量同比大于 -2%（下降小于2%）
        /// </summary>
        public static string DeptCompareMonthRateSQL = @"SELECT T1.ID AS ID,T1.Name AS Name,T1.TotalValue AS Value,T2.TotalValue AS LastValue ,
                                                                (T1.TotalValue - T2.TotalValue) AS DiffValue
                                                                ,case when T2.TotalValue = 0 then NULL ELSE (T1.TotalValue - T2.TotalValue)/T2.TotalValue*100 END AS Rate
                                                                From
		                                                                (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_Rate
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
			                                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
			                                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
					                                                                ,DepartmentExInfo.F_Rate,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)) AS T1
                                                                INNER JOIN
		                                                                (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                ,DepartmentExInfo.F_Area AS TotalArea
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
			                                                                AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
			                                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
					                                                                ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)) AS T2
                                                                ON T1.ID=T2.ID
                                                                -- WHERE CASE WHEN T2.TotalValue = 0 THEN NULL ELSE (T1.TotalValue - T2.TotalValue)/T2.TotalValue END > T1.F_Rate*-1
                                                                ORDER BY ID
                                                         ";

        /// <summary>
        /// 部门-季度用能总量同比大于 -2%（下降小于2%）
        /// </summary>
        public static string DeptCompareQuarterRateSQL = @"SELECT T1.ID AS ID,T1.Name AS Name,T1.TotalValue AS Value,T2.TotalValue AS LastValue ,
                                                                (T1.TotalValue - T2.TotalValue) AS DiffValue
                                                                ,case when T2.TotalValue = 0 then NULL ELSE (T1.TotalValue - T2.TotalValue)/T2.TotalValue*100 END AS Rate
                                                                From
		                                                                (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_Rate
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
			                                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
			                                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
					                                                                ,DepartmentExInfo.F_Rate,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)) AS T1
                                                                INNER JOIN
		                                                                (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                ,DepartmentExInfo.F_Area AS TotalArea
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
			                                                                AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
			                                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
					                                                                ,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)) AS T2
                                                                ON T1.ID=T2.ID
                                                                ORDER BY ID
                                                         ";

        /// <summary>
        /// 部门-年度用能总量同比大于 -2%（下降小于2%）
        /// </summary>
        public static string DeptCompareYearRateSQL = @"SELECT T1.ID AS ID,T1.Name AS Name,T1.TotalValue AS Value,T2.TotalValue AS LastValue ,
                                                                (T1.TotalValue - T2.TotalValue) AS DiffValue
                                                                ,case when T2.TotalValue = 0 then NULL ELSE (T1.TotalValue - T2.TotalValue)/T2.TotalValue*100 END AS Rate
                                                                From
		                                                                (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_Rate
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
			                                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
			                                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
					                                                                ,DepartmentExInfo.F_Rate,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0)) AS T1
                                                                INNER JOIN
		                                                                (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                ,DepartmentExInfo.F_Area AS TotalArea
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
			                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
			                                                                AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
			                                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
					                                                                ,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0)) AS T2
                                                                ON T1.ID=T2.ID
                                                                ORDER BY ID
                                                         ";

        /// <summary>
        /// 部门-月度 单位面积能耗 同比大于 -2%（下降小于2%）
        /// </summary>
        public static string DeptAreaAvgCompareMonthRateSQL = @"SELECT T1.ID AS ID,T1.Name AS Name,T1.AreaAvg AS Value,T2.AreaAvg AS LastValue
	                                                                    ,(T1.AreaAvg - T2.AreaAvg) AS DiffValue
	                                                                    ,CASE WHEN T2.AreaAvg = 0 THEN NULL ELSE (T1.AreaAvg - T2.AreaAvg)/T2.AreaAvg*100 END AS Rate
	                                                                    From
		                                                                     (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
				                                                                    ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_Rate
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
				                                                                    AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
				                                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
						                                                                    ,DepartmentExInfo.F_Rate,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)) AS T1
                                                                    INNER JOIN
		                                                                     (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
				                                                                    ,DepartmentExInfo.F_Area AS TotalArea
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
				                                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
				                                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
						                                                                    ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)) AS T2
	                                                                    ON T1.ID=T2.ID
	                                                                   -- WHERE CASE WHEN T2.AreaAvg = 0 THEN NULL ELSE (T1.AreaAvg - T2.AreaAvg)/T2.AreaAvg END > T1.F_Rate*-1
	                                                                    ORDER BY ID
                                                                ";

        /// <summary>
        /// 部门-季度 单位面积能耗 同比大于 -2%（下降小于2%）
        /// </summary>
        public static string DeptAreaAvgCompareQuarterRateSQL = @"SELECT T1.ID AS ID,T1.Name AS Name,T1.AreaAvg AS Value,T2.AreaAvg AS LastValue
	                                                                    ,(T1.AreaAvg - T2.AreaAvg) AS DiffValue
	                                                                    ,CASE WHEN T2.AreaAvg = 0 THEN NULL ELSE (T1.AreaAvg - T2.AreaAvg)/T2.AreaAvg*100 END AS Rate
	                                                                    From
		                                                                     (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
				                                                                    ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_Rate
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
				                                                                    AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
				                                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
						                                                                    ,DepartmentExInfo.F_Rate,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)) AS T1
                                                                    INNER JOIN
		                                                                     (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
				                                                                    ,DepartmentExInfo.F_Area AS TotalArea
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
				                                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
				                                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
						                                                                    ,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)) AS T2
	                                                                    ON T1.ID=T2.ID
	                                                                    ORDER BY ID
                                                                ";

        /// <summary>
        /// 部门-季度 单位面积能耗 同比大于 -2%（下降小于2%）
        /// </summary>
        public static string DeptAreaAvgCompareYearRateSQL = @"SELECT T1.ID AS ID,T1.Name AS Name,T1.AreaAvg AS Value,T2.AreaAvg AS LastValue
	                                                                    ,(T1.AreaAvg - T2.AreaAvg) AS DiffValue
	                                                                    ,CASE WHEN T2.AreaAvg = 0 THEN NULL ELSE (T1.AreaAvg - T2.AreaAvg)/T2.AreaAvg*100 END AS Rate
	                                                                    From
		                                                                     (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
				                                                                    ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_Rate
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
				                                                                    AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
				                                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
						                                                                    ,DepartmentExInfo.F_Rate,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0)) AS T1
                                                                    INNER JOIN
		                                                                     (SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
				                                                                    ,DepartmentExInfo.F_Area AS TotalArea
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
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
				                                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
				                                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_Area
						                                                                    ,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0)) AS T2
	                                                                    ON T1.ID=T2.ID
	                                                                    ORDER BY ID
                                                                ";


    }
}
