using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class OverAllSearchController : ApiController
    {
        OverAllSearchService service = new OverAllSearchService();
        /// <summary>
        /// 根据用户名获取建筑列表
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModelByUserName(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取搜索结果
        /// </summary>
        /// <param name="type">支路："Circuit"；部门："Dept";区域："Region"</param>
        /// <param name="keyWord">搜索内容</param>
        /// <param name="endDay">结束时间（"yyyy-MM-dd"）</param>
        public object Get(string timeType, string type, string keyWord, string buildID, string energyCode, string endDay)
        {
            try
            {
                return service.GetViewModel(timeType, type, keyWord, buildID, energyCode, endDay);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}