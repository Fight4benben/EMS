using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class CircuitOverviewController : ApiController
    {
        CircuitOverviewService service = new CircuitOverviewService();


        /// <summary>
        /// 当日支路用能概况
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的回路当日的用能概况
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表，以及第一支路数据</returns>
        public object Get()
        {
            string userName = User.Identity.Name;
            return service.GetCircuitOverviewViewModel(userName);
        }

        /// <summary>
        /// 支路用能概况：
        /// 根据用户传入的建筑ID，查找该建筑包含的分类能耗，所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表，以及第一支路数据</returns>
        public object Get(string buildId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                return service.GetCircuitOverviewViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据用户传入的建筑ID和分类能耗代码，
        /// 获取该建筑的分类能耗包含的所有支路以及第一支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗代码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表，以及第一支路数据</returns>
        public object Get(string buildId, string energyCode)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                return service.GetCircuitOverviewViewModel(buildId, energyCode, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }

        /// <summary>
        /// 根据用户传入的建筑ID，分类能耗代码，支路编码
        /// 获取传入支路的用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗代码</param>
        /// <param name="circuitId">支路编码</param>
        /// <param name="date">传入的日期("yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表，以及传入支路编码的数据</returns>
        public object Get(string buildId, string energyCode, string circuitId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                return service.GetCircuitOverviewViewModel(buildId, energyCode, circuitId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}