using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class AlarmDepartmentFreeTimeResources
    {
        /// <summary>
        /// 获取部门用能 非工作时间 越限报警
        /// </summary>
        public static string GetAlarmDeptOverLimitFreeTimeSQL = @" SELECT T1.ID,T1.Name,TimePeriod,T1.F_StartHour AS 'Time',T1.Value,T2.Value AS LimitValue,T1.Value-T2.Value AS DiffValue
		                                                                        ,CASE WHEN T2.Value >0 THEN (T1.Value-T2.Value)/T2.Value*100 ELSE NULL END Rate 
	                                                                        FROM
		                                                                        (SELECT DepartmentInfo.F_DepartmentID AS ID
				                                                                        ,DepartmentInfo.F_DepartmentName AS Name
				                                                                        ,AlarmFreeTime.F_StartTime +'~'+AlarmFreeTime.F_EndTime AS TimePeriod
				                                                                        ,HourResult.F_StartHour 
				                                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * DepartmentMeter.F_Rate/100) AS Value 
			                                                                        FROM T_MC_MeterHourResult AS HourResult
			                                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
			                                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                                        INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON HourResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                                        INNER JOIN T_ST_DepartmentAlarmFreeTime AS AlarmFreeTime ON DepartmentInfo.F_DepartmentID=AlarmFreeTime.F_DepartmentID		                                                          
			                                                                        WHERE DepartmentInfo.F_BuildID=@BuildID
				                                                                        AND ParamInfo.F_IsEnergyValue = 1
				                                                                        AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
				                                                                        AND F_StartHour BETWEEN (CASE WHEN AlarmFreeTime.F_IsOverDay =1 THEN DATEADD( DAY,-1,@StartDay+' '+ AlarmFreeTime.F_StartTime)
									                                                                        ELSE @StartDay+' '+AlarmFreeTime.F_StartTime END) AND DATEADD( HOUR,-1,@StartDay+' '+AlarmFreeTime.F_EndTime )
			                                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName
					                                                                        ,AlarmFreeTime.F_StartTime +'~'+AlarmFreeTime.F_EndTime,HourResult.F_StartHour 
		                                                                        ) T1
	                                                                        INNER JOIN 
		                                                                        (SELECT DepartmentInfo.F_DepartmentID AS ID
				                                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * DepartmentMeter.F_Rate/100)*F_LimitValue AS Value 
			                                                                        FROM T_MC_MeterHourResult AS HourResult
			                                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
			                                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                                        INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON HourResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                                                        INNER JOIN T_ST_DepartmentAlarmFreeTime AS AlarmFreeTime ON DepartmentInfo.F_DepartmentID=AlarmFreeTime.F_DepartmentID		                                                          
			                                                                        WHERE DepartmentInfo.F_BuildID=@BuildID
				                                                                        AND ParamInfo.F_IsEnergyValue = 1
				                                                                        AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
				                                                                        AND F_StartHour =DATEADD( DAY,-1,DATEADD( HOUR,-1,@StartDay+' '+ AlarmFreeTime.F_StartTime))
			                                                                        GROUP BY DepartmentInfo.F_DepartmentID,F_LimitValue
		                                                                        ) T2
	                                                                        ON T2.ID=T1.ID
	                                                                        WHERE T1.Value > T2.Value
	                                                                        ORDER BY ID,T1.F_StartHour ASC
                                                         ";

        /// <summary>
        /// 获取非工作时间 用能报警值临时表1(每个小时用能数据)
        /// </summary>
        public static string GetAlarmDeptT1SQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID
				                                            ,DepartmentInfo.F_DepartmentName AS Name
				                                            ,AlarmFreeTime.F_StartTime +'~'+AlarmFreeTime.F_EndTime AS TimePeriod
				                                            ,HourResult.F_StartHour AS 'Time'
				                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * DepartmentMeter.F_Rate/100) AS Value 
			                                            FROM T_MC_MeterHourResult AS HourResult
			                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
			                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON HourResult.F_MeterID = DepartmentMeter.F_MeterID
			                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                            INNER JOIN T_ST_DepartmentAlarmFreeTime AS AlarmFreeTime ON DepartmentInfo.F_DepartmentID=AlarmFreeTime.F_DepartmentID		                                                          
			                                            WHERE DepartmentInfo.F_BuildID=@BuildID
				                                            AND ParamInfo.F_IsEnergyValue = 1
				                                            AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
				                                            AND F_StartHour BETWEEN CONVERT(VARCHAR(10),DATEADD(DAY,-1,@StartDay),120)+' 00:00' AND @StartDay+' 23:00'
						                                    AND F_StartHour BETWEEN (CASE WHEN AlarmFreeTime.F_IsOverDay =1 THEN DATEADD( DAY,-1,@StartDay+' '+ AlarmFreeTime.F_StartTime)
                                                                    ELSE @StartDay+' '+AlarmFreeTime.F_StartTime END) AND DATEADD( HOUR,-1,@StartDay+' '+AlarmFreeTime.F_EndTime )	
			                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName
					                                            ,AlarmFreeTime.F_StartTime +'~'+AlarmFreeTime.F_EndTime,HourResult.F_StartHour 			
				                                                                  
                                                         ";

        /// <summary>
        /// 获取非工作时间 用能报警值临时表2(用能参考值)
        /// </summary>
        public static string GetAlarmDeptT2SQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID
				                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * DepartmentMeter.F_Rate/100)*F_LimitValue AS Value 
			                                            FROM T_MC_MeterHourResult AS HourResult
			                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
			                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON HourResult.F_MeterID = DepartmentMeter.F_MeterID
			                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
			                                            INNER JOIN T_ST_DepartmentAlarmFreeTime AS AlarmFreeTime ON DepartmentInfo.F_DepartmentID=AlarmFreeTime.F_DepartmentID		                                                          
			                                            WHERE DepartmentInfo.F_BuildID=@BuildID
				                                            AND ParamInfo.F_IsEnergyValue = 1
				                                            AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
				                                            AND F_StartHour BETWEEN CONVERT(VARCHAR(10),DATEADD(DAY,-1,@StartDay),120)+' 00:00' AND CONVERT(VARCHAR(10),DATEADD(DAY,-1,@StartDay),120)+' 23:00'
						                                    AND F_StartHour = DATEADD( DAY,-1,DATEADD( HOUR,-1,@StartDay+' '+ AlarmFreeTime.F_StartTime))
			                                            GROUP BY DepartmentInfo.F_DepartmentID,F_LimitValue
                                                         ";
    }
}
