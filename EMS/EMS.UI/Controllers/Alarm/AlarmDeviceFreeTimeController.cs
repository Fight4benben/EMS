using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers.Alarm
{
    [Authorize]
    public class AlarmDeviceFreeTimeController : ApiController
    {
        AlarmDeviceFreeTimeService service = new AlarmDeviceFreeTimeService();
        /// <summary>
        /// 获取设备用能越限告警（设定时间段内用能超过设置起始时间的前一个小时的值）
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有设备
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，能源按钮列表</returns>
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
        /// 获取设备用能越限告警（设定时间段内用能超过设置起始时间的前一个小时的值）
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns></returns>
        public object Get(string buildId, string date)
        {
            try
            {
                return service.GetViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}