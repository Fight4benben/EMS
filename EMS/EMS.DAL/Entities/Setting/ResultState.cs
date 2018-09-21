using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    /// <summary>
    /// 操作结果 成功：State=0  
    ///        失败：State=1
    ///        详细描述：Details
    /// </summary>
    public class ResultState
    {
        public int State { get; set; }
        public string Details { get; set; }
    }
}
