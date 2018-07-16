using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class DeptOverviewController : ApiController
    {
        DepartmentOverviewService service = new DepartmentOverviewService();

        /// <summary>
        /// 部门用能概况
        /// 初始加载：获取用户名查询建筑列表，部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势</returns>
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
        /// 部门用能概况
        /// 根据建筑ID和时间，获取该建筑包含部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回：部门天用能同比，部门计划/实际用能对比，最近31天用能拼图及趋势</returns>
        public object Get(string buildId)
        {
            try
            {
                return service.GetViewModel(buildId);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId,string energyCode)
        {
            try
            {
                return service.GetViewModel(buildId,energyCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}