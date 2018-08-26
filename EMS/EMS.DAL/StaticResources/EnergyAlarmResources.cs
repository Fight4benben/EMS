using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class EnergyAlarmResources
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
			                                                          ,CASE WHEN AlarmPlan.F_LimitValue = 0 THEN NULL ELSE (SUM(HourResult.F_Value)-AlarmPlan.F_LimitValue)/AlarmPlan.F_LimitValue END Rate
		                                                          FROM T_MC_MeterHourResult AS HourResult
		                                                          INNER JOIN T_ST_DeviceAlarmPlan AS AlarmPlan ON HourResult.F_MeterID=AlarmPlan.F_MeterID
		                                                          INNER JOIN T_ST_MeterUseInfo AS MeterUseInfo ON MeterUseInfo.F_MeterID=AlarmPlan.F_MeterID
		                                                          WHERE AlarmPlan.F_BuildID=@BuildID
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

    }
}
