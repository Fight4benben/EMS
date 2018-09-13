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
    public class AlarmDeviceController : ApiController
    {
        AlarmDeviceService service = new AlarmDeviceService();

        /// <summary>
        /// 设备用能越限告警数据
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一栋建筑对应的报警等级，第一栋建筑对应的报警设备
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，第一栋建筑对应的分类，第一栋建筑对应的报警等级，第一栋建筑对应的报警设备</returns>
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
        /// 获取设备告警
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类代码</param>
        /// <param name="type">数据类型: "DD"：日环比； "MM"：月同比； "SS":季度 </param>
        /// <param name="date">结束时间 "日环比"：yyyy-MM-dd; "月同比"：yyyy-MM; "季度":每个季度最后一个月 </param>
        /// <returns></returns>
        public object Get(string buildId, string energyCode, string type, string date)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode, type, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost]
        public object Set([FromBody] JObject obj)
        {
            try
            {
                decimal level1 = Decimal.Parse(obj["level1"].ToString());
                level1 = level1 > 0 ? level1 : 0.01m;

                decimal level2 = Decimal.Parse(obj["level2"].ToString());
                level2 = level2 > 0 ? level2 : 0.02m;

                return service.SetDeviceBuildAlarmLevel(obj["buildId"].ToString(), obj["energyCode"].ToString(), level1, level2);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}