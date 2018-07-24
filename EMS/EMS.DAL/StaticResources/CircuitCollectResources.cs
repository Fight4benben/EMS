﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class CircuitCollectResources
    {
        /// <summary>
        /// 查询回路信息
        /// </summary>
        public static string CircuitInfoSQL = @"SELECT F_CircuitID AS CircuitID , F_CircuitName AS CircuitName
	                                                    ,Circuit.F_MeterID AS MeterID,ParamInfo.F_MeterParamID AS MeterParamID
                                                      FROM T_ST_CircuitMeterInfo Circuit
                                                      INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                                      INNER JOIN T_ST_MeterParamInfo ParamInfo ON Meter.F_MeterProdID = ParamInfo.F_MeterProdID
                                                      where ParamInfo.F_IsEnergyValue=1
                                                      AND F_CircuitID IN ({0})
                                                      AND Circuit.F_BuildID=@BuildID";
    }
}
