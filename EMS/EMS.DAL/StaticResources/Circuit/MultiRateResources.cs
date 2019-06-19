using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources.Circuit
{
    public class MultiRateResources
    {
        /// <summary>
        /// 查询复费率
        /// </summary>
        public static string MultiRateDaySQL =
            @"SELECT Circuit.F_CircuitID Id, MAx(Circuit.F_CircuitName) Name, ParamInfo.F_MeterParamName ParamName,
            F_StartHour 'Time',F_Value Value , SUM( F_Value*ParamInfo.F_Price ) Cost
            FROM T_ST_CircuitMeterInfo Circuit
            INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
            INNER JOIN T_MC_MeterHourResult ON Meter.F_MeterID = T_MC_MeterHourResult.F_MeterID
            INNER JOIN T_ST_MeterParamInfo ParamInfo ON T_MC_MeterHourResult.F_MeterParamID = ParamInfo.F_MeterParamID
            WHERE 1=1
            AND Circuit.F_BuildID=@BuildID
            AND Circuit.F_EnergyItemCode=@Code
	        AND ParamInfo.F_IsTimeBlock =1
            AND F_StartHour BETWEEN CONVERT(VARCHAR(10),@EndDate,120)+' 00:00:00'
            AND CONVERT(VARCHAR(10),@EndDate,120)+' 23:00:00' ";

        public static string MultiRateDayGroup =
            @" GROUP BY Circuit.F_CircuitID ,ParamInfo.F_MeterParamName,F_StartHour,F_Value
	        ORDER BY Circuit.F_CircuitID,F_StartHour,ParamInfo.F_MeterParamName ASC  ";

        public static string MultiRateMonthSQL =
            @"SELECT Circuit.F_CircuitID Id, MAx(Circuit.F_CircuitName) Name, ParamInfo.F_MeterParamName ParamName,
            F_StartDay 'Time',F_Value Value , SUM( F_Value*ParamInfo.F_Price ) Cost
            FROM T_ST_CircuitMeterInfo Circuit
            INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
            INNER JOIN T_MC_MeterDayResult DayResult ON Meter.F_MeterID = DayResult.F_MeterID
            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
            WHERE 1=1
            AND Circuit.F_BuildID=@BuildID
            AND Circuit.F_EnergyItemCode=@Code
	        AND ParamInfo.F_IsTimeBlock =1
            AND F_StartDay BETWEEN DATEADD(MM, DATEDIFF(MM,0,@EndDate),0)
            AND DATEADD(DD,-DAY(@EndDate),DATEADD(MM,1,@EndDate)) ";

        public static string MultiRateMonthGroup =
            @" GROUP BY Circuit.F_CircuitID ,ParamInfo.F_MeterParamName,F_StartDay,F_Value
	        ORDER BY Circuit.F_CircuitID,F_StartDay,ParamInfo.F_MeterParamName ASC ";

        public static string MultiRateYearSQL =
            @"SELECT Circuit.F_CircuitID Id, MAx(Circuit.F_CircuitName) Name, ParamInfo.F_MeterParamName ParamName,
            DATEADD(MM, DATEDIFF(MM,0, F_StartDay),0) 'Time',SUM(F_Value) Value , SUM( F_Value*ParamInfo.F_Price ) Cost
            FROM T_ST_CircuitMeterInfo Circuit
            INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
            INNER JOIN T_MC_MeterDayResult DayResult ON Meter.F_MeterID = DayResult.F_MeterID
            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
            WHERE 1=1
            AND Circuit.F_BuildID=@BuildID
            AND Circuit.F_EnergyItemCode=@Code
	        AND ParamInfo.F_IsTimeBlock =1
            AND F_StartDay BETWEEN CONVERT(VARCHAR(4),@EndDate,120)+'-01-01 00:00:00'
            AND CONVERT(VARCHAR(4),@EndDate,120)+'-12-31 23:00:00' ";

        public static string MultiRateYearGroup =
            @" GROUP BY Circuit.F_CircuitID ,ParamInfo.F_MeterParamName, DATEADD(MM, DATEDIFF(MM,0, F_StartDay),0)
	        ORDER BY Circuit.F_CircuitID,'Time',ParamInfo.F_MeterParamName ASC ";

        public static string MultiRateIdsIN =
            @"  AND Circuit.F_CircuitID IN ({0})";


    }
}
