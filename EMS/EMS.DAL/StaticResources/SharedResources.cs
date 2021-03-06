﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class SharedResources
    {
        /// <summary>
        /// 根据用户名获取当前用户绑定建筑物
        /// </summary>
        public static string BuildListSQL = @"SELECT DISTINCT(Build.F_BuildID) BuildID,F_BuildName BuildName FROM T_BD_BuildBaseInfo Build 
                                            INNER JOIN T_SYS_User_Buildings UserBuildings  ON Build.F_BuildID = UserBuildings.F_BuildID
                                            WHERE F_UserName = @UserName
                                            ORDER BY Build.F_BuildID ASC";
        /// <summary>
        /// 根据建筑ID获取当前建筑下的分类能耗
        /// </summary>
        public static string EnergyItemDictSQL = @"SELECT EnergyItemDict.F_EnergyItemCode EnergyItemCode,MAX(EnergyItemDict.F_EnergyItemName) EnergyItemName,
                                                MAX(EnergyItemDict.F_EnergyItemUnit) EnergyItemUnit
                                                FROM T_ST_CircuitMeterInfo Circuit
                                                INNER JOIN T_DT_EnergyItemDict EnergyItemDict ON Circuit.F_EnergyItemCode = EnergyItemDict.F_EnergyItemCode
                                                WHERE F_BuildID=@BuildId AND EnergyItemDict.F_EnergyItemCode <>'84000'
                                                GROUP BY EnergyItemDict.F_EnergyItemCode";

        /// <summary>
        /// 根据建筑ID 获取T_BD_BuildExInfo中ExtendFunc字段，Normal表示曲线按照常规方式显示，Special表示按照特殊方式显示（铁狮门方式，带10%预测）
        /// </summary>
        public static string ExtendFunc = @"SELECT F_ExtendFunc ExtendFunc FROM T_BD_BuildExInfo WHERE F_BuildID = @BuildId";
    }
}
