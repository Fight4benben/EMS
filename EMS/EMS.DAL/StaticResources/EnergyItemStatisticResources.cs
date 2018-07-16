using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class EnergyItemStatisticResources
    {
        /// <summary>
        /// 当月计划用能数据
        /// </summary>
        public static string MonthPlanValueSQL = @"SELECT EnergyEstimateValue.F_EnergyItemCode AS ID, EnergyItemDict.F_EnergyItemName AS Name 
                                                        ,CAST(CONVERT(varchar(19), @EndTime, 120) as DATE) AS 'Time' ,SUM (EnergyEstimateValue.F_Value) AS Value
                                                        FROM T_ST_EnergyEstimateValue EnergyEstimateValue
                                                        INNER JOIN T_DT_EnergyItemDict EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = EnergyEstimateValue.F_EnergyItemCode
                                                        WHERE EnergyEstimateValue.F_BuildID=@BuildID
                                                        AND EnergyEstimateValue.F_Year=YEAR(@EndTime)
                                                        AND EnergyEstimateValue.F_Month= MONTH(@EndTime)
                                                        GROUP BY EnergyEstimateValue.F_EnergyItemCode,EnergyItemDict.F_EnergyItemName";

        /// <summary>
        /// 当年计划用能数据
        /// </summary>
        public static string YearPlanValueSQL = @"SELECT EnergyEstimateValue.F_EnergyItemCode AS ID, EnergyItemDict.F_EnergyItemName AS Name 
                                                        ,CAST(CONVERT(varchar(19), @EndTime, 120) as DATE) AS 'Time' ,SUM (EnergyEstimateValue.F_Value) AS Value
                                                        FROM T_ST_EnergyEstimateValue EnergyEstimateValue
                                                        INNER JOIN T_DT_EnergyItemDict EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = EnergyEstimateValue.F_EnergyItemCode
                                                        WHERE EnergyEstimateValue.F_BuildID=@BuildID
                                                        AND EnergyEstimateValue.F_Year=YEAR(@EndTime)
                                                        GROUP BY EnergyEstimateValue.F_EnergyItemCode,EnergyItemDict.F_EnergyItemName";

        /// <summary>
        /// 当月实际用能数据
        /// </summary>
        public static string MonthRealValueSQL = @"SELECT EnergyItem.F_EnergyItemCode AS ID, MAX(EnergyItem.F_EnergyItemName) Name
                                                    ,MAX(DayResult.F_StartDay) AS 'Time', SUM(F_Value) Value
                                                    FROM T_ST_CircuitMeterInfo Circuit 
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                    INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    WHERE Circuit.F_BuildID=@BuildID
                                                    AND Circuit.F_MainCircuit=1
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND F_StartDay BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, @EndTime), 0) 
                                                                   AND DATEADD(SS,-3,DATEADD(MONTH, DATEDIFF(MONTH,0,@EndTime)+1, 0))
                                                    GROUP BY EnergyItem.F_EnergyItemCode
                                                    ORDER BY EnergyItem.F_EnergyItemCode,'Time' ASC";

        /// <summary>
        /// 当年实际用能数据
        /// </summary>
        public static string YearRealValueSQL = @"SELECT EnergyItem.F_EnergyItemCode AS ID, MAX(EnergyItem.F_EnergyItemName) Name
                                                    ,MAX(DayResult.F_StartDay) AS 'Time', SUM(F_Value) Value
                                                    FROM T_ST_CircuitMeterInfo Circuit 
                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                    INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                    WHERE Circuit.F_BuildID=@BuildID
                                                    AND Circuit.F_MainCircuit=1
                                                    AND ParamInfo.F_IsEnergyValue = 1
                                                    AND F_StartDay BETWEEN DATEADD(YEAR, DATEDIFF(YEAR, 0, @EndTime), 0) 
                                                                   AND DATEADD(SS,-3,DATEADD(YEAR, DATEDIFF(YEAR,0,@EndTime)+1, 0))
                                                    GROUP BY EnergyItem.F_EnergyItemCode
                                                    ORDER BY EnergyItem.F_EnergyItemCode ,'Time' ASC";
        /// <summary>
        /// 用能数据单位
        /// </summary>
        public static string EnergyUnitSQL = @"SELECT EnergyItemDict.F_EnergyItemCode AS ID,MAX(F_EnergyItemName) AS Name
                                                      ,MAX(F_EnergyItemUnit) AS UnitName,MAX(F_EnergyItemUnitCode) AS UnitCode    
                                                    FROM T_DT_EnergyItemDict EnergyItemDict
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_EnergyItemCode = EnergyItemDict.F_EnergyItemCode
                                                    WHERE Circuit.F_BuildID=@BuildID
                                                    GROUP BY EnergyItemDict.F_EnergyItemCode";
    }
}
