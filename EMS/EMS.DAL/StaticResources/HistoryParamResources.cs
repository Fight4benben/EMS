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
        public static string MeterParamInfoSQL = @"SELECT Circuit.F_CircuitID AS CircuitID,ParamInfo.F_MeterParamID AS ParamID
                                                        , ParamInfo.F_MeterParamName AS ParamName, ParamInfo.F_MeterParaCode AS ParaCode
                                                        , ParamInfo.F_ParamUnit AS ParamUnit, F_ParamType AS ParamType
                                                      FROM T_ST_CircuitMeterInfo Circuit
                                                      INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                      INNER JOIN T_ST_MeterParamInfo ParamInfo ON Meter.F_MeterProdID = ParamInfo.F_MeterProdID
                                                      INNER JOIN T_DT_Param ParamDict ON ParamDict.F_ParamCode = ParamInfo.F_MeterParaCode
                                                      WHERE Circuit.F_BuildID=@BuildID
                                                      AND F_CircuitID IN ({0})
                                                      ORDER BY Circuit.F_CircuitID,F_Order ASC";

        /// <summary>
        /// 获取参数查询的支路列表
        /// </summary>
        public static string TreeViewInfoSQL = @" SELECT F_CircuitID AS ID, F_ParentID AS ParentID,F_CircuitName AS Name
                                                        FROM T_ST_CircuitMeterInfo AS Circuit	
                                                        WHERE Circuit.F_BuildID=@BuildID
	                                                    AND F_EnergyItemCode=@EnergyItemCode
                                                        ORDER BY ID ASC ";
    }
}
