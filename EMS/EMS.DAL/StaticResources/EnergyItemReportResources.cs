using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class EnergyItemReportResources
    {
        /// <summary>
        /// 天报表
        /// 查询小时表的当天每个小时的数据，需要先传入构造的CircuitsID
        /// </summary>
        public static string DayReportSQL = @"SELECT CalcFormula.F_FormulaID AS ID,EnergyItemDict.F_EnergyItemName AS Name 
	                                                 ,HourResult.F_StartHour AS 'Time'
                                                     ,SUM( (CASE WHEN CalcFormulaMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * CalcFormulaMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterHourResult HourResult
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON HourResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                    INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = CalcFormula.F_EnergyItemCode
                                                    WHERE CalcFormula.F_FormulaID IN ({0})
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND HourResult.F_StartHour BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime), 0) AND DATEADD(SS,-3,DATEADD(DAY, DATEDIFF(DAY,0,@EndTime)+1, 0))
                                                    GROUP BY CalcFormula.F_FormulaID, EnergyItemDict.F_EnergyItemName,HourResult.F_StartHour
                                                    ORDER BY CalcFormula.F_FormulaID,'Time' ASC";

        /// <summary>
        /// 月报表
        /// 查询天表的当天每个天的数据，需要先传入构造的CircuitsID
        /// </summary>
        public static string MonthReportSQL = @"SELECT CalcFormula.F_FormulaID AS ID,EnergyItemDict.F_EnergyItemName AS Name 
	                                                    ,DayResult.F_StartDay AS 'Time' 
                                                        ,SUM( (CASE WHEN CalcFormulaMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * CalcFormulaMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON DayResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                    INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = CalcFormula.F_EnergyItemCode
	                                                WHERE CalcFormula.F_FormulaID IN ({0})
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) 
							                                                    AND DATEADD(SS,-3,DATEADD(MONTH, DATEDIFF(MONTH,0,@EndTime)+1, 0))
                                                    GROUP BY CalcFormula.F_FormulaID,EnergyItemDict.F_EnergyItemName ,DayResult.F_StartDay
                                                    ORDER BY CalcFormula.F_FormulaID,'Time' ASC";

        /// <summary>
        /// 年报表
        /// 查询天表的每个月的数据，需要先传入构造的CircuitsID
        /// </summary>
        public static string YearReportSQL = @"SELECT CalcFormula.F_FormulaID AS ID,EnergyItemDict.F_EnergyItemName AS Name 
	                                                    ,DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0) AS 'Time' 
			                                            ,SUM( (CASE WHEN CalcFormulaMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * CalcFormulaMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON DayResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                    INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = CalcFormula.F_EnergyItemCode
                                                    WHERE CalcFormula.F_FormulaID IN ({0})
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YY, DATEDIFF(YY,0,@EndTime), 0) AND DATEADD(MS,-3,DATEADD(YY,DATEDIFF(YY,0,@EndTime)+1,0))
                                                    GROUP BY CalcFormula.F_FormulaID,EnergyItemDict.F_EnergyItemName ,DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0)
                                                    ORDER BY CalcFormula.F_FormulaID,'Time' ASC";
    }
}
