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
        public static string DepartmentTreeViewByBuildIDSQL = @"SELECT F_DepartmentID AS ID, F_DepartParentID AS ParentID
                                                                       ,F_DepartmentName AS Name
                                                                    FROM T_ST_DepartmentInfo AS DepartmentInfo
                                                                    WHERE F_BuildID=@BuildID
                                                                    ORDER BY F_DepartmentID
                                                                    ";
    }
}
