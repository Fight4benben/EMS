using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class PlatformResource
    {
        public static string SELECT_RunningDay =
          @"SELECT TOP 1  DATEDIFF(DAY,F_StartDay, GETDATE()) 
              FROM T_MC_MeterDayResult ";

        public static string SELECT_CountBuildID =
           @"SELECT COUNT(*) Number FROM T_BD_BuildBaseInfo
               INNER JOIN T_SYS_User_Buildings ON T_BD_BuildBaseInfo.F_BuildID = T_SYS_User_Buildings.F_BuildID
               INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
               WHERE T_SYS_Users.F_UserName = @UserName ";

        public static string SELECT_CountCollector =
           @"SELECT COUNT(*) Number FROM T_ST_DataCollectionInfo
               INNER JOIN T_SYS_User_Buildings ON T_ST_DataCollectionInfo.F_BuildID = T_SYS_User_Buildings.F_BuildID
               INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
               WHERE T_SYS_Users.F_UserName = @UserName ";

        public static string SELECT_CountMeter =
           @"SELECT COUNT(*) Number FROM T_ST_MeterUseInfo
               INNER JOIN T_SYS_User_Buildings ON T_ST_MeterUseInfo.F_BuildID = T_SYS_User_Buildings.F_BuildID
               INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
               WHERE T_SYS_Users.F_UserName = @UserName ";

        public static string SELECT_StandardcoalMonthValue =
           @"SELECT T_DT_EnergyItemDict.F_EnergyItemCode Id,MAX(T_DT_EnergyItemDict.F_EnergyItemName) Name,
		       SUM(F_Value * T_DT_EnergyItemDict.F_EnergyItemFml) Value,MAX(F_EnergyItemUnit) Unit
               FROM T_ST_CircuitMeterInfo Circuit 
               INNER JOIN T_DT_EnergyItemDict ON Circuit.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
               INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
               INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
		       INNER JOIN T_SYS_User_Buildings ON Circuit.F_BuildID = T_SYS_User_Buildings.F_BuildID
               INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
               WHERE T_SYS_Users.F_UserName=@UserName
               AND Circuit.F_MainCircuit=1
               AND ParamInfo.F_IsEnergyValue = 1
               AND F_StartDay BETWEEN DATEADD(DD,-DAY(@EndDate)+1,@EndDate) AND @EndDate
               GROUP BY T_DT_EnergyItemDict.F_EnergyItemCode 
		       ORDER BY T_DT_EnergyItemDict.F_EnergyItemCode ASC ";

        public static string SELECT_DayValue =
          @"SELECT CASE WHEN ThisTemp.EnergyItemCode IS NULL THEN LastTemp.EnergyItemCode ELSE ThisTemp.EnergyItemCode END Id,
                CASE WHEN ThisTemp.EnergyItemName IS NULL THEN LastTemp.EnergyItemName ELSE ThisTemp.EnergyItemName END Name,
		        ThisTemp.Value AS Value,LastTemp.LastValue LastValue,
                CASE WHEN ThisTemp.Unit IS NULL THEN LastTemp.Unit ELSE ThisTemp.Unit END Unit 
                FROM( 
			        SELECT T_DT_EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(T_DT_EnergyItemDict.F_EnergyItemName) EnergyItemName,
			        SUM(F_Value ) Value,MAX(F_EnergyItemUnit) Unit
			        FROM T_ST_CircuitMeterInfo Circuit 
			        INNER JOIN T_DT_EnergyItemDict ON Circuit.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
			        INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
			        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			        INNER JOIN T_SYS_User_Buildings ON Circuit.F_BuildID = T_SYS_User_Buildings.F_BuildID
			        INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
			        WHERE T_SYS_Users.F_UserName=@UserName
			        AND Circuit.F_MainCircuit=1
			        AND ParamInfo.F_IsEnergyValue = 1
			        AND F_StartDay = @EndDate
			        GROUP BY T_DT_EnergyItemDict.F_EnergyItemCode) AS ThisTemp
		        FULL JOIN
		          (
			        SELECT T_DT_EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(T_DT_EnergyItemDict.F_EnergyItemName) EnergyItemName,
			        SUM(F_Value ) LastValue,MAX(F_EnergyItemUnit) Unit
			        FROM T_ST_CircuitMeterInfo Circuit 
			        INNER JOIN T_DT_EnergyItemDict ON Circuit.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
			        INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
			        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			        INNER JOIN T_SYS_User_Buildings ON Circuit.F_BuildID = T_SYS_User_Buildings.F_BuildID
			        INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
			        WHERE T_SYS_Users.F_UserName=@UserName
			        AND Circuit.F_MainCircuit=1
			        AND ParamInfo.F_IsEnergyValue = 1
			        AND F_StartDay = DATEADD( DD,-1,@EndDate) 
			        GROUP BY T_DT_EnergyItemDict.F_EnergyItemCode ) AS LastTemp
		        ON ThisTemp.EnergyItemCode=LastTemp.EnergyItemCode
		        ORDER BY Id ASC ";

        public static string SELECT_MonthValue =
           @"SELECT CASE WHEN ThisTemp.EnergyItemCode IS NULL THEN LastTemp.EnergyItemCode ELSE ThisTemp.EnergyItemCode END Id,
                CASE WHEN ThisTemp.EnergyItemName IS NULL THEN LastTemp.EnergyItemName ELSE ThisTemp.EnergyItemName END Name,
		        ThisTemp.Value AS Value,LastTemp.LastValue LastValue,
                CASE WHEN ThisTemp.Unit IS NULL THEN LastTemp.Unit ELSE ThisTemp.Unit END Unit 
                FROM( 
			        SELECT T_DT_EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(T_DT_EnergyItemDict.F_EnergyItemName) EnergyItemName,
			        SUM(F_Value ) Value,MAX(F_EnergyItemUnit) Unit
			        FROM T_ST_CircuitMeterInfo Circuit 
			        INNER JOIN T_DT_EnergyItemDict ON Circuit.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
			        INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
			        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			        INNER JOIN T_SYS_User_Buildings ON Circuit.F_BuildID = T_SYS_User_Buildings.F_BuildID
			        INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
			        WHERE T_SYS_Users.F_UserName=@UserName
			        AND Circuit.F_MainCircuit=1
			        AND ParamInfo.F_IsEnergyValue = 1
			        AND F_StartDay BETWEEN CONVERT(VARCHAR(7),@EndDate,120)+'-01 00:00' AND DATEADD(DAY,-DAY(@EndDate),DATEADD(MM,1,@EndDate))
			        GROUP BY T_DT_EnergyItemDict.F_EnergyItemCode) AS ThisTemp
		        FULL JOIN
		          (
			        SELECT T_DT_EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(T_DT_EnergyItemDict.F_EnergyItemName) EnergyItemName,
			        SUM(F_Value ) LastValue,MAX(F_EnergyItemUnit) Unit
			        FROM T_ST_CircuitMeterInfo Circuit 
			        INNER JOIN T_DT_EnergyItemDict ON Circuit.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
			        INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
			        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			        INNER JOIN T_SYS_User_Buildings ON Circuit.F_BuildID = T_SYS_User_Buildings.F_BuildID
			        INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
			        WHERE T_SYS_Users.F_UserName=@UserName
			        AND Circuit.F_MainCircuit=1
			        AND ParamInfo.F_IsEnergyValue = 1
			        AND F_StartDay BETWEEN DATEADD( MM,-1,CONVERT(VARCHAR(7),@EndDate,120)+'-01 00:00') AND DATEADD(DAY,-DAY(@EndDate),@EndDate)
			        GROUP BY T_DT_EnergyItemDict.F_EnergyItemCode ) AS LastTemp
		        ON ThisTemp.EnergyItemCode=LastTemp.EnergyItemCode
		        ORDER BY Id ASC ";

        public static string SELECT_YearValue =
           @"SELECT CASE WHEN ThisTemp.EnergyItemCode IS NULL THEN LastTemp.EnergyItemCode ELSE ThisTemp.EnergyItemCode END Id,
                CASE WHEN ThisTemp.EnergyItemName IS NULL THEN LastTemp.EnergyItemName ELSE ThisTemp.EnergyItemName END Name,
		        ThisTemp.Value AS Value,LastTemp.LastValue LastValue,
                CASE WHEN ThisTemp.Unit IS NULL THEN LastTemp.Unit ELSE ThisTemp.Unit END Unit 
                FROM( 
			        SELECT T_DT_EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(T_DT_EnergyItemDict.F_EnergyItemName) EnergyItemName,
			        SUM(F_Value ) Value,MAX(F_EnergyItemUnit) Unit
			        FROM T_ST_CircuitMeterInfo Circuit 
			        INNER JOIN T_DT_EnergyItemDict ON Circuit.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
			        INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
			        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			        INNER JOIN T_SYS_User_Buildings ON Circuit.F_BuildID = T_SYS_User_Buildings.F_BuildID
			        INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
			        WHERE T_SYS_Users.F_UserName=@UserName
			        AND Circuit.F_MainCircuit=1
			        AND ParamInfo.F_IsEnergyValue = 1
			        AND F_StartDay BETWEEN CONVERT(VARCHAR(4),@EndDate,120)+'-01-01 00:00' AND CONVERT(VARCHAR(4),@EndDate,120)+'-12-31 00:00'
			        GROUP BY T_DT_EnergyItemDict.F_EnergyItemCode) AS ThisTemp
		        FULL JOIN
		          (
			        SELECT T_DT_EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(T_DT_EnergyItemDict.F_EnergyItemName) EnergyItemName,
			        SUM(F_Value ) LastValue,MAX(F_EnergyItemUnit) Unit
			        FROM T_ST_CircuitMeterInfo Circuit 
			        INNER JOIN T_DT_EnergyItemDict ON Circuit.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode
			        INNER JOIN T_MC_MeterDayResult DayResult ON Circuit.F_MeterID = DayResult.F_MeterID
			        INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
			        INNER JOIN T_SYS_User_Buildings ON Circuit.F_BuildID = T_SYS_User_Buildings.F_BuildID
			        INNER JOIN T_SYS_Users ON T_SYS_User_Buildings.F_UserName = T_SYS_Users.F_UserName
			        WHERE T_SYS_Users.F_UserName=@UserName
			        AND Circuit.F_MainCircuit=1
			        AND ParamInfo.F_IsEnergyValue = 1
			        AND F_StartDay BETWEEN DATEADD( YY,-1,CONVERT(VARCHAR(4),@EndDate,120)+'-01-01 00:00') AND DATEADD( YY,-1,CONVERT(VARCHAR(4),@EndDate,120)+'-12-31 00:00')
			        GROUP BY T_DT_EnergyItemDict.F_EnergyItemCode ) AS LastTemp
		        ON ThisTemp.EnergyItemCode=LastTemp.EnergyItemCode
		        ORDER BY Id ASC ";







    }
}

