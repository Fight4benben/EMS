using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class EnergyItemOverviewResources
    {
        /// <summary>
        /// 日分项用能同比
        /// </summary>
        public static string EnergyItemMomDaySQL = @"SELECT CalcFormula.F_EnergyItemCode AS EnergyItemCode ,CalcFormula.F_FormulaName AS Name 
                                                        ,DayResult.F_StartDay AS 'Time',SUM (DayResult.F_Value) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON DayResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                    INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                    WHERE Circuit.F_BuildID=@BuildID 
                                                    AND CalcFormula.F_EnergyItemCode LIKE '01[^0]00' 
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime)-1, 0) AND @EndTime
                                                    GROUP BY CalcFormula.F_EnergyItemCode,CalcFormula.F_FormulaName ,DayResult.F_StartDay
                                                    ORDER BY 'Time',EnergyItemCode ASC
                                                    ";

        /// <summary>
        /// 本月分项用能排名
        /// </summary>
        public static string EnergyItemRankByMonthSQL = @"SELECT CalcFormula.F_EnergyItemCode AS EnergyItemCode,Circuit.F_CircuitName AS Name 
	                                                        ,SUM(DayResult.F_Value) AS Value
                                                        FROM T_MC_MeterDayResult DayResult
                                                        INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                        INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON DayResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                        INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                        WHERE Circuit.F_BuildID=@BuildID 
                                                        AND CalcFormula.F_EnergyItemCode LIKE '01[^0]00'
                                                        AND ParamInfo.F_IsEnergyValue = 1
                                                        AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) AND  @EndTime
                                                        GROUP BY CalcFormula.F_EnergyItemCode,Circuit.F_CircuitName
                                                        ORDER BY EnergyItemCode,Value ASC
                                                        ";

        /// <summary>
        /// 最近31分项用能饼图
        /// </summary>
        public static string EnergyItemLast31DayPieChartSQL = @"SELECT CalcFormula.F_EnergyItemCode AS EnergyItemCode,CalcFormula.F_FormulaName AS Name 
	                                                                ,SUM(DayResult.F_Value) AS Value
                                                                FROM T_MC_MeterDayResult DayResult
                                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                                INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON DayResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                                INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                                WHERE Circuit.F_BuildID=@BuildID 
                                                                AND CalcFormula.F_EnergyItemCode LIKE '01[^0]00' 
                                                                AND ParamInfo.F_IsEnergyValue = 1
                                                                AND DayResult.F_StartDay BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime)-31, 0) AND @EndTime
                                                                GROUP BY CalcFormula.F_EnergyItemCode,CalcFormula.F_FormulaName 
                                                                ORDER BY EnergyItemCode ASC
                                                                ";

        /// <summary>
        /// 最近31天用能趋势
        /// </summary>
        public static string EnergyItemLast31DayValueSQL = @"SELECT CalcFormula.F_EnergyItemCode AS EnergyItemCode,CalcFormula.F_FormulaName AS Name 
	                                                            ,DayResult.F_StartDay AS 'Time',SUM (DayResult.F_Value) AS Value
                                                            FROM T_MC_MeterDayResult DayResult
                                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                            INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON DayResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                            INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                            WHERE Circuit.F_BuildID=@BuildID 
                                                            AND CalcFormula.F_EnergyItemCode LIKE '01[^0]00' 
                                                            AND ParamInfo.F_IsEnergyValue = 1
                                                            AND DayResult.F_StartDay BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime)-31, 0) AND @EndTime
                                                            GROUP BY CalcFormula.F_EnergyItemCode,CalcFormula.F_FormulaName ,DayResult.F_StartDay
                                                            ORDER BY 'Time',EnergyItemCode ASC
                                                            ";
    }
}
