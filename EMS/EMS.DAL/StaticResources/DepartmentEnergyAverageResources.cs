using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class DepartmentEnergyAverageResources
    {
        /// <summary>
        /// 部门月度人均用能
        /// </summary>
        public static string DeptMonthAvgSQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
		                                            ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_People AS TotalPeople
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_People AS  PeopleAvg
		                                            FROM T_MC_MeterDayResult DayResult
		                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                            INNER JOIN T_ST_DepartmentInfo_ExInfo DepartmentExInfo ON DepartmentExInfo.F_DepartmentID = DepartmentInfo.F_DepartmentID
		                                            WHERE ParamInfo.F_IsEnergyValue = 1 
		                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                            AND DepartmentInfo.F_DepartmentID =@DepartmentID
		                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_People,DepartmentExInfo.F_Area
				                                            ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)";

        /// <summary>
        /// 部门-季度人均用能
        /// </summary>
        public static string DeptQuarterAvgSQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
		                                            ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_People AS TotalPeople
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_People AS  PeopleAvg
		                                            FROM T_MC_MeterDayResult DayResult
		                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                            INNER JOIN T_ST_DepartmentInfo_ExInfo DepartmentExInfo ON DepartmentExInfo.F_DepartmentID = DepartmentInfo.F_DepartmentID
		                                            WHERE ParamInfo.F_IsEnergyValue = 1 
		                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                            AND DepartmentInfo.F_DepartmentID =@DepartmentID
		                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_People,DepartmentExInfo.F_Area
				                                            ,DATEADD(QUARTER,DATEDIFF(QUARTER,0,DayResult.F_StartDay),0)";

        /// <summary>
        /// 部门-年度人均用能
        /// </summary>
        public static string DeptYearAvgSQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
		                                            ,DepartmentExInfo.F_Area AS TotalArea ,DepartmentExInfo.F_People AS TotalPeople
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area AS  AreaAvg
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_People AS  PeopleAvg
		                                            FROM T_MC_MeterDayResult DayResult
		                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                            INNER JOIN T_ST_DepartmentInfo_ExInfo DepartmentExInfo ON DepartmentExInfo.F_DepartmentID = DepartmentInfo.F_DepartmentID
		                                            WHERE ParamInfo.F_IsEnergyValue = 1 
		                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                            AND DepartmentInfo.F_DepartmentID =@DepartmentID
		                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_People,DepartmentExInfo.F_Area
				                                            ,DATEADD(YEAR,DATEDIFF(YEAR,0,DayResult.F_StartDay),0)";

        /// <summary>
        /// 部门列表
        /// </summary>
        public static string TreeViewInfoSQL = @" SELECT DepartmentInfo.F_DepartmentID AS ID, F_DepartParentID AS ParentID,F_DepartmentName AS Name
                                                        FROM T_ST_DepartmentInfo AS DepartmentInfo
	                                                    INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
	                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON DepartmentMeter.F_MeterID = Circuit.F_MeterID
	                                                    INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                                                        WHERE DepartmentInfo.F_BuildID=@BuildID
	                                                    AND EnergyItem.F_EnergyItemCode=@EnergyItemCode
	                                                    GROUP BY DepartmentInfo.F_DepartmentID,F_DepartParentID,F_DepartmentName
                                                        ORDER BY ID";
    }
}
