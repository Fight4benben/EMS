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
			                                                        AND F_StartHour Between @StartDay+ AlarmPlan.F_StartTime 
			                                                        AND (CASE WHEN AlarmPlan.F_IsOverDay =1 THEN DATEADD( DAY,1,@StartDay+' '+AlarmPlan.F_EndTime )ELSE @StartDay+' '+AlarmPlan.F_EndTime END)
			                                                        GROUP BY AlarmPlan.F_MeterID,MeterUseInfo.F_MeterName,AlarmPlan.F_LimitValue) T1
	                                                        WHERE Value > LimitValue
	                                                        ORDER BY ID
                                                    ";
    }
}
