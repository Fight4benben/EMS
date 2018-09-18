using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class MeterConnectStateResources
    {
        /// <summary>
        /// 查询所有仪表通讯状态
        /// </summary>
        public static string MeterAllStateSQL = @"
                                                SELECT  MeterUseInfo.F_MeterID AS ID
                                                        ,F_MeterName AS Name
                                                        ,DataCollectionInfo.F_CollectionName AS CollectionName
                                                        , CASE WHEN F_DisConnect = 0 THEN '通讯正常' ELSE '通讯中断' END AS States
                                                        ,F_DisConnectTime  AS DisConnectTime 
	                                                    , LEFT(CONVERT(VARCHAR(20),DATEADD(MINUTE,DATEDIFF(MINUTE,F_DisConnectTime,GETDATE()),0),114),8) DiffDate	
                                                    FROM T_ST_MeterUseInfo AS MeterUseInfo
                                                    INNER JOIN T_ST_DataCollectionInfo AS DataCollectionInfo ON DataCollectionInfo.F_CollectionID=MeterUseInfo.F_CollectionID
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON MeterUseInfo.F_MeterID = Circuit.F_MeterID
                                                    where MeterUseInfo.F_BuildID=@BuildID
                                                    AND Circuit.F_EnergyItemCode=@EnergyItemCode
                                                ";
    }
}
