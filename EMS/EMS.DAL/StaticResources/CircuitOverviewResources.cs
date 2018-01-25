using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class CircuitOverviewResources
    {
        /// <summary>
        /// 当日负荷曲线图
        /// </summary>
        public static string CircuitLoadSQL = @"SELECT MAX(Circuit.F_CircuitID) AS CircuitID, MAX(EnergyItemDict.F_EnergyItemCode) AS EnergyItemCode,
                                                FifteenResult.F_StartTime AS 'Time', FifteenResult.F_Value *4 AS Value
                                                FROM T_MC_MeterFifteenResult AS FifteenResult
                                                INNER JOIN T_ST_CircuitMeterInfo AS Circuit ON FifteenResult.F_MeterID = Circuit.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict AS EnergyItemDict ON EnergyItemDict.F_EnergyItemCode = Circuit.F_EnergyItemCode
                                                INNER JOIN T_ST_MeterParamInfo AS ParamInfo ON ParamInfo.F_MeterParamID = FifteenResult.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildID 
                                                AND Circuit.F_CircuitID=@CircuitID
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND FifteenResult.F_StartTime BETWEEN CONVERT(VARCHAR(10),@EndTime,120)+' 00:00:00' AND @EndTime
                                                GROUP BY FifteenResult.F_StartTime ,FifteenResult.F_Value
                                                ORDER BY  FifteenResult.F_StartTime ASC";
    }
}
