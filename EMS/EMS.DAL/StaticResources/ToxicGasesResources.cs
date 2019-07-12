using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class ToxicGasesResources
    {
        /// <summary>
        /// 获取设备列表
        /// </summary>
        public static string SELECT_MeterList =
         @"SELECT T_ST_MeterUseInfo.F_MeterID ID,F_MeterName Name
              FROM T_ST_MeterUseInfo
              INNER JOIN T_ST_CircuitMeterInfo ON T_ST_MeterUseInfo.F_MeterID=T_ST_CircuitMeterInfo.F_MeterID
              WHERE T_ST_MeterUseInfo.F_BuildID =@BuildID
			  AND F_EnergyItemCode='P0001' ";

        public static string SELECT_MeterValue =
         @"SELECT HistoryData.F_MeterID ID,F_MeterName Name,F_MeterParaCode ParamCode,ParamInfo.F_MeterParamName ParamName,
		     ParamInfo.F_ParamUnit ParamUnit,F_RecentData Value
             FROM [EMSHistory].[dbo].[HistoryData]  
	         INNER JOIN [EMS].[dbo].T_ST_MeterParamInfo ParamInfo ON HistoryData.F_MeterParamID = ParamInfo.F_MeterParamID
	         INNER JOIN [EMS].[dbo].T_ST_MeterUseInfo ON HistoryData.F_MeterID = T_ST_MeterUseInfo.F_MeterID
	         WHERE F_Year=YEAR(GETDATE())
	         AND HistoryData.F_MeterID=@MeterID	            
		     AND HistoryData.F_BuildID=@BuildID ";

    }
}
