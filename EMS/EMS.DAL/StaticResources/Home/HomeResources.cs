using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class HomeResources
    {
        /// <summary>
        /// 查询分类能耗数据（当月数据，当年数据）
        /// </summary>
         public static string EnergyClassifySQL = @"SELECT CASE WHEN MonthTable.EnergyItemName IS NULL THEN YearTable.EnergyItemName ELSE MonthTable.EnergyItemName END EnergyItemName,
                                                MonthTable.Value AS MonthValue,YearTable.Value YearValue,
                                                CASE WHEN MonthTable.Unit IS NULL THEN YearTable.Unit ELSE MonthTable.Unit END Unit ,
                                                CASE WHEN MonthTable.EnergyRate IS NULL THEN  CAST(YearTable.EnergyRate AS decimal(8,4)) ELSE CAST(MonthTable.EnergyRate AS decimal(8,4)) END EnergyRate
                                                FROM (SELECT MAX(EnergyItem.F_EnergyItemName) EnergyItemName,SUM(F_Value) Value,
                                                MAX(F_EnergyItemUnit) Unit,MAX(EnergyItem.F_EnergyItemFml) EnergyRate,EnergyItem.F_EnergyItemCode EnergyItemCode
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
                                                (SELECT MAX(EnergyItem.F_EnergyItemName) EnergyItemName,SUM(F_Value) Value,MAX(F_EnergyItemUnit) Unit ,
                                                MAX(EnergyItem.F_EnergyItemFml) EnergyRate,EnergyItem.F_EnergyItemCode EnergyItemCode
                                                FROM T_ST_CircuitMeterInfo Circuit 
                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Circuit.F_BuildID=@BuildId
                                                AND Circuit.F_MainCircuit=1
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND F_StartDay BETWEEN DATEADD(YY,DATEDIFF(YY,0,@EndDate),0) AND @EndDate
                                                GROUP BY EnergyItem.F_EnergyItemCode)YearTable ON MonthTable.EnergyItemName = YearTable.EnergyItemName
                                                ORDER BY MonthTable.EnergyItemCode ASC,YearTable.EnergyItemCode ASC";

        /// <summary>
        /// 查询当月分项用能数据
        /// </summary>
        public static string EnergyItemSQL = @"SELECT Formula.F_BuildID BuildID,Formula.F_EnergyItemCode EnergyItemCode,
                                                MAX(EnergyItem.F_EnergyItemName) EnergyItemName,SUM(DayResult.F_Value) Value
                                                FROM T_ST_CalcFormula Formula
                                                INNER JOIN T_ST_CalcFormulaMeter FormulaMeter ON Formula.F_FormulaID = FormulaMeter.F_FormulaID
                                                INNER JOIN T_ST_MeterUseInfo Meter ON Meter.F_MeterID = FormulaMeter.F_MeterID
                                                INNER JOIN T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID = Meter.F_MeterID
                                                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Formula.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                INNER JOIN T_MC_MeterDayResult DayResult ON Meter.F_MeterID = DayResult.F_MeterID
                                                INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                                WHERE Formula.F_BuildID = @BuildId
                                                AND ParamInfo.F_IsEnergyValue = 1
                                                AND Right(EnergyItem.F_EnergyItemCode,3)<>'000'
                                                AND RIGHT(EnergyItem.F_EnergyItemCode,2) = '00'
                                                AND F_StartDay BETWEEN DATEADD(DD,-DAY(@EndDate)+1,@EndDate) AND @EndDate
                                                GROUP BY Formula.F_EnergyItemCode,Formula.F_BuildID,Circuit.F_EnergyItemCode
                                                ORDER BY Formula.F_EnergyItemCode ASC";
        /// <summary>
        /// 查询某一天用能报表，小时为分组；传入参数EndDate格式为“yyyy-MM-dd HH:mm:ss”
        /// </summary>
        public static string HourValueSQL = @"SELECT EnergyItem.F_EnergyItemCode EnergyItemCode,F_StartHour ValueTime,
                                            SUM(F_Value) Value
                                            FROM T_ST_CircuitMeterInfo Circuit
                                            INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                            INNER JOIN T_MC_MeterHourResult HourResult ON Circuit.F_MeterID = HourResult.F_MeterID
                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON HourResult.F_MeterParamID = ParamInfo.F_MeterParamID
                                            WHERE Circuit.F_MainCircuit = 1
                                            AND Circuit.F_BuildID = @BuildId
                                            AND ParamInfo.F_IsEnergyValue =1 
                                            AND F_StartHour BETWEEN CONVERT(VARCHAR(10),@EndDate,120)+' 00:00:00' AND  @EndDate
                                            GROUP BY EnergyItem.F_EnergyItemCode,F_StartHour
                                            ORDER BY EnergyItemCode ASC,ValueTime ASC";

        public static string MDSQL = @"SELECT Meter.F_MeterName Name,F_RecentTime 'Time',F_RecentData  Value
                                        FROM HistoryData
                                        INNER JOIN EMS.dbo.T_ST_MeterUseInfo Meter ON Meter.F_MeterID = HistoryData.F_MeterID
                                        WHERE HistoryData.F_BuildID = @BuildID
                                        AND F_TagName LIKE '%_MD'";

    }
}
