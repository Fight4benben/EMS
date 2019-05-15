using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class DeptReportController:ApiController
    {
        DepartmentReportService service = new DepartmentReportService();

        /// <summary>
        /// 部门用能统计报表
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的所有部门的用能天报表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，部门列表，以及用能数据天报表</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModel(userName);
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
                return service.GetViewModelByBuild(userName,buildId);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 部门用能统计报表
        /// 根据建筑ID和日期，获取能源按钮列表，部门列表，以及用能数据天报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回完整的数据：能源按钮列表，部门列表，以及用能数据天报表</returns>
        public object Get(string buildId,string date ,string type)
        {
            try
            {
                return service.GetViewModel(buildId, date, type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string energyCode, string date, string type)
        {
            try
            {
                return service.GetViewModel(buildId, date, energyCode,type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 部门用能统计报表
        /// 根据部门，时间，报表类型，获取指定的用能概况
        /// </summary>
        /// <param name="departmentIDs">部门ID</param>
        /// <param name="date">时间</param>
        /// <param name="type">报表类型：DD:日报
        ///                            MM:月报
        ///                            YY:年报
        /// </param>
        /// <returns>返回：指定部门，时间，报表类型的用能数据</returns>
        public object Get(string buildId,string energyCode,string departmentIDs, string date, string type)
        {
            try
            {
                string[] ids = departmentIDs.Split(',');
                return service.GetViewModel(energyCode, ids, date, type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}