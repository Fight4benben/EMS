using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class DepartmentCheckController : ApiController
    {
        AlarmDepartmentCompletionRateService service = new AlarmDepartmentCompletionRateService();
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

        public object Get(string buildId, string energyCode, string type, string date)
        {
            try
            {
                return service.GetViewModel(buildId,energyCode,type,date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        [HttpPost]
        public object Set([FromBody] JObject obj)
        {
            try
            {
                decimal completionRate = Decimal.Parse(obj["completionRate"].ToString());
                completionRate = completionRate > 0 ? completionRate : 0.01m;
                return service.SetDeptCompletionRate(obj["buildId"].ToString(), obj["energyCode"].ToString(), completionRate);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
