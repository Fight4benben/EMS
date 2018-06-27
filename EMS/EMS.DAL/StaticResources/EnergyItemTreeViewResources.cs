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
        /// 分项用能树状列表
        /// </summary>
        public static string EnergyItemTreeViewSQL = @"SELECT F_FormulaID AS FormulaID ,F_EnergyItemCode AS EnergyItemCode
                                                            ,F_FormulaName AS EnergyItemName
	                                                    FROM T_ST_CalcFormula
	                                                    WHERE F_BuildID=@BuildID
	                                                    AND F_State=1
	                                                    AND F_EnergyItemCode IS NOT NULL
	                                                    AND F_FormulaName IS NOT NULL
	                                                    ORDER BY F_EnergyItemCode ASC
                                                        ";
    }
}
