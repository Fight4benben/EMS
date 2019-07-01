using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class BuildSetResources
    {
        /// <summary>
        ///获取建筑信息
        /// </summary>
        public static string GetBuildList = @"
                                             SELECT F_BuildID BuildID,F_BuildName BuildName                               
                                                FROM T_BD_BuildBaseInfo
                                            ";

        /// <summary>
        ///获取建筑信息
        /// </summary>
        public static string GetBuildInfo = @"
                                             SELECT F_BuildID BuildID,F_DataCenterID DataCenterID,F_BuildName BuildName,F_AliasName AliasName,F_BuildOwner BuildOwner
		                                            ,F_State 'State',F_DistrictCode DistrictCode,F_BuildAddr BuildAddr,F_BuildLong BuildLong,F_BuildLat BuildLat
		                                            ,F_BuildYear BuildYear,F_UpFloor UpFloor,F_DownFloor DownFloor,F_BuildFunc BuildFunc,F_TotalArea TotalArea
		                                            ,F_AirArea AirArea,F_HeatArea HeatArea,F_AirType AirType,F_HeatType HeatType,F_BodyCoef BodyCoef
		                                            ,F_StruType StruType,F_WallMatType WallMatType,F_WallWarmType WallWarmType,F_WallWinType WallWinType,F_GlassType GlassType
		                                            ,F_WinFrameType WinFrameType,F_IsStandard IsStandard,F_DesignDept DesignDept,F_WorkDept WorkDept,F_CreateTime CreateTime
		                                            ,F_CreateUser CreateUser,F_MonitorDate MonitorDate,F_AcceptDate AcceptDate,F_NumberOfPeople NumberOfPeople ,F_SPArea SPArea
		                                            ,NULL 'Image',F_TransCount TransCount,F_InstallCapacity InstallCapacity,F_OperateCapacity OperateCapacity,F_DesignMeters DesignMeters
		                                            ,F_Mobiles Mobiles,F_ModelID ModelID
                                              FROM T_BD_BuildBaseInfo WHERE F_BuildID=@BuildID
                                            ";

        /// <summary>
        ///添加或者修改 建筑信息
        /// </summary>
        public static string AddBuildInfo = @"
	                                        INSERT INTO T_BD_BuildBaseInfo
		                                        (F_BuildID,F_DataCenterID,F_BuildName,F_AliasName,F_BuildOwner
		                                        ,F_DistrictCode,F_BuildAddr,F_BuildLong,F_BuildLat,F_BuildYear
		                                        ,F_UpFloor,F_DownFloor,F_BuildFunc,F_TotalArea,F_AirArea
		                                        ,F_DesignDept,F_WorkDept,F_CreateTime,F_CreateUser,F_MonitorDate
		                                        ,F_AcceptDate,F_NumberOfPeople,F_SPArea,F_TransCount
		                                        ,F_InstallCapacity,F_OperateCapacity,F_DesignMeters,F_Mobiles
		                                        ) VALUES
		                                        (@BuildID,@DataCenterID,@BuildName,@AliasName,@BuildOwner
		                                        ,@DistrictCode,@BuildAddr,@BuildLong,@BuildLat,@BuildYear
		                                        ,@UpFloor,@DownFloor,@BuildFunc,@TotalArea,@AirArea
		                                        ,@DesignDept,@WorkDept,@CreateTime,@CreateUser,@MonitorDate
		                                        ,@AcceptDate,@NumberOfPeople,@SPArea,@TransCount
		                                        ,@InstallCapacity,@OperateCapacity,@DesignMeters,@Mobiles)
                                            ";

        public static string UpdateBuildInfo = @"
	                                            UPDATE T_BD_BuildBaseInfo 
		                                            SET F_DataCenterID=@DataCenterID, F_BuildName=@BuildName, F_AliasName=@AliasName, F_BuildOwner=@BuildOwner
			                                            ,F_DistrictCode=@DistrictCode, F_BuildAddr=@BuildAddr, F_BuildLong=@BuildLong, F_BuildLat =@BuildLat
			                                            ,F_BuildYear=@BuildYear, F_UpFloor=@UpFloor, F_DownFloor=@DownFloor, F_BuildFunc=@BuildFunc
			                                            ,F_TotalArea=@TotalArea, F_AirArea=@AirArea,F_DesignDept=@DesignDept, F_WorkDept=@WorkDept
			                                            ,F_CreateTime=@CreateTime, F_CreateUser=@CreateUser, F_MonitorDate=@MonitorDate, F_AcceptDate=@AcceptDate
			                                            ,F_NumberOfPeople=@NumberOfPeople, F_SPArea=@SPArea, F_TransCount=@TransCount
			                                            ,F_InstallCapacity=@InstallCapacity, F_OperateCapacity=@OperateCapacity, F_DesignMeters=@DesignMeters
			                                            ,F_Mobiles =@Mobiles
                                                    WHERE F_BuildID=@BuildID
                                            ";

        public static string SetBuildInfo = @"
                                            IF EXISTS (SELECT 1 FROM T_BD_BuildBaseInfo WHERE F_BuildID=@BuildID) 
	                                            UPDATE T_BD_BuildBaseInfo 
		                                            SET F_DataCenterID=@DataCenterID, F_BuildName=@BuildName, F_AliasName=@AliasName, F_BuildOwner=@BuildOwner
			                                            ,F_DistrictCode=@DistrictCode, F_BuildAddr=@BuildAddr, F_BuildLong=@BuildLong, F_BuildLat =@BuildLat
			                                            ,F_BuildYear=@BuildYear, F_UpFloor=@UpFloor, F_DownFloor=@DownFloor, F_BuildFunc=@BuildFunc
			                                            ,F_TotalArea=@TotalArea, F_AirArea=@AirArea,F_DesignDept=@DesignDept, F_WorkDept=@WorkDept
			                                            ,F_CreateTime=@CreateTime, F_CreateUser=@CreateUser, F_MonitorDate=@MonitorDate, F_AcceptDate=@AcceptDate
			                                            ,F_NumberOfPeople=@NumberOfPeople, F_SPArea=@SPArea, F_TransCount=@TransCount
			                                            ,F_InstallCapacity=@InstallCapacity, F_OperateCapacity=@OperateCapacity, F_DesignMeters=@DesignMeters
			                                            ,F_Mobiles =@Mobiles
		                                            WHERE F_BuildID=@BuildID
                                            ELSE
	                                            INSERT INTO T_BD_BuildBaseInfo
		                                            (F_BuildID,F_DataCenterID,F_BuildName,F_AliasName,F_BuildOwner
		                                            ,F_DistrictCode,F_BuildAddr,F_BuildLong,F_BuildLat,F_BuildYear
		                                            ,F_UpFloor,F_DownFloor,F_BuildFunc,F_TotalArea,F_AirArea
		                                            ,F_DesignDept,F_WorkDept,F_CreateTime,F_CreateUser,F_MonitorDate
		                                            ,F_AcceptDate,F_NumberOfPeople,F_SPArea,F_TransCount
		                                            ,F_InstallCapacity,F_OperateCapacity,F_DesignMeters,F_Mobiles
		                                            ) VALUES
		                                            (@BuildID,@DataCenterID,@BuildName,@AliasName,@BuildOwner
		                                            ,@DistrictCode,@BuildAddr,@BuildLong,@BuildLat,@BuildYear
		                                            ,@UpFloor,@DownFloor,@BuildFunc,@TotalArea,@AirArea
		                                            ,@DesignDept,@WorkDept,@CreateTime,@CreateUser,@MonitorDate
		                                            ,@AcceptDate,@NumberOfPeople,@SPArea,@TransCount
		                                            ,@InstallCapacity,@OperateCapacity,@DesignMeters,@Mobiles)
                                            ";

        /// <summary>
        ///获取建筑信息
        /// </summary>
        public static string DeleteBuildSQL = @"
                                              DELETE FROM T_BD_BuildBaseInfo WHERE F_BuildID=@BuildID
                                            ";
    }
}
