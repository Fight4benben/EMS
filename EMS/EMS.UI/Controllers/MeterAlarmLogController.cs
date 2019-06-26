using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class MeterAlarmLogController : ApiController
    {
        MeterAlarmService service = new MeterAlarmService();
        
        /// <summary>
        /// 获取用户报警记录（默认获取当天的报警记录）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetMeterAlarmLog()
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetAlarmLogViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据建筑id，获取报警记录（默认获取当天的报警记录）
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetMeterAlarmLog(string buildID)
        {
            try
            {
                string userName = User.Identity.Name;
                string beginDate = DateTime.Now.ToString("yyyy-MM-dd 00:00");
                string endDate = DateTime.Now.ToString("yyyy-MM-dd 23:59");

                return service.GetAlarmLogViewModel(userName, buildID, beginDate, endDate);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>
        /// 根据建筑ID,时间, 分页 获取用户报警记录（默认获取当天的报警记录）
        /// </summary>
        /// <param name="buildID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetMeterAlarmLog(string buildID, string beginDate, string endDate, string pageIndex, string pageSize)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetAlarmLogViewModel(userName, buildID, beginDate, endDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
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

                return service.GetAlarmLogViewModel(userName, buildID, alarmType, beginDate, endDate, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}