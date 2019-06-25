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
    public class MeterAlarmConfirmController : ApiController
    {
        MeterAlarmService service = new MeterAlarmService();

        /// <summary>
        /// 确认所有报警
        /// </summary>
        /// <param name="describe">备注</param>
        /// <returns></returns>
        [HttpGet]
        public object SetConfirmAlarm(string describe = "")
        {
            try
            {
                string userName = User.Identity.Name;

                return service.SetConfirmAllMeterAlarm(userName, describe);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 确认选择的报警
        /// </summary>
        /// <param name="obj">
        /// ids：报警ID；
        /// describe：备注
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public object SetConfirmAlarm([FromBody] JObject obj)
        {
            try
            {
                string userName = User.Identity.Name;

                string ids = obj["ids"].ToString();
                string describe = obj["describe"].ToString();

                return service.SetConfirmMeterAlarm(userName, describe, ids);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}