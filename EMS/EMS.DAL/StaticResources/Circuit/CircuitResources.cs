using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class CircuitResources
    {
        /// <summary>
        /// 查询回路信息
        /// </summary>
        public static string CircuitSQL = @"SELECT F_CircuitID CircuitId,F_CircuitName CircuitName,F_MeterID MeterId,F_ParentID ParentId 
                                            FROM T_ST_CircuitMeterInfo Circuit
                                            WHERE F_BuildID=@BuildId
                                            AND F_EnergyItemCode=@EnergyItemCode";

        /// <summary>
        /// 查询小时表的当天每个小时的数据，需要先传入构造的CircuitsID
        /// </summary>
        public static string CircuitsDayReportSQL = @"SELECT Circuit.F_CircuitID Id, Circuit.F_CircuitName Name, 
                                                    F_StartHour 'Time',F_Value Value
                                                    FROM T_ST_CircuitMeterInfo Circuit
                                                    INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                    INNER JOIN T_MC_MeterHourResult HourResult ON Meter.F_MeterID = HourResult.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    WHERE 1=1
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND Circuit.F_CircuitID IN ({0})
                                                    AND F_StartHour Between CONVERT(VARCHAR(10),@EndDate,120)+' 00:00:00'and  CONVERT(VARCHAR(10),@EndDate,120)+' 23:00:00'";

        /// <summary>
        /// 查询当月中仪表每一天的数据
        /// </summary>
        public static string CircuitMonthReportSQL = @"SELECT Circuit.F_CircuitID Id, Circuit.F_CircuitName Name, 
                                                    F_StartDay 'Time',F_Value Value
                                                    FROM T_ST_CircuitMeterInfo Circuit
                                                    INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                    INNER JOIN T_MC_MeterDayResult DayResult ON Meter.F_MeterID = DayResult.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    WHERE 1=1
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND Circuit.F_CircuitID IN ({0})
                                                    AND F_StartDay BETWEEN DATEADD(MM, DATEDIFF(MM,0,@EndDate),0)
                                                    AND  DATEADD(DD,-DAY(@EndDate),DATEADD(MM,1,@EndDate))";
        /// <summary>
        /// 查询回路每个月的数据
        /// </summary>
        public static string CircuitYearReportSQL = @"SELECT Circuit.F_CircuitID Id, Circuit.F_CircuitName Name, 
                                                    DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0) 'Time',SUM(F_Value) Value
                                                    FROM T_ST_CircuitMeterInfo Circuit
                                                    INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                    INNER JOIN T_MC_MeterDayResult DayResult ON Meter.F_MeterID = DayResult.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    WHERE 1=1
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND Circuit.F_CircuitID IN ({0})
                                                    AND F_StartDay Between DATEADD(YY, DATEDIFF(YY,0,@EndDate), 0) 
                                                    AND  DATEADD(MS,-3,DATEADD(YY,DATEDIFF(YY,0,@EndDate)+1,0))
                                                    GROUP BY Circuit.F_CircuitID , Circuit.F_CircuitName , 
                                                    DATEADD(MM, DATEDIFF(MM,0,F_StartDay),0)";
    }
}
