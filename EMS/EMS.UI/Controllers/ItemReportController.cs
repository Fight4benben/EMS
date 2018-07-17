using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class ItemReportController:ApiController
    {
        EnergyItemReportService service = new EnergyItemReportService();

        /// <summary>
        /// 分项用能报表：
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的第一个分项日报表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，分项列表，以及日报表</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetEnergyItemReportViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 分项用能报表：
        /// 根据用户传入的建筑ID，查找该建筑包含的分类能耗，所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：能源按钮列表，分项列表，以及日报表</returns>
        public object Get(string buildId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                return service.GetEnergyItemReportViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string type, string date)
        {
            try
            {
                return service.GetEnergyItemReportViewModel(buildId, type, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string energyCode, string type, string date)
        {
            return null;
        }

        public object Get(string buildId,string energyCode,string type, string formulaIds, string date)
        {
            string[] formulas = formulaIds.Trim().Split(',');

            try
            {
                return service.GetEnergyItemReportViewModel(formulas, date, type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}