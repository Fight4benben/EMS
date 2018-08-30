using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class ItemOverviewController : ApiController
    {
        EnergyItemOverviewService service = new EnergyItemOverviewService();

        /// <summary>
        /// 当日分项用能概况
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，当日分项用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表吗，完整的分项用能数据</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetEnergyItemOverviewViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 分项用能概况：
        /// 根据用户传入的建筑ID，查找该建筑包含的分类能耗，所有完整的分项用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：完整的分项用能数据</returns>
        public object Get(string buildId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                return service.GetEnergyItemOverviewViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

       
    }
}