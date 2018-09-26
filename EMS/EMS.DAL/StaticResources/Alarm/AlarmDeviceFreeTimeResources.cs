using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class AlarmDeviceFreeTimeResources
    {
        /// <summary>
        /// 获取设告警等级值
        /// </summary>
        public static string GetAlarmDeviceOverLimitFreeTimeSQL = @" SELECT T1.ID,T1.Name,TimePeriod,T1.F_StartHour AS 'Time',T1.Value,T2.Value AS LimitValue,T1.Value-T2.Value AS DiffValue
		                                                            ,CASE WHEN T2.Value >0 THEN (T1.Value-T2.Value)/T2.Value*100 ELSE NULL END Rate 
		                                                            FROM
			                                                            (SELECT AlarmFreeTime.F_CircuitID AS ID
					                                                            ,MeterUseInfo.F_MeterName AS Name
					                                                            ,HourResult.F_StartHour 
					                                                            ,SUM(HourResult.F_Value) AS Value
					                                                            ,AlarmFreeTime.F_StartTime +'~'+AlarmFreeTime.F_EndTime TimePeriod
				                                                            FROM T_MC_MeterHourResult AS HourResult
				                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
				                                                            INNER JOIN T_ST_MeterUseInfo AS MeterUseInfo ON MeterUseInfo.F_MeterID=HourResult.F_MeterID
				                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
				                                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
				                                                            INNER JOIN T_ST_DeviceAlarmFreeTime AS AlarmFreeTime ON Circuit.F_CircuitID=AlarmFreeTime.F_CircuitID                                                   		                                                          
				                                                            WHERE AlarmFreeTime.F_BuildID=@BuildID
					                                                            AND ParamInfo.F_IsEnergyValue = 1
						                                                            AND F_StartHour BETWEEN (CASE WHEN AlarmFreeTime.F_IsOverDay =1 THEN DATEADD( DAY,-1,@StartDay+' '+ AlarmFreeTime.F_StartTime)
                                                                                        ELSE @StartDay+' '+AlarmFreeTime.F_StartTime END) AND @StartDay+' '+AlarmFreeTime.F_EndTime 
				                                                            GROUP BY AlarmFreeTime.F_CircuitID,MeterUseInfo.F_MeterName,HourResult.F_StartHour,AlarmFreeTime.F_StartTime +'~'+AlarmFreeTime.F_EndTime 
				                                                            ) T1				
                                                            INNER JOIN 

			                                                            (SELECT AlarmFreeTime.F_CircuitID AS ID
					                                                            ,SUM(HourResult.F_Value)*F_LimitValue AS Value
				                                                            FROM T_MC_MeterHourResult AS HourResult
				                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
				                                                            INNER JOIN T_ST_MeterUseInfo AS MeterUseInfo ON MeterUseInfo.F_MeterID=HourResult.F_MeterID
				                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
				                                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
				                                                            INNER JOIN T_ST_DeviceAlarmFreeTime AS AlarmFreeTime ON Circuit.F_CircuitID=AlarmFreeTime.F_CircuitID                                                   		                                                          
				                                                            WHERE AlarmFreeTime.F_BuildID=@BuildID
					                                                            AND ParamInfo.F_IsEnergyValue = 1
						                                                            AND F_StartHour = DATEADD( DAY,-1,DATEADD( HOUR,-1,@StartDay+' '+ AlarmFreeTime.F_StartTime))
				                                                            GROUP BY AlarmFreeTime.F_CircuitID,F_LimitValue
				                                                            ) T2
				
			                                                             ON T2.ID=T1.ID
		                                                            WHERE T1.Value > T2.Value
		                                                            ORDER BY ID
                                                         ";

        /// <summary>
        /// 获取 已设置用能越限告警的设备
        /// </summary>
        public static string GetDeviceLimitValueListSQL = @" 
                                                           SELECT AlarmFreeTime.F_CircuitID AS ID
                                                                    ,Circuit.F_CircuitName AS Name
                                                                    ,F_StartTime AS StartTime 
                                                                    ,F_EndTime AS EndTime
                                                                    ,F_IsOverDay AS IsOverDay
                                                                    ,AlarmFreeTime.F_EnergyItemCode AS EnergyCode
                                                                    ,F_LimitValue AS LimitValue
                                                                FROM T_ST_DeviceAlarmFreeTime AS AlarmFreeTime
                                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON  AlarmFreeTime.F_CircuitID = Circuit.F_CircuitID
                                                                WHERE AlarmFreeTime.F_BuildID =@BuildID
                                                    ";

        /// <summary>
        /// 设置 设备用能越限告警
        /// </summary>
        public static string SetDeviceOverLimitValueSQL = @" 
                                                            IF EXISTS (SELECT 1 FROM T_ST_DeviceAlarmFreeTime WHERE F_CircuitID= @CircuitID AND F_BuildID=@BuildID) 
		                                                            UPDATE T_ST_DeviceAlarmFreeTime 
			                                                            SET F_StartTime = @StartTime , F_EndTime = @EndTime,F_IsOverDay=@isOverDay, F_LimitValue=@LimitValue 
			                                                            WHERE F_CircuitID= @CircuitID AND F_BuildID=@BuildID
	                                                            ELSE
		                                                            INSERT INTO T_ST_DeviceAlarmFreeTime
		                                                            (F_CircuitID, F_BuildID, F_Year,F_StartTime,F_EndTime,F_IsOverDay,F_LimitValue) VALUES
		                                                            ( @CircuitID,@BuildID,2018,@StartTime,@EndTime,@isOverDay,@LimitValue)
                                                    ";

        /// <summary>
        /// 删除 设备用能越限告警
        /// </summary>
        public static string DeleteDeviceOverLimitValueSQL = @" 
                                                              DELETE FROM T_ST_DeviceAlarmFreeTime WHERE F_CircuitID= @CircuitID AND F_BuildID=@BuildID
                                                    ";

        /// <summary>
        /// 获取未设置报警值的支路列表
        /// </summary>
        public static string UnSettingTreeViewInfoSQL = @"
                                                        SELECT F_CircuitID AS ID, null AS ParentID,F_CircuitName AS Name
                                                                FROM T_ST_CircuitMeterInfo AS Circuit	
                                                                WHERE Circuit.F_BuildID=@BuildID
		                                                        AND Circuit.F_CircuitID NOT IN ( SELECT F_CircuitID FROM T_ST_DeviceAlarmFreeTime )
                                                                ORDER BY ID ASC
                                                        ";
    }
}
