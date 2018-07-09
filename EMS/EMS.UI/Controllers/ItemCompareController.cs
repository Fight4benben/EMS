using EMS.DAL.Entities;
using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class ItemCompareController:ApiController
    {
        EnergyItemCompareService service = new EnergyItemCompareService();

        /// <summary>
        /// 分项用能同比：
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的第一个回路当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，分项列表，以及第一分类数据</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetEnergyItemCompareViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 分项用能同比：
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <returns>返回：能源按钮列表，分项列表，以及第一分类数据</returns>
        public object Get(string buildId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                return service.GetEnergyItemCompareViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 分项用能同比：
        /// 根据用户传入的建筑ID，分项ID，时间，查找该建筑分项用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分项ID</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回：该分项用能同比数据</returns>
        public object Get(string buildId, string energyCode, string date)
        {
            try
            {
                return service.GetEnergyItemCompareViewModel(buildId, energyCode, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}