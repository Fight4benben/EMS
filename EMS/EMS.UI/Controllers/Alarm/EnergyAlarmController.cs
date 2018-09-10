﻿using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class EnergyAlarmController : ApiController
    {
        EnergyAlarmService service = new EnergyAlarmService();

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
                return service.GetViewModelByUserName(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 设备用能越限告警数据
        /// 根据建筑ID和日期，获取指定建筑的用能越限告警数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns>用能数据：</returns>
        public object Get(string buildId, string date,string type)
        {
            try
            {
                switch (type)
                {
                    case "Limit":
                        return service.GetViewModel(buildId, date);
                    case "Day":
                        return service.GetMomDayViewModel(buildId, date);
                    case "Month":
                        return service.GetCompareMonthViewModel(buildId, date);
                    case "Dept":
                        return service.GetCompareDeptMonthViewModel(buildId, date);
                    default:
                        return "null";
                }
                
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        
    }
}