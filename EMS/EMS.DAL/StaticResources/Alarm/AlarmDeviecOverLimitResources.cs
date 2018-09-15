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
        public static string GetDeviceOverLimitValueSQL = @" SELECT ID,Name,Value,LimitValue,DiffValue,Rate 
	                                                        FROM
		                                                        (SELECT AlarmPlan.F_CircuitID AS ID
			                                                            ,MeterUseInfo.F_MeterName AS Name
			                                                            ,SUM(HourResult.F_Value) AS Value
			                                                            ,AlarmPlan.F_LimitValue AS LimitValue
			                                                            ,SUM(HourResult.F_Value)-AlarmPlan.F_LimitValue AS DiffValue
			                                                            ,CASE WHEN AlarmPlan.F_LimitValue = 0 THEN NULL ELSE (SUM(HourResult.F_Value)-AlarmPlan.F_LimitValue)/AlarmPlan.F_LimitValue*100 END Rate
		                                                            FROM T_MC_MeterHourResult AS HourResult
                                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
			                                                        INNER JOIN T_ST_MeterUseInfo AS MeterUseInfo ON MeterUseInfo.F_MeterID=HourResult.F_MeterID
                                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                        INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                                            INNER JOIN T_ST_DeviceAlarmPlan AS AlarmPlan ON Circuit.F_CircuitID=AlarmPlan.F_CircuitID
		                                                           		                                                          
                                                                    WHERE AlarmPlan.F_BuildID=@BuildID
                                                                        AND ParamInfo.F_IsEnergyValue = 1
			                                                            AND F_StartHour Between CONVERT(varchar(10), @StartDay+ AlarmPlan.F_StartTime,120)
			                                                            AND (CASE WHEN AlarmPlan.F_IsOverDay =1 THEN DATEADD( DAY,1,CONVERT(varchar(10), @StartDay+ AlarmPlan.F_StartTime,120) )ELSE CONVERT(varchar(10), @StartDay+ AlarmPlan.F_StartTime,120) END)
			                                                        GROUP BY AlarmPlan.F_CircuitID,MeterUseInfo.F_MeterName,AlarmPlan.F_LimitValue) T1
	                                                        WHERE Value > LimitValue
	                                                        ORDER BY ID
                                                    ";
        /// <summary>
        /// 设置 设备用能越限告警
        /// </summary>
        public static string GetDeviceLimitValueListSQL = @" 
                                                            SELECT DeviceAlarmPlan.F_CircuitID AS ID
                                                                  ,Circuit.F_CircuitName AS Name
                                                                  ,F_StartTime AS StartTime 
                                                                  ,F_EndTime AS EndTime
                                                                  ,F_IsOverDay AS IsOverDay
                                                                  ,DeviceAlarmPlan.F_EnergyItemCode AS EnergyCode
                                                                  ,F_LimitValue AS LimitValue
                                                              FROM T_ST_DeviceAlarmPlan DeviceAlarmPlan
                                                              INNER JOIN T_ST_CircuitMeterInfo Circuit ON  DeviceAlarmPlan.F_CircuitID = Circuit.F_CircuitID
                                                              WHERE DeviceAlarmPlan.F_BuildID =@BuildID
                                                    ";

        /// <summary>
        /// 设置 设备用能越限告警
        /// </summary>
        public static string SetDeviceOverLimitValueSQL = @" 
                                                    IF EXISTS (SELECT 1 FROM T_ST_DeviceAlarmPlan WHERE F_CircuitID= @CircuitID AND F_BuildID=@BuildID) 
		                                                    UPDATE T_ST_DeviceAlarmPlan 
			                                                    SET F_StartTime = @StartTime , F_EndTime = @EndTime,F_IsOverDay=@isOverDay, F_LimitValue=@LimitValue 
			                                                    WHERE F_CircuitID= @CircuitID AND F_BuildID=@BuildID
	                                                    ELSE
		                                                    INSERT INTO T_ST_DeviceAlarmPlan
		                                                    (F_CircuitID, F_BuildID, F_Year, F_Month,F_StartTime,F_EndTime,F_IsOverDay,F_LimitValue) VALUES
		                                                    ( @CircuitID,@BuildID,2018,1,@StartTime,@EndTime,@isOverDay,@LimitValue)
                                                    ";

        /// <summary>
        /// 删除 设备用能越限告警
        /// </summary>
        public static string DeleteDeviceOverLimitValueSQL = @" DELETE FROM T_ST_DeviceAlarmPlan WHERE F_CircuitID= @CircuitID AND F_BuildID=@BuildID
                                                    ";

        /// <summary>
        /// 获取未设置报警值的支路列表
        /// </summary>
        public static string UnSettingTreeViewInfoSQL = @"SELECT F_CircuitID AS ID, null AS ParentID,F_CircuitName AS Name
                                                                FROM T_ST_CircuitMeterInfo AS Circuit	
                                                                WHERE Circuit.F_BuildID=@BuildID
		                                                        AND Circuit.F_CircuitID NOT IN ( SELECT F_CircuitID FROM T_ST_DeviceAlarmPlan )
                                                                ORDER BY ID ASC
                                                        ";


    }
}
