using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class SystemLogApiController : ApiController
    {
        public SystemLogService service = new SystemLogService();

        /// <summary>
        /// 默认获取当天的操作日志
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            try
            {
                string startDay = DateTime.Now.ToString("yyyy-MM-dd");
                string endDay = DateTime.Now.ToString("yyyy-MM-dd");

                return service.GetViewModel(startDay, endDay);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="type"> DD:当日
        ///                     WW:本周
        ///                     MM：本月
        ///                     YY：本年
        ///                     LDD:昨天
        ///                     LWW:上周
        ///                     LMM：本月
        ///                     LYY：本年
        /// </param>
        /// <returns></returns>
        public object Get(string type)
        {
            try
            {
                return service.GetViewModel(type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="startDay">时间格式："yyyy-MM-dd"</param>
        /// <param name="endDay">时间格式："yyyy-MM-dd"</param>
        /// <returns></returns>
        public object Get(string startDay, string endDay)
        {
            try
            {
                return service.GetViewModel(startDay, endDay);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}