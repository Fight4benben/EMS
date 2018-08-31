using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers.Alarm
{
    [Authorize]
    public class AlarmCompareDeptController : ApiController
    {
        EnergyAlarmService service = new EnergyAlarmService();

        /// <summary>
        /// 获取部门用能 月份同比大于20%
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public object Get(string buildId, string date)
        {
            try
            {
                return service.GetCompareDeptMonthViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}