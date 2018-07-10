using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class DepartmentCompareResources
    {
        /// <summary>
        /// 部门用能同比分析
        /// </summary>
        public static string CompareSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID, DepartmentInfo.F_DepartmentName AS Name 
                                                ,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0) AS 'Time'
                                                ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                FROM T_MC_MeterDayResult DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND DepartmentInfo.F_DepartmentID = @DepartmentID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime)-1, 0) 
                                                                             AND DATEADD(SS,-3,DATEADD(YY, DATEDIFF(YY,0,@EndTime)+1, 0))
                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
                                                ORDER BY ID,'Time' ASC
                                                ";
    }
}
