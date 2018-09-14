using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class AlarmDeviceOverLimitResources
    {
        /// <summary>
        /// 获取设备用能越限告警
        /// </summary>
        public static string OverLimitValueSQL = @" SELECT ID,Name,Value,LimitValue,DiffValue,Rate 
	                                                        FROM
		                                                        (SELECT AlarmPlan.F_MeterID AS ID
			                                                            ,MeterUseInfo.F_MeterName AS Name
			                                                            ,SUM(HourResult.F_Value) AS Value
			                                                            ,AlarmPlan.F_LimitValue AS LimitValue
			                                                            ,SUM(HourResult.F_Value)-AlarmPlan.F_LimitValue AS DiffValue
			                                                            ,CASE WHEN AlarmPlan.F_LimitValue = 0 THEN NULL ELSE (SUM(HourResult.F_Value)-AlarmPlan.F_LimitValue)/AlarmPlan.F_LimitValue*100 END Rate
		                                                            FROM T_MC_MeterHourResult AS HourResult
		                                                            INNER JOIN T_ST_DeviceAlarmPlan AS AlarmPlan ON HourResult.F_MeterID=AlarmPlan.F_MeterID
		                                                            INNER JOIN T_ST_MeterUseInfo AS MeterUseInfo ON MeterUseInfo.F_MeterID=AlarmPlan.F_MeterID
                                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID		                                                          
                                                                    WHERE AlarmPlan.F_BuildID=@BuildID
                                                                        AND ParamInfo.F_IsEnergyValue = 1
			                                                            AND F_StartHour Between CONVERT(varchar(10), @StartDay+ AlarmPlan.F_StartTime,120)
			                                                            AND (CASE WHEN AlarmPlan.F_IsOverDay =1 THEN DATEADD( DAY,1,CONVERT(varchar(10), @StartDay+ AlarmPlan.F_StartTime,120) )ELSE CONVERT(varchar(10), @StartDay+ AlarmPlan.F_StartTime,120) END)
			                                                        GROUP BY AlarmPlan.F_MeterID,MeterUseInfo.F_MeterName,AlarmPlan.F_LimitValue) T1
	                                                        WHERE Value > LimitValue
	                                                        ORDER BY ID
                                                    ";
        /// <summary>
        /// 设置设备用能越限告警
        /// </summary>
        public static string SetOverLimitValueSQL = @" IF EXISTS (SELECT 1 FROM T_ST_DeviceAlarmPlan WHERE F_MeterID= @MeterID AND F_BuildID=@BuildID AND F_Year=@Year AND F_Month=@Month) 
                                                                    UPDATE T_ST_DeviceAlarmPlan SET F_StartTime = @StartDay , F_EndTime = @EndDay
					                                                        ,F_IsOverDay=@isOverDay, F_LimitValue=@LimitValue
                                                            ELSE
                                                                INSERT INTO T_ST_DeviceAlarmPlan
                                                                (F_MeterID, F_BuildID, F_Year, F_Month,F_StartTime,F_EndTime,F_IsOverDay,F_LimitValue) VALUES
                                                                    ( @MeterID,@BuildID,@Year,@Month,@StartDay,@EndDay,@isOverDay,@LimitValue)
                                                    ";

        /// <summary>
        /// 设备 天用能 环比
        /// </summary>
        public static string DayCompareValueSQL = @" SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name
                                                            ,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                        (t1.F_Value - t2.F_Value) AS DiffValue
	                                                        ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                        FROM (
		                                                        SELECT DayResult.F_MeterID,Meter.F_MeterName,DayResult.F_Value FROM T_MC_MeterDayResult DayResult
		                                                        INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
		                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                        WHERE ParamInfo.F_IsEnergyValue = 1
		                                                        AND Meter.F_BuildID = @BuildID
		                                                        AND DayResult.F_StartDay = @StartDay) T1
                                                        INNER JOIN 
		                                                        (SELECT DayResult.F_MeterID,DayResult.F_Value FROM T_MC_MeterDayResult DayResult
			                                                        INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                        WHERE ParamInfo.F_IsEnergyValue = 1
			                                                        AND Meter.F_BuildID = @BuildID
			                                                        AND DayResult.F_StartDay = DATEADD(DAY, -1,  @StartDay)) T2 ON T1.F_MeterID = T2.F_MeterID 
                                                        WHERE ABS(case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> 0.2 
                                                        ORDER BY ID
                                                    ";

        /// <summary>
        /// 设备环比--本月与上月
        /// </summary>
        public static string MonthMomValueSQL = @"SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                            (t1.F_Value - t2.F_Value) AS DiffValue
	                                                            ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                            FROM (
		                                                            SELECT DayResult.F_MeterID,Meter.F_MeterName,SUM(DayResult.F_Value) AS F_Value 
		                                                            FROM T_MC_MeterDayResult DayResult
		                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
		                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                            WHERE ParamInfo.F_IsEnergyValue = 1
		                                                            AND Meter.F_BuildID = @BuildID
		                                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                            ) T1
                                                            INNER JOIN 
		                                                            (SELECT DayResult.F_MeterID,SUM(DayResult.F_Value) AS F_Value 
			                                                            FROM T_MC_MeterDayResult DayResult
			                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                            WHERE ParamInfo.F_IsEnergyValue = 1
			                                                            AND Meter.F_BuildID = @BuildID
			                                                            AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay) 
			                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
			                                                            ) T2 ON T1.F_MeterID = T2.F_MeterID
	                                                        WHERE ABS(case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> 0.2 
	                                                        ORDER BY ID
                                                    ";

        /// <summary>
        /// 设备同比--本月与上年同月
        /// </summary>
        public static string MonthCompareValueSQL = @"SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                            (t1.F_Value - t2.F_Value) AS DiffValue
	                                                            ,case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                            FROM (
		                                                            SELECT DayResult.F_MeterID,Meter.F_MeterName,SUM(DayResult.F_Value) AS F_Value 
		                                                            FROM T_MC_MeterDayResult DayResult
		                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
		                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                            WHERE ParamInfo.F_IsEnergyValue = 1
		                                                            AND Meter.F_BuildID = @BuildID
		                                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                            ) T1
                                                            INNER JOIN 
		                                                            (SELECT DayResult.F_MeterID,SUM(DayResult.F_Value) AS F_Value 
			                                                            FROM T_MC_MeterDayResult DayResult
			                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                            WHERE ParamInfo.F_IsEnergyValue = 1
			                                                            AND Meter.F_BuildID = @BuildID
			                                                            AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay) 
			                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
			                                                            ) T2 ON T1.F_MeterID = T2.F_MeterID
	                                                        WHERE ABS(case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> 0.2 
	                                                        ORDER BY ID
                                                    ";

        /// <summary>
        /// 部门月份环比--本月与上月
        /// </summary>
        public static string DeptMomValueSQL = @"SELECT t1.ID AS ID,T1.Name AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
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
		                                                        WHERE Circuit.F_BuildID=@BuildID 
		                                                        AND ParamInfo.F_IsEnergyValue = 1
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                        (
			                                                        SELECT DepartmentInfo.F_DepartmentID AS ID
			                                                        ,DepartmentInfo.F_DepartmentName AS Name 
			                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
			                                                        FROM T_MC_MeterDayResult DayResult
			                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                        WHERE Circuit.F_BuildID=@BuildID 
			                                                        AND ParamInfo.F_IsEnergyValue = 1
			                                                        AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, -1,  @StartDay) AND DATEADD(MONTH, -1,  @EndDay)
			                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T2 ON T1.ID = T2.ID
	                                                        WHERE ABS(case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> 0.2 
	                                                        ORDER BY ID
                                                    ";

        /// <summary>
        /// 部门月份同比--本月与上年同月
        /// </summary>
        public static string DeptCompareValueSQL = @"SELECT t1.ID AS ID,T1.Name AS Name,t1.F_Value AS Value,t2.F_Value AS LastValue ,
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
		                                                        WHERE Circuit.F_BuildID=@BuildID 
		                                                        AND ParamInfo.F_IsEnergyValue = 1
		                                                        AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T1
                                                        INNER JOIN 
		                                                        (
			                                                        SELECT DepartmentInfo.F_DepartmentID AS ID
			                                                        ,DepartmentInfo.F_DepartmentName AS Name 
			                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS F_Value
			                                                        FROM T_MC_MeterDayResult DayResult
			                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
			                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                        WHERE Circuit.F_BuildID=@BuildID 
			                                                        AND ParamInfo.F_IsEnergyValue = 1
			                                                        AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay)
			                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                        ) T2 ON T1.ID = T2.ID
	                                                        WHERE ABS(case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> 0.2 
	                                                        ORDER BY ID
                                                    ";
    }
}
