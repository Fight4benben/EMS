using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class TreeViewResources
    {
        /// <summary>
        /// 根据建筑ID和分类能耗 获取分项用能树状列表
        /// </summary>
        public static string DepartmentTreeViewByBuildID_SQL = 
            @"SELECT DepartmentInfo.F_DepartmentID AS ID, MAX(F_DepartParentID) AS ParentID
                ,MAX(F_DepartmentName) AS Name
                FROM T_ST_DepartmentInfo AS DepartmentInfo
                INNER JOIN T_ST_DepartmentMeter DepartmentMeter ON DepartmentInfo.F_DepartmentID = DepartmentMeter.F_DepartmentID
                INNER JOIN T_ST_CircuitMeterInfo Circuit ON DepartmentMeter.F_MeterID = Circuit.F_MeterID
                INNER JOIN T_DT_EnergyItemDict EnergyItem ON Circuit.F_EnergyItemCode = EnergyItem.F_EnergyItemCode
                WHERE DepartmentInfo.F_BuildID=@BuildID
                AND EnergyItem.F_EnergyItemCode =  @EnergyItemCode
                GROUP BY DepartmentInfo.F_DepartmentID ";

        /// <summary>
        /// 根据建筑ID和分类能耗 获取支路树状列表
        /// </summary>
        public static string CircuitTreeViewByBuildIDEnergyCode_SQL =
            @"SELECT F_CircuitID Id,F_CircuitName Name,F_ParentID ParentId 
                FROM T_ST_CircuitMeterInfo Circuit
                WHERE F_BuildID=@BuildId
                AND F_EnergyItemCode=@EnergyItemCode ";
    }
}
