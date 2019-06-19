using EMS.DAL.Entities;
using EMS.DAL.Services.Circuit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers.Circuit
{
    [Authorize]
    public class MultiRateController : ApiController
    {
        MultiRateService service = new MultiRateService();

        /// <summary>
        /// 根据用户名，获取建筑列表，第一个建筑能耗分类，第一个建筑的第一个分类的支路列表以及支路的月报表
        /// </summary>
        /// <returns></returns>
        public object GetReport()
        {
            string userName = User.Identity.Name;

            return service.GetViewModel(userName);
        }

        /// <summary>
        /// 根据建筑ID ，获取指定建筑的分类能耗，第一个分类的支路列表以及支路的月报表
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public object GetReport(string buildId)
        {
            string userName = User.Identity.Name;

            return service.GetViewModel(userName, buildId);
        }

        /// <summary>
        /// 获取复费率报表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="type">报表类型（DD-日报；MM-月报；YY-年报）</param>
        /// <param name="date">日期（yyyy-MM-dd）</param>
        /// <returns>返回：复费率报表</returns>
        public object GetReport(string buildId, string type, string date)
        {
            return service.GetViewModel(buildId, type, date);
        }
        

        [HttpPost]
        public object Report([FromBody] JObject obj)
        {
            string buildId = obj["buildId"].ToString();
            string type = obj["type"].ToString();
            string date = obj["date"].ToString();
            string circuits = obj["circuits"].ToString();

            return service.GetViewModel(buildId, type, date, circuits);
        }

    }
}