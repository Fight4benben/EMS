using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class NoWorkDayResources
    {
        public static string SELECT_NoWorkDayByBuildID =
          @"WITH TempA AS(
	            SELECT T_ST_CircuitMeterInfo.F_CircuitID ID,
	            MAX(T_ST_CircuitMeterInfo.F_CircuitName) AS Name, 
	            CASE WHEN T_DT_NonWorkDays.F_BuildID IS NOT NULL THEN 'NoWork' ELSE 'Work' END DayType,
	            SUM(T_MC_MeterDayResult.F_Value) Value  
	            FROM T_ST_CircuitMeterInfo INNER JOIN T_BD_BuildBaseInfo ON T_BD_BuildBaseInfo.F_BuildID = T_ST_CircuitMeterInfo.F_BuildID 
	            INNER JOIN T_DT_EnergyItemDict ON T_ST_CircuitMeterInfo.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
	            LEFT OUTER JOIN T_ST_MeterUseInfo ON T_ST_MeterUseInfo.F_MeterID = T_ST_CircuitMeterInfo.F_MeterID 
	            LEFT OUTER JOIN T_MC_MeterDayResult ON T_MC_MeterDayResult.F_MeterID = T_ST_MeterUseInfo.F_MeterID 
	            LEFT OUTER JOIN T_DT_NonWorkDays ON T_MC_MeterDayResult.F_StartDay = T_DT_NonWorkDays.F_Date 
	            LEFT OUTER JOIN T_ST_MeterParamInfo ON T_MC_MeterDayResult.F_MeterParamID = T_ST_MeterParamInfo.F_MeterParamID  
	            WHERE T_ST_CircuitMeterInfo.F_BuildID = @BuildID 
                AND T_ST_CircuitMeterInfo.F_EnergyItemCode=@Code
	            AND T_MC_MeterDayResult.F_StartDay BETWEEN @BeginDate AND @EndDate
	            AND T_ST_MeterParamInfo.F_IsEnergyValue = 1 
	            GROUP BY T_ST_CircuitMeterInfo.F_CircuitID,
	            CASE WHEN T_DT_NonWorkDays.F_BuildID IS NOT NULL THEN 'NoWork' ELSE 'Work' END
             )
	            SELECT * FROM TempA PIVOT(MAX(Value) FOR DayType IN([Work],[NoWork]) ) AS PVT order by ID ";

        public static string SELECT_NoWorkDayByBuildIDCircuitID =
           @"WITH TempA AS(
	            SELECT T_ST_CircuitMeterInfo.F_CircuitID ID,
	            MAX(T_ST_CircuitMeterInfo.F_CircuitName) AS Name, 
	            CASE WHEN T_DT_NonWorkDays.F_BuildID IS NOT NULL THEN 'NoWork' ELSE 'Work' END DayType,
	            SUM(T_MC_MeterDayResult.F_Value) Value  
	            FROM T_ST_CircuitMeterInfo INNER JOIN T_BD_BuildBaseInfo ON T_BD_BuildBaseInfo.F_BuildID = T_ST_CircuitMeterInfo.F_BuildID 
	            INNER JOIN T_DT_EnergyItemDict ON T_ST_CircuitMeterInfo.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
	            LEFT OUTER JOIN T_ST_MeterUseInfo ON T_ST_MeterUseInfo.F_MeterID = T_ST_CircuitMeterInfo.F_MeterID 
	            LEFT OUTER JOIN T_MC_MeterDayResult ON T_MC_MeterDayResult.F_MeterID = T_ST_MeterUseInfo.F_MeterID 
	            LEFT OUTER JOIN T_DT_NonWorkDays ON T_MC_MeterDayResult.F_StartDay = T_DT_NonWorkDays.F_Date 
	            LEFT OUTER JOIN T_ST_MeterParamInfo ON T_MC_MeterDayResult.F_MeterParamID = T_ST_MeterParamInfo.F_MeterParamID  
	            WHERE T_ST_CircuitMeterInfo.F_BuildID = @BuildID 
                AND T_ST_CircuitMeterInfo.F_EnergyItemCode=@Code
	            AND T_MC_MeterDayResult.F_StartDay BETWEEN @BeginDate AND @EndDate
	            AND T_ST_MeterParamInfo.F_IsEnergyValue = 1 
	            AND T_ST_CircuitMeterInfo.F_CircuitID IN ({0})
	            GROUP BY T_ST_CircuitMeterInfo.F_CircuitID,
	            CASE WHEN T_DT_NonWorkDays.F_BuildID IS NOT NULL THEN 'NoWork' ELSE 'Work' END
             )
	            SELECT * FROM TempA PIVOT(MAX(Value) FOR DayType IN([Work],[NoWork]) ) AS PVT order by ID ";

    }
}
