using EMS.DAL.Entities;
using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    public class SettingAlarmDeviceOverLimitController : ApiController
    {
        AlarmDeviceOverLimitService service = new AlarmDeviceOverLimitService();

        /// <summary>
        /// 设备用能越限告警数据
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有设备
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，能源按钮列表</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetSettingViewModelByUserName(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据建筑 获取已设置告警列表和未设置的设备列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public object Get(string buildId)
        {
            try
            {
                return service.GetAlarmLimitValueViewModel(buildId);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>
        /// 设置设备用能越限值（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode"></param>
        /// <param name="departmentID"></param>
        /// <param name="startTime"> 起始时间：17:00</param>
        /// <param name="endTime">结束时间：08:00</param>
        /// <param name="isOverDay">是否跨越天 1：跨天 ；其他不夸天</param>
        /// <param name="limitValue"> 报警阈值</param>
        /// <returns></returns>
        [HttpPost]
        public object Set([FromBody] JObject obj)
        {
            try
            {
                string buildId = obj["buildId"].ToString()
                     , circuitID = obj["circuitID"].ToString()
                     , startTime = obj["startTime"].ToString()
                     , endTime = obj["endTime"].ToString();

                int isOverDay = int.Parse(obj["isOverDay"].ToString());
                decimal limitValue = Decimal.Parse(obj["limitValue"].ToString());

                return service.SetDeviceOverLimitValue(buildId, circuitID, startTime, endTime, isOverDay, limitValue);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 删除设备用能越限值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpDelete]
        public object Delete([FromBody] JObject obj )
        {
            try
            {
                string buildId = obj["buildId"].ToString();
                string circuitID = obj["circuitID"].ToString();
                return service.DeleteDeviceOverLimitValue(buildId, circuitID);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}