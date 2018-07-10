using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class DepartmentReportResources
    {
        /// <summary>
        /// 查询小时表的当天每个小时的数据，需要先传入构造的部门ID，DepartmentIDs
        /// </summary>
        public static string DayReportSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID,DepartmentInfo.F_DepartmentName AS Name 
                                                    ,HourResult.F_StartHour AS 'Time'
                                                    ,SUM( (CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterHourResult HourResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON HourResult.F_MeterID = DepartmentMeter.F_MeterID
                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                    WHERE DepartmentInfo.F_DepartmentID IN ({0})
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND HourResult.F_StartHour BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime), 0) 
							                                                    AND   DATEADD(SS,-3,DATEADD(DAY, DATEDIFF(DAY,0,@EndTime)+1, 0))
                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName ,HourResult.F_StartHour
                                                    ORDER BY DepartmentInfo.F_DepartmentID,'Time' ASC";

        /// <summary>
        /// 月报表
        /// 查询天表的每天的数据，需要先传入构造的ID
        /// </summary>
        public static string MonthReportSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID,DepartmentInfo.F_DepartmentName AS Name 
                                                    ,DayResult.F_StartDay AS 'Time'
                                                    ,SUM( (CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                    WHERE DepartmentInfo.F_DepartmentID IN ({0})
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) 
							                                                     AND DATEADD(SS,-3,DATEADD(MONTH, DATEDIFF(MONTH,0,@EndTime)+1, 0))
                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName ,DayResult.F_StartDay
                                                    ORDER BY DepartmentInfo.F_DepartmentID, 'Time' ASC";

        /// <summary>
        /// 年报表
        /// 查询天表的每天的数据，需要先传入构造的ID
        /// </summary>
        public static string YearReportSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID,DepartmentInfo.F_DepartmentName AS Name 
                                                    ,DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0) AS 'Time'
                                                    ,SUM( (CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                    WHERE DepartmentInfo.F_DepartmentID IN ({0})
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime), 0) 
							                                                     AND DATEADD(SS,-3,DATEADD(YEAR, DATEDIFF(YEAR,0,@EndTime)+1, 0))
                                                    GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName ,DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0) 
                                                    ORDER BY DepartmentInfo.F_DepartmentID, 'Time' ASC";
    }
}
