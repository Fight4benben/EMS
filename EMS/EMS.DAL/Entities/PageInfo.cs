using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class PageInfo
    {
        //总报警数量
        public int TotalNumber { get; set; }
        //每页显示行数
        public int PageSize { get; set; }
        //当前页码
        public int CurrentPage { get; set; }
        //总页数
        public int TotalPage { get; set; }
    }
}
