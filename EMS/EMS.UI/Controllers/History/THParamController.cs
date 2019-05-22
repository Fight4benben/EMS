using EMS.DAL.Services;
using EMS.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class THParamController : ApiController
    {
        THParamService service = new THParamService();

        /// <summary>
        /// 获取用户名查询建筑列表，第一栋建筑的84000分类（环境温湿度）包含的支路列表
        /// </summary>
        /// <returns></returns>
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
        /// 根据传入的建筑ID，获取所有的温湿度支路列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 根据传入的建筑ID，获取所有的温湿度支路列表
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public object Get(string buildId,string a,string b,string c,string d)
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
        /// 获取单个支路包含的参数和当天的数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="circuitID"></param>
        /// <returns></returns>
        public object Get(string buildId, string circuitID)
        {
            try
            {
                
                return service.GetViewModel(buildId, circuitID);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// 根据建筑ID，支路ID和日期，获取单个环境温湿度的一天的数据，
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="circuitID"></param>
        /// <param name="startDay">时间格式"yyyy-MM-dd"</param>
        /// <returns></returns>
        public object Get(string buildId, string circuitID, string startDay)
        {
            try
            {
                return service.GetViewModel(buildId,circuitID, startDay);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据建筑ID，支路ID和日期，获取单个环境温湿度的一天的数据，
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="circuitID"></param>
        /// <param name="startDay">时间格式"yyyy-MM-dd"</param>
        /// <param name="step">时间间隔(5,10,15,30,60)</param>
        /// <returns></returns>
        public object Get(string buildId, string circuitID, string startDay,int step)
        {
            try
            {
                return service.GetViewModel(buildId, circuitID, startDay,step);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}