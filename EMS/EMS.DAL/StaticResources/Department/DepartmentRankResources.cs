using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class DepartmentRankResources
    {
        public static string DepartmentRankSQL = @"SELECT Department.F_DepartmentID ID,Department.F_DepartmentName Name,
                                                SUM( (CASE WHEN DMeter.F_Operator='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DMeter.F_Rate/100) Value
                                                 FROM T_ST_DepartmentInfo Department 
                                                INNER JOIN T_ST_DepartmentMeter DMeter ON Department.F_DepartmentID = DMeter.F_DepartmentID
                                                INNER JOIN T_ST_MeterUseInfo Meter ON DMeter.F_MeterID = Meter.F_MeterID
                                                INNER JOIN T_MC_MeterDayResult DayResult ON DayResult.F_MeterID = Meter.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Meter.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict ItemDict ON Circuit.F_EnergyItemCode = ItemDict.F_EnergyItemCode
                                                WHERE Department.F_BuildID = @BuildID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND DayResult.F_StartDay BETWEEN @BegDate AND @EndDate
                                                AND ItemDict.F_EnergyItemCode = @EnergyItemCode
                                                GROUP BY Department.F_DepartmentID,Department.F_DepartmentName";
    }
}
