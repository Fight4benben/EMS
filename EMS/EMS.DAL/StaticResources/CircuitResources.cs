using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.StaticResources
{
    public class CircuitResources
    {
        /// <summary>
        /// 查询回路信息
        /// </summary>
        public static string CircuitSQL = @"SELECT F_CircuitID CircuitId,F_CircuitName CircuitName,F_MeterID MeterId,F_ParentID ParentId 
                                            FROM T_ST_CircuitMeterInfo Circuit
                                            WHERE F_BuildID=@BuildId
                                            AND F_EnergyItemCode=@EnergyItemCode";
    }
}
