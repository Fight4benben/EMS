﻿using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    public class CircuitCompareController : ApiController
    {
        CircuitCompareService service = new CircuitCompareService();
        
        /// <summary>
        /// 支路同比分析
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的回路当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表，以及第一支路数据</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetCircuitCompareViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId)
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetCircuitCompareVMWithBuild(userName,buildId);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据用户传入的建筑ID和时间，查找该建筑包含的分类能耗，所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回数据：能源按钮列表，回路列表，以及第一支路数据</returns>
        public object Get(string buildId, string date)
        {
            try
            {
                return service.GetCircuitCompareViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据用户传入的建筑ID，分类能耗编码和时间，查找所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回数据：回路列表，以及第一支路数据</returns>
        public object Get(string buildId, string energyCode, string date)
        {
            try
            {
                return service.GetCircuitCompareViewModel(buildId, energyCode, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>
        /// 根据用户传入的建筑ID，分类能耗编码，支路编码和时间，查找该支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗编码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回数据：支路用能数据</returns>
        public object Get(string buildId, string energyCode, string circuitId, string date)
        {
            try
            {
                return service.GetCircuitCompareViewModel(buildId, energyCode, circuitId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}