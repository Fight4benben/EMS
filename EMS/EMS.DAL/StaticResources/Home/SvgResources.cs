using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources.Home
{
    public class SvgResources
    {
        public static string SvgListSQL = @"SELECT 
                                            F_SvgID SvgID,
                                            F_SvgName SvgName 
                                            FROM T_ST_Svg
                                            WHERE F_BuildID=@BuildId";

        public static string SvgPathSQL = @"SELECT 
                                            F_Path 'Path'
                                            FROM T_ST_Svg
                                            WHERE F_SvgID = @SvgID";

        //获取SVGBingding信息
        public static string SvgBindingSQL = @"SELECT F_SvgID SvgID,F_Meters Meters, F_Params ParamStrings FROM T_ST_SvgBinding
                                            WHERE F_SvgId = @SvgId";
    }
}
