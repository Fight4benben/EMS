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
        public static string GetBuildInfo = @"
                                             SELECT F_BuildID BuildID,F_DataCenterID DataCenterID,F_BuildName BuildName,F_AliasName AliasName,F_BuildOwner BuildOwner
                                                  ,F_State 'State',F_DistrictCode DistrictCode,F_BuildAddr BuildAddr,F_BuildLong BuildLong,F_BuildLat BuildLat
                                                  ,F_BuildYear BuildYear,F_UpFloor UpFloor,F_DownFloor DownFloor,F_BuildFunc BuildFunc,F_TotalArea TotalArea
                                                  ,F_AirArea AirArea,F_HeatArea HeatArea,F_AirType AirType,F_HeatType HeatType,F_BodyCoef BodyCoef
                                                  ,F_StruType StruType,F_WallMatType WallMatType,F_WallWarmType WallWarmType,F_WallWinType WallWinType,F_GlassType GlassType
                                                  ,F_WinFrameType WinFrameType,F_IsStandard IsStandard,F_DesignDept DesignDept,F_WorkDept WorkDept,F_CreateTime CreateTime
                                                  ,F_CreateUser,F_MonitorDate,F_AcceptDate,F_NumberOfPeople,F_SPArea
                                                  ,F_Image Image,F_TransCount TransCount,F_InstallCapacity InstallCapacity,F_OperateCapacity OperateCapacity,F_DesignMeters DesignMeters
                                                  ,F_Mobiles Mobiles,F_ModelID ModelID
                                              FROM T_BD_BuildBaseInfo
                                            ";
    }
}
