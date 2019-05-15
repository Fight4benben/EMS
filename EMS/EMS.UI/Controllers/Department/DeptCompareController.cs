using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class DeptCompareController : ApiController
    {
        DepartmentCompareService service = new DepartmentCompareService();

        /// <summary>
        /// 部门用能同比分析
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，第一个分类对应的第一个部门用能数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，部门列表，以及第一个部门用能数据</returns>
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
        /// 部门用能同比分析
        /// 根据建筑ID和时间，获取该建筑对应的能源按钮列表，第一个部门用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回：能源按钮列表，部门列表，以及第一个部门用能数据</returns>
        //public object Get(string buildId)
        //{
        //    try
        //    {
        //        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
        //        return service.GetViewModel(buildId, date);
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}

        public object Get(string buildId,string date)
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

        public object Get(string buildId,string energyCode,string date)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode,date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 部门用能同比分析
        /// 根据建筑ID，部门ID和时间，获取该部门用能数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <param name="date">时间</param>
        /// <returns>返回：部门用能数据</returns>
        public object Get(string buildId, string energyCode,string departmentID, string date)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode, departmentID, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}