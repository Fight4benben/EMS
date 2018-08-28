using EMS.DAL.RepositoryImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class OverAllSearchService
    {
        private OverAllSearchDbContext context;

        public OverAllSearchService()
        {
            context = new OverAllSearchDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">之路："Circuit"；部门："Dept";区域："Region"</param>
        /// <param name="keyWord">搜索内容</param>
        public void GetInputInfo(string type, string keyWord)
        {
            switch (type)
            {
                case "Circuit":

                    break;

                case "Dept":

                    break;

                case "Region":

                    break;

                default:
                    break;
            }
        }
    }
}
