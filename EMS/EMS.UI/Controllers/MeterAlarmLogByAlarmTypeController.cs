using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    public class MeterAlarmLogByAlarmTypeController : ApiController
    {
        MeterAlarmService service = new MeterAlarmService();


        /// <summary>
        /// 根据报警类型,时间, 分页 获取用户报警记录（默认获取当天的报警记录）
        /// </summary>
        /// <param name="buildID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetMeterAlarmLog(string alarmType, string beginDate, string endDate, string pageIndex, string pageSize)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetAlarmLogViewModelByAlarmType(userName, alarmType, beginDate, endDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public object GetMeterAlarmLog(string buildID, string alarmType, string beginDate, string endDate, string pageIndex, string pageSize)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetAlarmLogViewModelByAlarmType(userName, buildID, alarmType, beginDate, endDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}