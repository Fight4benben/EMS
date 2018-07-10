﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class EnergyItemCompareResources
    {
        /// <summary>
        /// 分项用能同比分析
        /// </summary>
        public static string EnergyItemCompareSQL = @"SELECT CalcFormula.F_EnergyItemCode AS EnergyItemCode
                                                    ,CalcFormula.F_FormulaName AS Name 
                                                    ,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0) AS 'Time'
                                                    ,SUM((CASE WHEN CalcFormulaMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * CalcFormulaMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_CalcFormulaMeter CalcFormulaMeter ON DayResult.F_MeterID = CalcFormulaMeter.F_MeterID
                                                    INNER JOIN T_ST_CalcFormula CalcFormula ON CalcFormula.F_FormulaID = CalcFormulaMeter.F_FormulaID
                                                    WHERE Circuit.F_BuildID=@BuildID 
                                                    AND CalcFormula.F_EnergyItemCode =@EnergyItemCode
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime)-1, 0) AND  DATEADD(SS,-3,DATEADD(YY, DATEDIFF(YY,0,@EndTime)+1, 0))
                                                    GROUP BY CalcFormula.F_EnergyItemCode,CalcFormula.F_FormulaName ,DATEADD(MM,DATEDIFF(MM,0,DayResult.F_StartDay),0)
                                                    ORDER BY EnergyItemCode,'Time' ASC
                                                    ";
    }
}
