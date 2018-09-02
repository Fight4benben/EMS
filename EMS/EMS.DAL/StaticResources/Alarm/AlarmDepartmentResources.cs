using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class AlarmDepartmentResources
    {
        /// <summary>
        /// 获取设告警等级值
        /// </summary>
        public static string AlarmDeptLevelValueSQL = @" SELECT F_BuildID AS BuildID
                                                                    ,F_EnergyItemCode AS EnergyCode
                                                                    ,F_Level1 AS Level1
                                                                    ,F_Level2 AS Level2
                                                                FROM T_ST_BuildAlarmLevel 
                                                                WHERE F_BuildID=@BuildID
                                                         ";

        /// <summary>
        /// 越限告警--获取设备天环比 
        /// </summary>
        public static string DeptMomDayOverLevel1ValueSQL = @" SELECT t1.ID AS ID,T1.Name AS Name,T1.F_StartDay AS 'Time'
		                                                            ,t1.F_Value AS Value,t2.F_Value AS LastValue
	                                                                ,(t1.F_Value - t2.F_Value) AS DiffValue
	                                                                ,CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                                FROM (
		                                                                SELECT DepartmentInfo.F_DepartmentID AS ID
		                                                                ,DepartmentInfo.F_DepartmentName AS Name 
			                                                            ,DayResult.F_StartDay AS F_StartDay
			                                                            ,AlarmLevel.F_Level1
		                                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
		                                                                FROM T_MC_MeterDayResult DayResult
		                                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                            INNER JOIN T_ST_BuildAlarmLevel AlarmLevel ON DepartmentInfo.F_BuildID = AlarmLevel.F_BuildID
                                                                        INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                                WHERE ParamInfo.F_IsEnergyValue = 1 
                                                                        AND DepartmentInfo.F_BuildID=@BuildID
                                                                        AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                                                AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,AlarmLevel.F_Level1,DayResult.F_StartDay
		                                                                ) T1
                                                                INNER JOIN 
		                                                                (
				                                                            SELECT DepartmentInfo.F_DepartmentID AS ID
				                                                            ,DayResult.F_StartDay AS F_StartDay
				                                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
				                                                            FROM T_MC_MeterDayResult DayResult
				                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
				                                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
				                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
				                                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
				                                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
				                                                            WHERE ParamInfo.F_IsEnergyValue = 1
				                                                            AND DepartmentInfo.F_BuildID=@BuildID
				                                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
				                                                            AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
				                                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DayResult.F_StartDay
		                                                                ) T2 ON T1.ID = T2.ID 
			                                                            WHERE ABS(CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> T1.F_Level1
	                                                                ORDER BY ID
                                                         ";

        /// <summary>
        /// 越限告警--获取设备月份同比
        /// </summary>
        public static string DeptCompareMonthOverLevel1ValueSQL = @" SELECT t1.ID AS ID,T1.Name AS Name,T1.F_StartDay AS 'Time'
		                                                                    ,t1.F_Value AS Value,t2.F_Value AS LastValue
	                                                                        ,(t1.F_Value - t2.F_Value) AS DiffValue
	                                                                        ,CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                                        FROM (
		                                                                        SELECT DepartmentInfo.F_DepartmentID AS ID
		                                                                        ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                    ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0) AS F_StartDay
			                                                                    ,AlarmLevel.F_Level1
		                                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
		                                                                        FROM T_MC_MeterDayResult DayResult
		                                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                                    INNER JOIN T_ST_BuildAlarmLevel AlarmLevel ON DepartmentInfo.F_BuildID = AlarmLevel.F_BuildID
                                                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                                        WHERE ParamInfo.F_IsEnergyValue = 1 
                                                                                AND DepartmentInfo.F_BuildID=@BuildID
                                                                                AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,AlarmLevel.F_Level1,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
		                                                                        ) T1
                                                                        INNER JOIN 
		                                                                        (
				                                                                    SELECT DepartmentInfo.F_DepartmentID AS ID
				                                                                    ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0) AS F_StartDay
				                                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
				                                                                    FROM T_MC_MeterDayResult DayResult
				                                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
				                                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
				                                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
				                                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
				                                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
				                                                                    WHERE ParamInfo.F_IsEnergyValue = 1
				                                                                    AND DepartmentInfo.F_BuildID=@BuildID
				                                                                    AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
				                                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
				                                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
		                                                                        ) T2 ON T1.ID = T2.ID AND RIGHT(T1.F_StartDay,2) = RIGHT(T2.F_StartDay,2)
			                                                                    WHERE ABS(CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> T1.F_Level1
	                                                                        ORDER BY ID
                                                    ";

        /// <summary>
        /// 越限告警--获取设备季度同比
        /// </summary>
        public static string DeptCompareQuarterOverLevel1ValueSQL = @" SELECT t1.ID AS ID,T1.Name AS Name,T1.F_StartDay AS 'Time'
		                                                                        ,t1.F_Value AS Value,t2.F_Value AS LastValue
	                                                                            ,(t1.F_Value - t2.F_Value) AS DiffValue
	                                                                            ,CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                                            FROM (
		                                                                            SELECT DepartmentInfo.F_DepartmentID AS ID
		                                                                            ,DepartmentInfo.F_DepartmentName AS Name 
			                                                                        ,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0) AS F_StartDay
			                                                                        ,AlarmLevel.F_Level1
		                                                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
		                                                                            FROM T_MC_MeterDayResult DayResult
		                                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                                        INNER JOIN T_ST_BuildAlarmLevel AlarmLevel ON DepartmentInfo.F_BuildID = AlarmLevel.F_BuildID
                                                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                                            WHERE ParamInfo.F_IsEnergyValue = 1 
                                                                                    AND DepartmentInfo.F_BuildID=@BuildID
                                                                                    AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,AlarmLevel.F_Level1,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
		                                                                            ) T1
                                                                            INNER JOIN 
		                                                                            (
				                                                                        SELECT DepartmentInfo.F_DepartmentID AS ID
				                                                                        ,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0) AS F_StartDay
				                                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
				                                                                        FROM T_MC_MeterDayResult DayResult
				                                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
				                                                                        INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
				                                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
				                                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
				                                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
				                                                                        WHERE ParamInfo.F_IsEnergyValue = 1
				                                                                        AND DepartmentInfo.F_BuildID=@BuildID
				                                                                        AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
				                                                                        AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
				                                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
		                                                                            ) T2 ON T1.ID = T2.ID AND RIGHT(T1.F_StartDay,2) = RIGHT(T2.F_StartDay,2)
			                                                                        WHERE ABS(CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> T1.F_Level1
	                                                                            ORDER BY ID
                                                    ";
    }
}
