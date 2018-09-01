using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class EnergyItemTreeViewResources
    {
        /// <summary>
        /// 根据建筑ID 分项用能树状列表
        /// </summary>
        public static string EnergyItemTreeViewSQL = @"SELECT T_ST_CalcFormula.F_FormulaID AS FormulaID
                                                        ,T_DT_EnergyItemDict.F_EnergyItemName AS EnergyItemName
                                                        ,T_DT_EnergyItemDict.F_EnergyItemCode AS EnergyItemCode
                                                        ,T_DT_EnergyItemDict.F_ParentItemCode AS ParentItemCode
                                                        FROM T_ST_CalcFormula 
                                                        INNER JOIN T_BD_BuildBaseInfo ON T_ST_CalcFormula.F_BuildID = T_BD_BuildBaseInfo.F_BuildID 
                                                        INNER JOIN T_DT_EnergyItemDict ON T_ST_CalcFormula.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode 
                                                        WHERE T_ST_CalcFormula.F_BuildID=@BuildID 
                                                        AND T_ST_CalcFormula.F_State =1
                                                        ORDER BY T_DT_EnergyItemDict.F_EnergyItemCode
                                                        ";

        /// <summary>
        /// 根据建筑ID和分类能耗 获取分项用能树状列表
        /// </summary>
        public static string EnergyItemTreeViewByEnergyCodeSQL = @"SELECT T_ST_CalcFormula.F_FormulaID AS FormulaID
                                                                    ,T_DT_EnergyItemDict.F_EnergyItemName AS EnergyItemName
                                                                    ,T_DT_EnergyItemDict.F_EnergyItemCode AS EnergyItemCode
                                                                    ,T_DT_EnergyItemDict.F_ParentItemCode AS ParentItemCode
                                                                    FROM T_ST_CalcFormula 
                                                                    INNER JOIN T_BD_BuildBaseInfo ON T_ST_CalcFormula.F_BuildID = T_BD_BuildBaseInfo.F_BuildID 
                                                                    INNER JOIN T_DT_EnergyItemDict ON T_ST_CalcFormula.F_EnergyItemCode = T_DT_EnergyItemDict.F_EnergyItemCode 
                                                                    WHERE T_ST_CalcFormula.F_BuildID=@BuildID 
                                                                    AND T_DT_EnergyItemDict.F_EnergyItemCode LIKE (LEFT(@EnergyItemCode,2)+'%')
                                                                    AND T_ST_CalcFormula.F_State =1
                                                                    ORDER BY T_DT_EnergyItemDict.F_EnergyItemCode
                                                                    ";
    }
}
