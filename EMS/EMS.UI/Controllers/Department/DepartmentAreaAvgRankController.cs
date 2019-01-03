using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers.Department
{
    [Authorize]
    public class DepartmentAreaAvgRankController : ApiController
    {
        DepartmentAreaAvgRankService service = new DepartmentAreaAvgRankService();

        /// <summary>
        /// 部门 月份 单位面积能耗
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有部门
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表，能源按钮列表，部门单位面积能耗</returns>
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

        public object Get(string buildId, string date)
        {
            try
            {
                return service.GetViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

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