using EMS.DAL.Services;
using System;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class AlarmDepartmentOverLimitController : ApiController
    {
        AlarmDepartmentOverLimitService service = new AlarmDepartmentOverLimitService();

        /// <summary>
        /// 获取部门用能告警-（每天设定时间段内用能超过设定阈值）
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有用能越限的部门
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，能源按钮列表，所有用能越限的部门</returns>
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
        /// 部门用能越限告警（每天设定时间段内用能超过设定阈值）
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="energyCode">分类代码</param>
        /// <param name="date">时间（"yyyy-MM-dd"）</param>
        /// <returns>所有用能越限的部门</returns>
        public object Get(string buildId, string energyCode, string date)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}