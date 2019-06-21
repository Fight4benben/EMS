using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class MeterAlarmResources
    {
        public static string SELECT_AlarmingMeterTotalPage =
           @"SELECT COUNT(*) TotalNumber,COUNT(*)/@PageSize+1 TotalPage 
	            FROM T_MA_MeterALarming
	            INNER JOIN T_SYS_User_Buildings on T_MA_MeterALarming.F_BuildID=T_SYS_User_Buildings.F_BuildID
	            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
	            WHERE T_SYS_Users.F_UserName=@UserName ";

        public static string SELECT_AlarmingMeter =
           @"SELECT F_ID ID,tempA.F_BuildID BuildID,F_BuildName BuildName,tempA.F_MeterID MeterID,F_MeterName MeterName,
	            F_MeterParamName MeterParamName,F_ParamUnit ParamUnit,
	            CONVERT(varchar,F_Low) +'~'+CONVERT(varchar,F_High) AS NormalRange ,
	            F_Name TypeName,F_SetValue SetValue,F_AlarmValue AlarmValue,F_AlarmTime AlarmTime,F_IsConfirm IsConfirm
	            FROM( SELECT F_ID,T_MA_MeterALarming.F_BuildID,F_MeterID,F_MeterParamID,F_level,F_Type,
				            F_SetValue,F_AlarmValue,F_AlarmTime,F_IsConfirm,ROW_NUMBER() OVER (ORDER BY F_ID DESC ) pageID 
				            FROM T_MA_MeterALarming 
				            INNER JOIN T_SYS_User_Buildings on T_MA_MeterALarming.F_BuildID=T_SYS_User_Buildings.F_BuildID
				            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
				            WHERE T_SYS_Users.F_UserName=@UserName
	            ) tempA
	            INNER JOIN T_MA_MeterAlarmType on tempA.F_Type=T_MA_MeterAlarmType.F_Type
	            INNER JOIN T_MA_MeterALarmInfo on tempA.F_BuildID=T_MA_MeterALarmInfo.F_BuildID 
		            AND tempA.F_MeterID=T_MA_MeterALarmInfo.F_MeterID
		            AND tempA.F_MeterParamID=T_MA_MeterALarmInfo.F_MeterParamID
	            INNER JOIN T_ST_MeterUseInfo on tempA.F_MeterID=T_ST_MeterUseInfo.F_MeterID
	            INNER JOIN T_ST_MeterParamInfo on tempA.F_MeterParamID=T_ST_MeterParamInfo.F_MeterParamID
	            INNER JOIN T_BD_BuildBaseInfo on tempA.F_BuildID=T_BD_BuildBaseInfo.F_BuildID
	            WHERE 
	            pageID BETWEEN (@PageIndex-1) * @PageSize+1 and @PageSize*@PageIndex ";


        public static string SELECT_AlarmLogByUserTotalPage =
           @"SELECT COUNT(*) TotalNumber,COUNT(*)/@PageSize+1 TotalPage 
	            FROM T_MA_MeterAlarmLog
	            INNER JOIN T_SYS_User_Buildings on T_MA_MeterAlarmLog.F_BuildID=T_SYS_User_Buildings.F_BuildID
	            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
	            WHERE T_SYS_Users.F_UserName=@UserName
                AND T_MA_MeterAlarmLog.F_AlarmTime BETWEEN @BeginDate AND @EndDate ";


        public static string SELECT_AlarmLogByUser =
           @"SELECT F_ID ID,tempA.F_BuildID BuildID,F_BuildName BuildName,tempA.F_MeterID MeterID,F_MeterName MeterName,
	            F_MeterParamName MeterParamName,F_ParamUnit ParamUnit,
	            CONVERT(varchar,F_Low) +'~'+CONVERT(varchar,F_High) AS NormalRange ,
	            F_Name TypeName,F_SetValue SetValue,F_AlarmValue AlarmValue,F_AlarmTime AlarmTime,F_IsConfirm IsConfirm
	            ,F_RecoverValue RecoverValue,F_RecoverTime RecoverTime,F_ConfirmUser ConfirmUser,
	            F_ConfirmTime ConfirmTime,F_Describe Describe
	            FROM( SELECT F_ID,T_MA_MeterAlarmLog.F_BuildID,F_MeterID,F_MeterParamID,F_level,F_Type,
				            F_SetValue,F_AlarmValue,F_AlarmTime,F_IsConfirm,F_RecoverValue,F_RecoverTime,
				            F_ConfirmUser,F_ConfirmTime,F_Describe,
				            ROW_NUMBER() OVER (ORDER BY F_ID DESC ) pageID 
				            FROM T_MA_MeterAlarmLog
				            INNER JOIN T_SYS_User_Buildings on T_MA_MeterAlarmLog.F_BuildID=T_SYS_User_Buildings.F_BuildID
				            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
				            WHERE T_SYS_Users.F_UserName=@UserName
                            AND T_MA_MeterAlarmLog.F_AlarmTime BETWEEN @BeginDate AND @EndDate
	            ) tempA
	            INNER JOIN T_MA_MeterAlarmType on tempA.F_Type=T_MA_MeterAlarmType.F_Type
	            INNER JOIN T_MA_MeterALarmInfo on tempA.F_BuildID=T_MA_MeterALarmInfo.F_BuildID 
		            AND tempA.F_MeterID=T_MA_MeterALarmInfo.F_MeterID
		            AND tempA.F_MeterParamID=T_MA_MeterALarmInfo.F_MeterParamID
	            INNER JOIN T_ST_MeterUseInfo on tempA.F_MeterID=T_ST_MeterUseInfo.F_MeterID
	            INNER JOIN T_ST_MeterParamInfo on tempA.F_MeterParamID=T_ST_MeterParamInfo.F_MeterParamID
	            INNER JOIN T_BD_BuildBaseInfo on tempA.F_BuildID=T_BD_BuildBaseInfo.F_BuildID
	            WHERE pageID BETWEEN (@PageIndex-1) * @PageSize+1 and @PageSize*@PageIndex ";


        public static string SELECT_AlarmLogByBuildIDTotalPage =
           @"SELECT COUNT(*) TotalNumber,COUNT(*)/@PageSize+1 TotalPage 
	            FROM T_MA_MeterAlarmLog
	            INNER JOIN T_SYS_User_Buildings on T_MA_MeterAlarmLog.F_BuildID=T_SYS_User_Buildings.F_BuildID
	            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
	            WHERE T_SYS_Users.F_UserName=@UserName
                AND T_MA_MeterAlarmLog.F_BuildID = @BuildID
                AND T_MA_MeterAlarmLog.F_AlarmTime BETWEEN @BeginDate AND @EndDate ";

        public static string SELECT_AlarmLogByBuildID =
           @"SELECT F_ID ID,tempA.F_BuildID BuildID,F_BuildName BuildName,tempA.F_MeterID MeterID,F_MeterName MeterName,
	            F_MeterParamName MeterParamName,F_ParamUnit ParamUnit,
	            CONVERT(varchar,F_Low) +'~'+CONVERT(varchar,F_High) AS NormalRange ,
	            F_Name TypeName,F_SetValue SetValue,F_AlarmValue AlarmValue,F_AlarmTime AlarmTime,F_IsConfirm IsConfirm
	            ,F_RecoverValue RecoverValue,F_RecoverTime RecoverTime,F_ConfirmUser ConfirmUser,
	            F_ConfirmTime ConfirmTime,F_Describe Describe
	            FROM( SELECT F_ID,T_MA_MeterAlarmLog.F_BuildID,F_MeterID,F_MeterParamID,F_level,F_Type,
				            F_SetValue,F_AlarmValue,F_AlarmTime,F_IsConfirm,F_RecoverValue,F_RecoverTime,
				            F_ConfirmUser,F_ConfirmTime,F_Describe,
				            ROW_NUMBER() OVER (ORDER BY F_ID DESC ) pageID 
				            FROM T_MA_MeterAlarmLog
				            INNER JOIN T_SYS_User_Buildings on T_MA_MeterAlarmLog.F_BuildID=T_SYS_User_Buildings.F_BuildID
				            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
				            WHERE T_SYS_Users.F_UserName = @UserName
                            AND T_MA_MeterAlarmLog.F_BuildID = @BuildID
                            AND T_MA_MeterAlarmLog.F_AlarmTime BETWEEN @BeginDate AND @EndDate
	            ) tempA
	            INNER JOIN T_MA_MeterAlarmType on tempA.F_Type=T_MA_MeterAlarmType.F_Type
	            INNER JOIN T_MA_MeterALarmInfo on tempA.F_BuildID=T_MA_MeterALarmInfo.F_BuildID 
		            AND tempA.F_MeterID=T_MA_MeterALarmInfo.F_MeterID
		            AND tempA.F_MeterParamID=T_MA_MeterALarmInfo.F_MeterParamID
	            INNER JOIN T_ST_MeterUseInfo on tempA.F_MeterID=T_ST_MeterUseInfo.F_MeterID
	            INNER JOIN T_ST_MeterParamInfo on tempA.F_MeterParamID=T_ST_MeterParamInfo.F_MeterParamID
	            INNER JOIN T_BD_BuildBaseInfo on tempA.F_BuildID=T_BD_BuildBaseInfo.F_BuildID
	            WHERE pageID BETWEEN (@PageIndex-1) * @PageSize+1 and @PageSize*@PageIndex ";


        public static string SELECT_AlarmLogByMeterIDTotalPage =
           @"SELECT COUNT(*) TotalNumber,COUNT(*)/@PageSize+1 TotalPage 
	            FROM T_MA_MeterAlarmLog
	            INNER JOIN T_SYS_User_Buildings on T_MA_MeterAlarmLog.F_BuildID=T_SYS_User_Buildings.F_BuildID
	            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
	            WHERE T_SYS_Users.F_UserName=@UserName
                AND T_MA_MeterAlarmLog.F_BuildID = @BuildID 
                AND T_MA_MeterAlarmLog.F_MeterID = @MeterID
                AND T_MA_MeterAlarmLog.F_AlarmTime BETWEEN @BeginDate AND @EndDate ";

        public static string SELECT_AlarmLogByMeterID =
           @"SELECT F_ID ID,tempA.F_BuildID BuildID,F_BuildName BuildName,tempA.F_MeterID MeterID,F_MeterName MeterName,
	            F_MeterParamName MeterParamName,F_ParamUnit ParamUnit,
	            CONVERT(varchar,F_Low) +'~'+CONVERT(varchar,F_High) AS NormalRange ,
	            F_Name TypeName,F_SetValue SetValue,F_AlarmValue AlarmValue,F_AlarmTime AlarmTime,F_IsConfirm IsConfirm
	            ,F_RecoverValue RecoverValue,F_RecoverTime RecoverTime,F_ConfirmUser ConfirmUser,
	            F_ConfirmTime ConfirmTime,F_Describe Describe
	            FROM( SELECT F_ID,T_MA_MeterAlarmLog.F_BuildID,F_MeterID,F_MeterParamID,F_level,F_Type,
				            F_SetValue,F_AlarmValue,F_AlarmTime,F_IsConfirm,F_RecoverValue,F_RecoverTime,
				            F_ConfirmUser,F_ConfirmTime,F_Describe,
				            ROW_NUMBER() OVER (ORDER BY F_ID DESC ) pageID 
				            FROM T_MA_MeterAlarmLog
				            INNER JOIN T_SYS_User_Buildings on T_MA_MeterAlarmLog.F_BuildID=T_SYS_User_Buildings.F_BuildID
				            INNER JOIN T_SYS_Users on T_SYS_User_Buildings.F_UserName=T_SYS_Users.F_UserName
				            WHERE T_SYS_Users.F_UserName = @UserName
                            AND T_MA_MeterAlarmLog.F_BuildID = @BuildID
                            AND T_MA_MeterAlarmLog.F_MeterID = @MeterID
                            AND T_MA_MeterAlarmLog.F_AlarmTime BETWEEN @BeginDate AND @EndDate
	            ) tempA
	            INNER JOIN T_MA_MeterAlarmType on tempA.F_Type=T_MA_MeterAlarmType.F_Type
	            INNER JOIN T_MA_MeterALarmInfo on tempA.F_BuildID=T_MA_MeterALarmInfo.F_BuildID 
		            AND tempA.F_MeterID=T_MA_MeterALarmInfo.F_MeterID
		            AND tempA.F_MeterParamID=T_MA_MeterALarmInfo.F_MeterParamID
	            INNER JOIN T_ST_MeterUseInfo on tempA.F_MeterID=T_ST_MeterUseInfo.F_MeterID
	            INNER JOIN T_ST_MeterParamInfo on tempA.F_MeterParamID=T_ST_MeterParamInfo.F_MeterParamID
	            INNER JOIN T_BD_BuildBaseInfo on tempA.F_BuildID=T_BD_BuildBaseInfo.F_BuildID
	            WHERE pageID BETWEEN (@PageIndex-1) * @PageSize+1 and @PageSize*@PageIndex ";


        public static string UPDATE_ConfirmOne =
           @"UPDATE T_MA_MeterAlarmLog SET F_IsConfirm=1,F_ConfirmUser=@UserName,
	            F_ConfirmTime=GETDATE(),F_Describe =@Describe
	            FROM T_MA_MeterAlarmLog
	            INNER JOIN T_MA_MeterALarming ON T_MA_MeterAlarmLog.F_BuildID=T_MA_MeterALarming.F_BuildID
		            AND T_MA_MeterAlarmLog.F_MeterID=T_MA_MeterALarming.F_MeterID
		            AND T_MA_MeterAlarmLog.F_MeterParamID=T_MA_MeterALarming.F_MeterParamID
		            AND T_MA_MeterAlarmLog.F_Type=T_MA_MeterALarming.F_Type
	            WHERE T_MA_MeterALarming.F_ID IN ( {0} )
	            AND T_MA_MeterAlarmLog.F_IsConfirm =0
	            AND T_MA_MeterAlarmLog.F_ConfirmTime IS NULL

	            DELETE T_MA_MeterALarming 
	            WHERE F_ID IN ( {0} ) ";

        public static string UPDATE_ConfirmAll =
           @"UPDATE T_MA_MeterAlarmLog SET F_IsConfirm=1,F_ConfirmUser=@UserName,
	            F_ConfirmTime=GETDATE(),F_Describe =@Describe
	            FROM T_MA_MeterAlarmLog
	            INNER JOIN T_MA_MeterALarming ON T_MA_MeterAlarmLog.F_BuildID=T_MA_MeterALarming.F_BuildID
		            AND T_MA_MeterAlarmLog.F_MeterID=T_MA_MeterALarming.F_MeterID
		            AND T_MA_MeterAlarmLog.F_MeterParamID=T_MA_MeterALarming.F_MeterParamID
		            AND T_MA_MeterAlarmLog.F_Type=T_MA_MeterALarming.F_Type
	            WHERE T_MA_MeterAlarmLog.F_IsConfirm =0
	            AND T_MA_MeterAlarmLog.F_ConfirmTime IS NULL

	            DELETE FROM T_MA_MeterALarming  ";
    }
}
