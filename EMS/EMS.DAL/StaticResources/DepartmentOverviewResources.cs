using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class DepartmentOverviewResources
    {
        /// <summary>
        /// 部门天用能同比
        /// </summary>
        public static string MomDayValueSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID,DepartmentInfo.F_DepartmentName AS Name 
                                                ,DayResult.F_StartDay AS 'Time'
                                                ,SUM( (CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                FROM T_MC_MeterDayResult DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime)-1, 0) AND DATEADD(SS,-3,DATEADD(DAY, DATEDIFF(DAY,0,@EndTime)+1, 0))
                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName ,DayResult.F_StartDay
                                                ORDER BY 'Time',DepartmentInfo.F_DepartmentID ASC
                                                ";

        /// <summary>
        /// 部门当月用能
        /// </summary>
        public static string MonthValueSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID,DepartmentInfo.F_DepartmentName AS Name 
                                                ,MAX(DayResult.F_StartDay) AS 'Time',SUM (DayResult.F_Value) AS Value
                                                FROM T_MC_MeterDayResult DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay  BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) 
						                                                  AND  DATEADD(SS,-3,DATEADD(MONTH, DATEDIFF(MONTH,0,@EndTime)+1, 0))
                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName
                                                ORDER BY DepartmentInfo.F_DepartmentID ASC
                                                ";

        /// <summary>
        /// 部门当年用能
        /// </summary>
        public static string YearValueSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID, DepartmentInfo.F_DepartmentName AS Name 
                                                ,SUM( (CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                FROM T_MC_MeterDayResult DayResult
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay  BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime), 0) 
						                                                  AND DATEADD(SS,-3,DATEADD(YEAR, DATEDIFF(YEAR,0,@EndTime)+1, 0))
                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName
                                                ORDER BY DepartmentInfo.F_DepartmentID ASC
                                                ";


        /// <summary>
        /// 部门当月计划用能数据
        /// </summary>
        public static string MonthPlanValueSQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID
                                                        ,DepartmentInfo.F_DepartmentName AS Name 
                                                        ,CONVERT(varchar(7), @EndTime, 120) AS 'Time'
                                                        ,SUM (DepartmentPlan.F_Value) AS Value
                                                        FROM T_ST_DepartmentPlan DepartmentPlan
                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentPlan.F_DepartmentID
                                                        WHERE DepartmentInfo.F_BuildID=@BuildID
                                                        AND DepartmentPlan.F_Year=YEAR(@EndTime)
                                                        AND DepartmentPlan.F_Month= MONTH(@EndTime)
                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName
                                                        ";

        /// <summary>
        /// 部门当年计划用能数据
        /// </summary>
        public static string YearPlanValueSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID
                                                ,DepartmentInfo.F_DepartmentName AS Name 
                                                ,SUM (DepartmentPlan.F_Value) AS Value
                                                FROM T_ST_DepartmentPlan DepartmentPlan
                                                INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentPlan.F_DepartmentID
                                                WHERE DepartmentInfo.F_BuildID=@BuildID
                                                AND DepartmentPlan.F_Year=YEAR(@EndTime)
                                                GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentPlan.F_Year
                                                ";

        /// <summary>
        /// 最近31天总部门用能饼图
        /// </summary>
        public static string Last31DayPieChartValueSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID
                                                        ,DepartmentInfo.F_DepartmentName AS Name
                                                        ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                        FROM T_MC_MeterDayResult DayResult
                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                        INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                        INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                        WHERE Circuit.F_BuildID=@BuildID 
                                                        AND ParamInfo.F_IsEnergyValue = 1
                                                        AND DayResult.F_StartDay BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime)-30, 0) AND  DATEADD(SS,-3,DATEADD(DAY, DATEDIFF(DAY,0,@EndTime)+1, 0))
                                                        GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName 
                                                        ORDER BY DepartmentInfo.F_DepartmentID ASC
                                                        ";

        /// <summary>
        /// 最近31天总部门用能趋势
        /// </summary>
        public static string Last31DayValueSQL = @"SELECT DepartmentInfo.F_DepartmentID AS ID
                                                    ,DepartmentInfo.F_DepartmentName AS Name
                                                    ,DayResult.F_StartDay AS 'Time'
                                                    ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
                                                    INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                                                    WHERE Circuit.F_BuildID=@BuildID 
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime)-30, 0) AND  DATEADD(SS,-3,DATEADD(DAY, DATEDIFF(DAY,0,@EndTime)+1, 0))
                                                    GROUP BY DepartmentInfo.F_DepartmentID, DepartmentInfo.F_DepartmentName, DayResult.F_StartDay
                                                    ORDER BY DepartmentInfo.F_DepartmentID ASC
                                                    ";

    }
}
