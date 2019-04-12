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
                                               SELECT  MeterUseInfo.F_MeterID AS ID, F_MeterName AS Name
                                                    ,DataCollectionInfo.F_CollectionName AS CollectionName
                                                    ,CASE WHEN  DATEDIFF(MINUTE,DataCollectionInfo.F_LastUpTime,GETDATE()) > 15 OR ( F_DisConnect = 1 OR DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())>15 )THEN '通讯中断' ELSE '通讯正常' END AS States
                                                    ,CASE WHEN F_DisConnectTime IS NULL  OR DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())<15 THEN NULL ELSE F_DisConnectTime END  AS DisConnectTime 
                                                    ,CASE WHEN DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())<15 THEN NULL ELSE  CAST(DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())/1440 as varchar(5)) 
	                                                    +'天'+CAST( DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())/60 as varchar(5)) 
	                                                    +'时'+CAST(DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())%60 as varchar(5))+'分' END DiffDate	
                                                    FROM T_ST_MeterUseInfo AS MeterUseInfo
                                                    INNER JOIN T_ST_DataCollectionInfo AS DataCollectionInfo ON DataCollectionInfo.F_CollectionID=MeterUseInfo.F_CollectionID
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON MeterUseInfo.F_MeterID = Circuit.F_MeterID
                                                    where MeterUseInfo.F_BuildID=@BuildID
                                                    AND Circuit.F_EnergyItemCode=@EnergyItemCode
                                                ";

        /// <summary>
        /// 查询离线仪表通讯状态
        /// </summary>
        public static string MeterOfflineStateSQL = @"
                                                     SELECT  MeterUseInfo.F_MeterID AS ID, F_MeterName AS Name
                                                        ,DataCollectionInfo.F_CollectionName AS CollectionName
                                                        ,CASE WHEN  DATEDIFF(MINUTE,DataCollectionInfo.F_LastUpTime,GETDATE()) > 15 OR ( F_DisConnect = 1 OR DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())>15 )THEN '通讯中断' ELSE '通讯正常' END AS States
                                                        ,CASE WHEN F_DisConnectTime IS NULL  OR DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())<15 THEN NULL ELSE F_DisConnectTime END  AS DisConnectTime 
                                                        ,CASE WHEN DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())<15 THEN NULL ELSE  CAST(DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())/1440 as varchar(5)) 
	                                                        +'天'+CAST( DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())/60 as varchar(5)) 
	                                                        +'时'+CAST(DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())%60 as varchar(5))+'分' END DiffDate		
                                                    FROM T_ST_MeterUseInfo AS MeterUseInfo
                                                    INNER JOIN T_ST_DataCollectionInfo AS DataCollectionInfo ON DataCollectionInfo.F_CollectionID=MeterUseInfo.F_CollectionID
                                                    INNER JOIN T_ST_CircuitMeterInfo Circuit ON MeterUseInfo.F_MeterID = Circuit.F_MeterID
                                                    where MeterUseInfo.F_BuildID=@BuildID
                                                    AND Circuit.F_EnergyItemCode=@EnergyItemCode
                                                    AND F_DisConnect=@Type
                                                    AND DATEDIFF(MINUTE,F_DisConnectTime,GETDATE())>=15
                                                ";
    }
}
