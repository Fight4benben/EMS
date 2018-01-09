using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class HomeResources
    {
         public static string EnergyClassifySQL = @"SELECT MonthTable.EnergyItemName,MonthTable.Value AS MonthValue,YearTable.Value YearValue,
                                                MonthTable.Unit ,CAST(EnergyRate AS decimal(8,4)) EnergyRate
                                                FROM (SELECT MAX(EnergyItem.F_EnergyItemName) EnergyItemName,SUM(F_Value) Value,
                                                MAX(F_EnergyItemUnit) Unit,MAX(EnergyItem.F_EnergyItemFml) EnergyRate
                                                FROM T_ST_CircuitMeterInfo Circuit 
                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildId
                                                AND Circuit.F_MainCircuit=1
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND F_StartDay BETWEEN DATEADD(DD,-DAY(@EndDate)+1,@EndDate) AND @EndDate
                                                GROUP BY EnergyItem.F_EnergyItemCode) MonthTable
                                                FULL JOIN
                                                (SELECT MAX(EnergyItem.F_EnergyItemName) EnergyItemName,SUM(F_Value) Value,MAX(F_EnergyItemUnit) Unit 
                                                FROM T_ST_CircuitMeterInfo Circuit 
                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildId
                                                AND Circuit.F_MainCircuit=1
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND F_StartDay BETWEEN DATEADD(YY,DATEDIFF(YY,0,@EndDate),0) AND @EndDate
                                                GROUP BY EnergyItem.F_EnergyItemCode)YearTable ON MonthTable.EnergyItemName = YearTable.EnergyItemName";

    }
}
