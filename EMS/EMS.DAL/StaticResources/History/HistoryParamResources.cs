using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class HistoryParamResources
    {
        /// <summary>
        /// 查询支路对应仪表的参数信息
        /// </summary>
        public static string ParamClassifySQL = @"SELECT F_Order AS ParamOrder, F_ParamType AS ParamType
	                                                    , ParamInfo.F_ParamUnit AS ParamUnit
                                                        FROM T_ST_CircuitMeterInfo Circuit
                                                        INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                        INNER JOIN T_ST_MeterParamInfo ParamInfo ON Meter.F_MeterProdID = ParamInfo.F_MeterProdID
                                                        INNER JOIN T_DT_Param ParamDict ON ParamDict.F_ParamCode = ParamInfo.F_MeterParaCode
                                                        WHERE Circuit.F_BuildID=@BuildID
                                                        AND F_CircuitID = @CircuitID
	                                                    group BY F_Order, F_ParamType, ParamInfo.F_ParamUnit
                                                        ORDER BY F_Order ASC";
        /// <summary>
        /// 查询支路对应仪表的参数信息
        /// </summary>
        public static string MeterParamSQL = @"SELECT ParamInfo.F_MeterParamID AS ParamID,ParamInfo.F_MeterParaCode AS ParamCode
	                                                , ParamInfo.F_MeterParamName AS ParamName,F_Order AS ParamOrder
                                                    ,F_ParamType AS ParamType,ParamInfo.F_ParamUnit AS ParamUnit
                                                    FROM T_ST_CircuitMeterInfo Circuit
                                                    INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON Meter.F_MeterProdID = ParamInfo.F_MeterProdID
                                                    INNER JOIN T_DT_Param ParamDict ON ParamDict.F_ParamCode = ParamInfo.F_MeterParaCode
                                                    WHERE Circuit.F_BuildID=@BuildID
                                                    AND F_CircuitID = @CircuitID
                                                    ORDER BY F_Order ASC";

        /// <summary>
        /// 获取参数查询的支路列表
        /// </summary>
        public static string TreeViewInfoSQL = @" SELECT F_CircuitID AS ID, F_ParentID AS ParentID,F_CircuitName AS Name
                                                        FROM T_ST_CircuitMeterInfo AS Circuit	
                                                        WHERE Circuit.F_BuildID=@BuildID
	                                                    AND F_EnergyItemCode=@EnergyItemCode
                                                        ORDER BY ID ASC ";
        /// <summary>
        /// 获取参数查询的支路列表
        /// </summary>
        public static string THEnergyItemSQL = @" SELECT EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(EnergyItemDict.F_EnergyItemName) EnergyItemName,
                                                MAX(EnergyItemDict.F_EnergyItemUnit) EnergyItemUnit
                                                FROM T_ST_CircuitMeterInfo Circuit
                                                INNER JOIN T_DT_EnergyItemDict EnergyItemDict ON Circuit.F_EnergyItemCode = EnergyItemDict.F_EnergyItemCode
                                                WHERE F_BuildID=@BuildId
                                                GROUP BY EnergyItemDict.F_EnergyItemCode ";
    }
}
