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

        public object GetReport()
        {
            string userName = User.Identity.Name;

            return service.GetViewModel(userName);
        }

        public object GetReport(string buildId)
        {
            string userName = User.Identity.Name;

            return service.GetViewModel(userName, buildId);
        }

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