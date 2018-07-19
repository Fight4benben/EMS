using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class RegionReportResources
    {
        /// <summary>
        /// 查询小时表的当天每个小时的数据，需要先传入构造的区域ID，
        /// </summary>
        public static string DayReportSQL = @"SELECT Region.F_RegionID AS ID,Region.F_RegionName AS Name, HourResult.F_StartHour AS 'Time'
                                                    ,SUM( (CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*HourResult.F_Value * RegionMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterHourResult HourResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON HourResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_RegionMeter RegionMeter ON HourResult.F_MeterID = RegionMeter.F_MeterID
                                                    INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
                                                    WHERE Region.F_RegionID IN ({0})
                                                    AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND HourResult.F_StartHour BETWEEN DATEADD(DAY, DATEDIFF(DAY, 0, @EndTime), 0) 
							                                                    AND   DATEADD(SS,-3,DATEADD(DAY, DATEDIFF(DAY,0,@EndTime)+1, 0))
                                                    GROUP BY Region.F_RegionID,Region.F_RegionName ,HourResult.F_StartHour
                                                    ORDER BY Region.F_RegionID,'Time' ASC";

        /// <summary>
        /// 月报表
        /// 查询天表的每天的数据，需要先传入构造的ID
        /// </summary>
        public static string MonthReportSQL = @"SELECT Region.F_RegionID AS ID,Region.F_RegionName AS Name, DayResult.F_StartDay AS 'Time'
                                                    ,SUM( (CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
                                                    INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
                                                    WHERE Region.F_RegionID IN ({0})
                                                    AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) 
							                                                     AND DATEADD(SS,-3,DATEADD(MONTH, DATEDIFF(MONTH,0,@EndTime)+1, 0))
                                                    GROUP BY Region.F_RegionID,Region.F_RegionName ,DayResult.F_StartDay
                                                    ORDER BY Region.F_RegionID, 'Time' ASC";

        /// <summary>
        /// 年报表
        /// 查询天表的每天的数据，需要先传入构造的ID
        /// </summary>
        public static string YearReportSQL = @"SELECT Region.F_RegionID AS ID,Region.F_RegionName AS Name , DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0) AS 'Time'
                                                    ,SUM( (CASE WHEN RegionMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * RegionMeter.F_Rate/100) AS Value
                                                    FROM T_MC_MeterDayResult DayResult
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    INNER JOIN T_ST_RegionMeter RegionMeter ON DayResult.F_MeterID = RegionMeter.F_MeterID
                                                    INNER JOIN T_ST_Region Region ON Region.F_RegionID = RegionMeter.F_RegionID
                                                    WHERE Region.F_RegionID IN ({0})
                                                    AND Region.F_BuildID=@BuildID
                                                    AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND DayResult.F_StartDay BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime), 0) 
							                                                     AND DATEADD(SS,-3,DATEADD(YEAR, DATEDIFF(YEAR,0,@EndTime)+1, 0))
                                                    GROUP BY Region.F_RegionID,Region.F_RegionName ,DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0) 
                                                    ORDER BY Region.F_RegionID, 'Time' ASC";
        /// <summary>
        /// 获取区域列表
        /// 
        /// </summary>
        public static string TreeViewInfoSQL = @" SELECT Region.F_RegionID AS ID, F_RegionParentID AS ParentID,F_RegionName AS Name
                                                        FROM T_ST_Region AS Region
	                                                    INNER JOIN T_ST_RegionMeter RegionMeter ON Region.F_RegionID = RegionMeter.F_RegionID
	                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON RegionMeter.F_MeterID = Circuit.F_MeterID
	                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                        WHERE Region.F_BuildID=@BuildID
	                                                    AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
	                                                    GROUP BY Region.F_RegionID,F_RegionParentID,F_RegionName
                                                        ORDER BY ID";

    }
}
