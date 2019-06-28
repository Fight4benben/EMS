using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class NoWorkDayController : ApiController
    {
        NoWorkDayService service = new NoWorkDayService();

        /// <summary>
        /// 非工作日用能报表
        /// 根据用户名，获取建筑列表，第一个建筑能耗分类，第一个建筑的第一个分类的支路列表以及支路的当月报表
        /// </summary>
        /// <returns></returns>
        public object GetReport()
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

        /// <summary>
        /// 非工作日用能报表
        /// 根据建筑ID ，获取指定建筑的分类能耗，第一个分类的支路列表以及支路的当月报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <returns></returns>
        public object GetReport(string buildId)
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModel(userName, buildId);
            }
            catch (Exception e)
            {
                return e.Message;
            }
           
        }

        /// <summary>
        /// 非工作日用能报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="code">分类能耗代码</param>
        /// <returns></returns>
        public object GetReport(string buildId,string code)
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModel(userName, buildId,code);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        /// <summary>
        /// 非工作日用能报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗代码</param>
        /// <param name="beginDate">起始日期（格式：yyyy-MM-dd）</param>
        /// <param name="endDate">截止日期（格式：yyyy-MM-dd）</param>
        /// <returns></returns>
        public object GetReport(string buildId, string energyCode,string beginDate, string endDate)
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModel(userName, buildId, energyCode, beginDate, endDate);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        /// <summary>
        /// 非工作日用能报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗代码</param>
        /// <param name="cicruitIDs">支路代码，多个支路之间使用英文逗号分隔</param>
        /// <param name="beginDate">起始日期（格式：yyyy-MM-dd）</param>
        /// <param name="endDate">截止日期（格式：yyyy-MM-dd）</param>
        /// <returns></returns>
        public object GetReport(string buildId, string energyCode,string cicruitIDs, string beginDate, string endDate)
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModel(userName, buildId, energyCode, cicruitIDs, beginDate, endDate);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}