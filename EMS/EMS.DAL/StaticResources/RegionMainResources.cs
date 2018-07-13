using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class RegionMainResources
    {
        public static string DayCompareSQL = @"SELECT 
                                            T_ST_Region.F_RegionID ID,T_ST_Region.F_RegionName Name,
                                            T_MC_MeterDayResult.F_StartDay 'Time',
                                            SUM((CASE WHEN T_ST_RegionMeter.F_Operator='加' THEN 1 ELSE -1 END)*T_ST_RegionMeter.F_Rate*T_MC_MeterDayResult.F_Value/100) Value 
                                            FROM T_ST_Region INNER JOIN T_ST_RegionMeter ON T_ST_Region.F_RegionID = T_ST_RegionMeter.F_RegionID 
                                            INNER JOIN T_ST_MeterUseInfo ON T_ST_RegionMeter.F_MeterID = T_ST_MeterUseInfo.F_MeterID 
                                            INNER JOIN T_MC_MeterDayResult ON T_ST_MeterUseInfo.F_MeterID = T_MC_MeterDayResult.F_MeterID 
                                            INNER JOIN T_ST_MeterParamInfo ON T_MC_MeterDayResult.F_MeterParamID = T_ST_MeterParamInfo.F_MeterParamID 
                                            INNER JOIN T_ST_MeterProdInfo ON T_ST_MeterUseInfo.F_MeterProdID=T_ST_MeterProdInfo.F_MeterProdID 
                                            INNER JOIN T_ST_CircuitMeterInfo ON T_ST_MeterUseInfo.F_MeterID = T_ST_CircuitMeterInfo.F_MeterID                                                           
                                            INNER JOIN T_DT_EnergyItemDict ON T_ST_CircuitMeterInfo.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode 
                                            LEFT JOIN T_ST_Region ParentRegion ON ParentRegion.F_RegionID=T_ST_Region.F_RegionParentID  
                                            WHERE (T_MC_MeterDayResult.F_StartDay BETWEEN  DATEADD(DD,-1,@FDate) AND @FDate ) 
                                            AND (T_ST_Region.F_BuildID=@BuildID) 
                                            AND (T_ST_MeterParamInfo.F_IsEnergyValue = 1) 
                                            AND (T_DT_EnergyItemDict.F_EnergyItemCode=@EnergyItemCode)
                                            AND {0}
                                            GROUP BY T_ST_Region.F_RegionID,T_ST_Region.F_RegionName,T_MC_MeterDayResult.F_StartDay
                                            ORDER BY 'Time',T_ST_Region.F_RegionID";

        public static string RankSQL = @"SELECT 
                                        T_ST_Region.F_RegionID ClassifyID,T_ST_Region.F_RegionName ClassifyName,T_ST_MeterUseInfo.F_MeterName Name,
                                        SUM(T_MC_MeterDayResult.F_Value) Value 
                                        FROM T_ST_Region INNER JOIN T_ST_RegionMeter ON T_ST_Region.F_RegionID = T_ST_RegionMeter.F_RegionID 
                                        INNER JOIN T_ST_MeterUseInfo ON T_ST_RegionMeter.F_MeterID = T_ST_MeterUseInfo.F_MeterID 
                                        INNER JOIN T_MC_MeterDayResult ON T_ST_MeterUseInfo.F_MeterID = T_MC_MeterDayResult.F_MeterID 
                                        INNER JOIN T_ST_MeterParamInfo ON T_MC_MeterDayResult.F_MeterParamID = T_ST_MeterParamInfo.F_MeterParamID 
                                        INNER JOIN T_ST_CircuitMeterInfo ON T_ST_MeterUseInfo.F_MeterID = T_ST_CircuitMeterInfo.F_MeterID                                                           
                                        INNER JOIN T_DT_EnergyItemDict ON T_ST_CircuitMeterInfo.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode 
                                        LEFT JOIN T_ST_Region ParentRegion ON ParentRegion.F_RegionID=T_ST_Region.F_RegionParentID  
                                        WHERE (T_MC_MeterDayResult.F_StartDay BETWEEN CONVERT(VARCHAR(7),@FDate,120)+'-01' AND @FDate) 
                                        AND (T_ST_Region.F_BuildID=@BuildID) 
                                        AND (T_ST_MeterParamInfo.F_IsEnergyValue = 1) 
                                        AND (T_DT_EnergyItemDict.F_EnergyItemCode=@EnergyItemCode)
                                        AND {0}
                                        GROUP BY T_ST_MeterUseInfo.F_MeterName,T_ST_Region.F_RegionID,T_ST_Region.F_RegionName
                                        ORDER BY ClassifyID,Value DESC";

        public static string PieSQL = @"SELECT 
                                        T_ST_Region.F_RegionID ID,MAX(T_ST_Region.F_RegionName) Name,
                                        SUM((CASE WHEN T_ST_RegionMeter.F_Operator='加' THEN 1 ELSE -1 END)*T_ST_RegionMeter.F_Rate*T_MC_MeterDayResult.F_Value/100) Value 
                                        FROM T_ST_Region INNER JOIN T_ST_RegionMeter ON T_ST_Region.F_RegionID = T_ST_RegionMeter.F_RegionID 
                                        INNER JOIN T_ST_MeterUseInfo ON T_ST_RegionMeter.F_MeterID = T_ST_MeterUseInfo.F_MeterID 
                                        INNER JOIN T_MC_MeterDayResult ON T_ST_MeterUseInfo.F_MeterID = T_MC_MeterDayResult.F_MeterID 
                                        INNER JOIN T_ST_MeterParamInfo ON T_MC_MeterDayResult.F_MeterParamID = T_ST_MeterParamInfo.F_MeterParamID 
                                        INNER JOIN T_ST_CircuitMeterInfo ON T_ST_MeterUseInfo.F_MeterID = T_ST_CircuitMeterInfo.F_MeterID                                                           
                                        INNER JOIN T_DT_EnergyItemDict ON T_ST_CircuitMeterInfo.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode 
                                        LEFT JOIN T_ST_Region ParentRegion ON ParentRegion.F_RegionID=T_ST_Region.F_RegionParentID  
                                        WHERE (T_MC_MeterDayResult.F_StartDay BETWEEN DATEADD(DAY,-30,@FDate) AND @FDate) 
                                        AND (T_ST_Region.F_BuildID=@BuildID) 
                                        AND (T_ST_MeterParamInfo.F_IsEnergyValue = 1) 
                                        AND (T_DT_EnergyItemDict.F_EnergyItemCode=@EnergyItemCode)
                                        AND {0}
                                        GROUP BY T_ST_Region.F_RegionID ";

        public static string BarTrendSQL = @"SELECT 
                                            T_ST_Region.F_RegionID ID,MAX(T_ST_Region.F_RegionName) Name,
                                            T_MC_MeterDayResult.F_StartDay 'Time',
                                            SUM((CASE WHEN T_ST_RegionMeter.F_Operator='加' THEN 1 ELSE -1 END)*T_ST_RegionMeter.F_Rate*T_MC_MeterDayResult.F_Value/100) Value 
                                            FROM T_ST_Region INNER JOIN T_ST_RegionMeter ON T_ST_Region.F_RegionID = T_ST_RegionMeter.F_RegionID 
                                            INNER JOIN T_ST_MeterUseInfo ON T_ST_RegionMeter.F_MeterID = T_ST_MeterUseInfo.F_MeterID 
                                            INNER JOIN T_MC_MeterDayResult ON T_ST_MeterUseInfo.F_MeterID = T_MC_MeterDayResult.F_MeterID 
                                            INNER JOIN T_ST_MeterParamInfo ON T_MC_MeterDayResult.F_MeterParamID = T_ST_MeterParamInfo.F_MeterParamID 
                                            INNER JOIN T_ST_CircuitMeterInfo ON T_ST_MeterUseInfo.F_MeterID = T_ST_CircuitMeterInfo.F_MeterID                                                           
                                            INNER JOIN T_DT_EnergyItemDict ON T_ST_CircuitMeterInfo.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode 
                                            LEFT JOIN T_ST_Region ParentRegion ON ParentRegion.F_RegionID=T_ST_Region.F_RegionParentID  
                                            WHERE (T_MC_MeterDayResult.F_StartDay BETWEEN DATEADD(DAY,-30,@FDate) AND @FDate) 
                                            AND (T_ST_Region.F_BuildID=@BuildID) 
                                            AND (T_ST_MeterParamInfo.F_IsEnergyValue = 1)
                                            AND (T_DT_EnergyItemDict.F_EnergyItemCode=@EnergyItemCode) 
                                            AND {0}
                                            GROUP BY T_ST_Region.F_RegionID,T_MC_MeterDayResult.F_StartDay
                                            ORDER BY 'Time' ASC,ID";

        public static string FilterSQL = @"SELECT F_BuildID BuildID,F_ExtendFunc ShowMode FROM T_BD_BuildExInfo
                                        WHERE F_BuildID = @BuildID";
    }
}
