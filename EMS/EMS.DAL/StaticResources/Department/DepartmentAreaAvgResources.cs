using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    class DepartmentAreaAvgResources
    {
        /// <summary>
        /// 部门 月份 单位面积能耗排名
        /// </summary>
        public static string AreaAvgMonthSQL = @" 
                                            SELECT DepartmentInfo.F_DepartmentID AS ID ,DepartmentInfo.F_DepartmentName AS Name 
		                                            ,DepartmentExInfo.F_Area AS TotalArea 
		                                            ,SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) AS TotalValue
		                                            ,CASE WHEN F_Area = 0 THEN NULL 
		                                                ELSE Convert(DECIMAL(18,2),SUM((CASE WHEN DepartmentMeter.F_Operator ='加' THEN 1 ELSE -1 END)*DayResult.F_Value * DepartmentMeter.F_Rate/100) /F_Area) 
                                                        END AS  AreaAvg
		                                            FROM T_MC_MeterDayResult DayResult
		                                            INNER JOIN T_ST_CircuitMeterInfo Circuit ON DayResult.F_MeterID = Circuit.F_MeterID
		                                            INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
		                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		                                            INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DayResult.F_MeterID = DepartmentMeter.F_MeterID
		                                            INNER JOIN T_ST_DepartmentInfo DepartmentInfo ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
		                                            INNER JOIN T_ST_DepartmentInfo_ExInfo DepartmentExInfo ON DepartmentExInfo.F_DepartmentID = DepartmentInfo.F_DepartmentID
		                                            WHERE ParamInfo.F_IsEnergyValue = 1 
                                                    AND DepartmentInfo.F_BuildID=@BuildID
		                                            AND EnergyItem.F_EnergyItemCode = @EnergyItemCode
		                                            AND DayResult.F_StartDay BETWEEN @StartDay AND @EndDay 
		                                            GROUP BY DepartmentInfo.F_DepartmentID,DepartmentInfo.F_DepartmentName,DepartmentExInfo.F_People,DepartmentExInfo.F_Area
				                                            ,DATEADD(MONTH,DATEDIFF(MONTH,0,DayResult.F_StartDay),0)
				                                            ORDER BY AreaAvg DESC
                                           ";
    }
}
