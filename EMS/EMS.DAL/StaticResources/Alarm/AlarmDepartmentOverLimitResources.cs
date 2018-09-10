﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources.Alarm
{
    public class AlarmDepartmentOverLimitResources
    {
        /// <summary>
        /// 获取部门用能越限告警（超过设定数值）
        /// </summary>
        public static string DeptOverLimitValueSQL = @"SELECT ID,Name,Value,LimitValue,DiffValue,Rate 
	                                                        FROM
		                                                        (SELECT DepartmentInfo.F_DepartmentID AS ID
			                                                            ,DepartmentInfo.F_DepartmentName AS Name
			                                                            ,SUM(HourResult.F_Value) AS Value
			                                                            ,AlarmPlan.F_LimitValue AS LimitValue
			                                                            ,SUM(HourResult.F_Value)-AlarmPlan.F_LimitValue AS DiffValue
			                                                            ,CASE WHEN AlarmPlan.F_LimitValue = 0 THEN NULL ELSE (SUM(HourResult.F_Value)-AlarmPlan.F_LimitValue)/AlarmPlan.F_LimitValue*100 END Rate
		                                                            FROM T_MC_MeterHourResult AS HourResult
			                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
			                                                        INNER JOIN T_ST_MeterUseInfo AS MeterUseInfo ON MeterUseInfo.F_MeterID=HourResult.F_MeterID
                                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
			                                                        INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
			                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON HourResult.F_MeterID = DepartmentMeter.F_MeterID
			                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                                            INNER JOIN T_ST_DepartmentAlarmPlan AS AlarmPlan ON DepartmentInfo.F_DepartmentID=AlarmPlan.F_DepartmentID		                                                          
                                                                    WHERE DepartmentInfo.F_BuildID=@BuildID
                                                                        AND ParamInfo.F_IsEnergyValue = 1
				                                                        AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
			                                                            AND F_StartHour BETWEEN CONVERT(VARCHAR(10), @StartDay+ AlarmPlan.F_StartTime,120)
								                                                        AND (CASE WHEN AlarmPlan.F_IsOverDay =1 THEN DATEADD( DAY,1,CONVERT(VARCHAR(10), @StartDay+ AlarmPlan.F_StartTime,120))
									                                                        ELSE CONVERT(VARCHAR(10), @StartDay+ AlarmPlan.F_StartTime,120) END)
			                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,AlarmPlan.F_LimitValue) T1
	                                                        WHERE Value > LimitValue
	                                                        ORDER BY ID
                                                         ";
    }
}