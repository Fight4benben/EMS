using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class DeviceAlarmResources
    {
        /// <summary>
        /// 获取设告警等级值
        /// </summary>
        public static string DeviceAlarmLevelValueSQL = @" SELECT F_BuildID AS BuildID
                                                                    ,F_EnergyItemCode AS EnergyCode
                                                                    ,F_Level1 AS Level1
                                                                    ,F_Level2 AS Level2
                                                                FROM T_ST_BuildAlarmLevel 
                                                                WHERE F_BuildID=@BuildID
                                                         ";

        /// <summary>
        /// 越限告警--获取设备天环比 
        /// </summary>
        public static string DeviceMomDayOverLevel1ValueSQL = @" SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name,T1.F_StartHour AS 'Time',t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                                    (t1.F_Value - t2.F_Value) AS DiffValue
	                                                                    ,CASE WHEN t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                                    FROM (
		                                                                    SELECT HuorResult.F_MeterID,Meter.F_MeterName,AlarmLevel.F_Level1
			                                                                    ,DATEADD(DAY,DATEDIFF(DAY,0,HuorResult.F_StartHour),0) AS F_StartHour
			                                                                    ,SUM(HuorResult.F_Value) AS F_Value FROM T_MC_MeterHourResult HuorResult
		                                                                    INNER JOIN T_ST_MeterUseInfo Meter ON HuorResult.F_MeterID = Meter.F_MeterID
		                                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON Meter.F_MeterID = Circuit.F_MeterID
		                                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                                                    INNER JOIN T_ST_BuildAlarmLevel AlarmLevel ON Meter.F_BuildID = AlarmLevel.F_BuildID
		                                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HuorResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                                    WHERE ParamInfo.F_IsEnergyValue = 1
		                                                                    AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                                                    AND Meter.F_BuildID = @BuildID
		                                                                    AND HuorResult.F_StartHour BETWEEN DATEADD(DAY,DATEDIFF(DAY,0,@StartDay),0) AND @StartDay
		                                                                    GROUP BY HuorResult.F_MeterID,Meter.F_MeterName,AlarmLevel.F_Level1,DATEADD(DAY,DATEDIFF(DAY,0,HuorResult.F_StartHour),0)
		                                                                    ) T1
                                                                    INNER JOIN 
		                                                                    (
			                                                                    SELECT HuorResult.F_MeterID,Meter.F_MeterName
				                                                                    ,DATEADD(DAY,DATEDIFF(DAY,0,HuorResult.F_StartHour),0) AS F_StartHour
				                                                                    ,SUM(HuorResult.F_Value) AS F_Value FROM T_MC_MeterHourResult HuorResult
			                                                                    INNER JOIN T_ST_MeterUseInfo Meter ON HuorResult.F_MeterID = Meter.F_MeterID
			                                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON Meter.F_MeterID = Circuit.F_MeterID
			                                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HuorResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                                    WHERE ParamInfo.F_IsEnergyValue = 1
			                                                                    AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
			                                                                    AND Meter.F_BuildID = @BuildID
			                                                                    AND HuorResult.F_StartHour BETWEEN DATEADD(DAY,DATEDIFF(DAY,0,@StartDay)-1,0) AND DATEADD(DAY, -1,  @StartDay)
			                                                                    GROUP BY HuorResult.F_MeterID,Meter.F_MeterName,DATEADD(DAY,DATEDIFF(DAY,0,HuorResult.F_StartHour),0)
		                                                                    ) T2 ON T1.F_MeterID = T2.F_MeterID
	                                                                    WHERE ABS(case when t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> T1.F_Level1
	                                                                    ORDER BY ID
                                                    ";

        /// <summary>
        /// 越限告警--获取设备月份同比
        /// </summary>
        public static string DeviceCompareMonthOverLevel1ValueSQL = @" SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name,T1.F_StartDay AS 'Time'
	                                                                            ,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                                            (t1.F_Value - t2.F_Value) AS DiffValue
	                                                                            ,CASE WHEN t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                                            FROM (
		                                                                            SELECT DayResult.F_MeterID,Meter.F_MeterName
			                                                                            ,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0) AS F_StartDay
			                                                                            ,AlarmLevel.F_Level1,SUM(DayResult.F_Value) AS F_Value 
		                                                                            FROM T_MC_MeterDayResult DayResult
		                                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
		                                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON Meter.F_MeterID = Circuit.F_MeterID
		                                                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                                                            INNER JOIN T_ST_BuildAlarmLevel AlarmLevel ON Meter.F_BuildID = AlarmLevel.F_BuildID
		                                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                                            WHERE ParamInfo.F_IsEnergyValue = 1
		                                                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                                                            AND Meter.F_BuildID = @BuildID
		                                                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,AlarmLevel.F_Level1,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
		                                                                            ) T1
                                                                            INNER JOIN 
		                                                                            (SELECT DayResult.F_MeterID,SUM(DayResult.F_Value) AS F_Value 
			                                                                            FROM T_MC_MeterDayResult DayResult
			                                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON Meter.F_MeterID = Circuit.F_MeterID
			                                                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                                            WHERE ParamInfo.F_IsEnergyValue = 1
			                                                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
			                                                                            AND Meter.F_BuildID = @BuildID
			                                                                            AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay) 
			                                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
			                                                                            ) T2 ON T1.F_MeterID = T2.F_MeterID
	                                                                            WHERE ABS(CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> T1.F_Level1
	                                                                            ORDER BY ID
                                                    ";

        /// <summary>
        /// 越限告警--获取设备月份同比
        /// </summary>
        public static string DeviceCompareQuarterOverLevel1ValueSQL = @" SELECT t1.F_MeterID AS ID,T1.F_MeterName AS Name,T1.F_StartDay AS 'Time'
	                                                                            ,t1.F_Value AS Value,t2.F_Value AS LastValue ,
	                                                                            (t1.F_Value - t2.F_Value) AS DiffValue
	                                                                            ,CASE WHEN t2.F_Value = 0 then NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value*100 END AS Rate
                                                                            FROM (
		                                                                            SELECT DayResult.F_MeterID,Meter.F_MeterName
			                                                                            ,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0) AS F_StartDay
			                                                                            ,AlarmLevel.F_Level1,SUM(DayResult.F_Value) AS F_Value 
		                                                                            FROM T_MC_MeterDayResult DayResult
		                                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
		                                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON Meter.F_MeterID = Circuit.F_MeterID
		                                                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                                                            INNER JOIN T_ST_BuildAlarmLevel AlarmLevel ON Meter.F_BuildID = AlarmLevel.F_BuildID
		                                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                                                            WHERE ParamInfo.F_IsEnergyValue = 1
		                                                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                                                            AND Meter.F_BuildID = @BuildID
		                                                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,AlarmLevel.F_Level1,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
		                                                                            ) T1
                                                                            INNER JOIN 
		                                                                            (SELECT DayResult.F_MeterID,SUM(DayResult.F_Value) AS F_Value 
			                                                                            FROM T_MC_MeterDayResult DayResult
			                                                                            INNER JOIN T_ST_MeterUseInfo Meter ON DayResult.F_MeterID = Meter.F_MeterID
			                                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON Meter.F_MeterID = Circuit.F_MeterID
			                                                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                                            WHERE ParamInfo.F_IsEnergyValue = 1
			                                                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
			                                                                            AND Meter.F_BuildID = @BuildID
			                                                                            AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, -1,  @StartDay) AND DATEADD(YEAR, -1,  @EndDay) 
			                                                                            GROUP BY DayResult.F_MeterID,Meter.F_MeterName,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)
			                                                                            ) T2 ON T1.F_MeterID = T2.F_MeterID
	                                                                            WHERE ABS(CASE WHEN t2.F_Value = 0 THEN NULL ELSE (t1.F_Value - t2.F_Value)/t2.F_Value END)> T1.F_Level1
	                                                                            ORDER BY ID
                                                    ";
    }
}
