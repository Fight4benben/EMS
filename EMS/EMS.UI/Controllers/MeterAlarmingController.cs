using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class MeterAlarmingController : ApiController
    {
        MeterAlarmService service = new MeterAlarmService();


        /// <summary>
        /// 获取登录用户关联的所有报警，（默认第一页，每页100条）
        /// </summary>
        /// <param name="type"> 
        /// A:是否有报警
        /// B:报警信息</param>
        /// <returns></returns>
        [HttpGet]

        public object GetMeterAlarming(string type = "A")
        {
            try
            {
                string userName = User.Identity.Name;
                switch (type.ToUpper())
                {
                    case "A":
                        return service.GetIsAlarming(userName);
                    case "B":
                        return service.GetAlarmingViewModel(userName);
                }

                return service.GetIsAlarming(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据用户名，分页 获取登录用户关联的报警
        /// </summary>
        /// <param name="pageIndex">页码（第几页）</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <returns></returns>
        [HttpGet]
        public object GetMeterAlarming(string pageIndex, string pageSize)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetAlarmingViewModel(userName, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}