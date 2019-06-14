using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources.Setting
{
    public class SvgSettingResources
    {
        public static string SvgListSQL = @"SELECT F_BuildID BuildId,F_SvgID SvgId,F_SvgName SvgName,F_Path 'Path'
                                            FROM T_ST_Svg
                                            WHERE F_BuildID = @BuildId
                                            Order by F_SvgID ASC";

        public static string CircuitsSQL = @"SELECT Circuit.F_MeterID Id,Circuit.F_CircuitName Name FROM T_ST_CircuitMeterInfo Circuit
                                            INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                            WHERE Circuit.F_BuildID=@BuildId";

        public static string ParamsSQL = @"SELECT ParamInfo.F_MeterParaCode Id,ParamInfo.F_MeterParamName Name FROM T_ST_CircuitMeterInfo Circuit
                                            INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
                                            INNER JOIN T_ST_MeterParamInfo ParamInfo ON Meter.F_MeterProdID = ParamInfo.F_MeterProdID
                                            WHERE Circuit.F_BuildID = @BuildId
                                            GROUP BY ParamInfo.F_MeterParaCode,ParamInfo.F_MeterParamName";

        public static string UpdateSvgBindingSQL = @"Update T_ST_SvgBinding SET F_Meters=@Meters,F_Params=@Params WHERE F_ID=@Id";
    }
}
