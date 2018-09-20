using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.UI.Controllers.Setting
{
    public class UserBuildingApiController : ApiController
    {
        public UserBuildingService service = new UserBuildingService();


        public object Get()
        {
            try
            {
                return service.GetViewModel();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string userName)
        {
            try
            {
                return service.GetViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost]
        public int AddBuild([FromBody] JObject obj)
        {
            try
            {
                string userName = obj["userName"].ToString();
                string buildId = obj["buildId"].ToString();
                return service.AddBuild(userName,buildId);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }

        [HttpDelete]
        public int DeleteBuild([FromBody] JObject obj)
        {
            try
            {
                string userName = obj["userName"].ToString();
                string buildId = obj["buildId"].ToString();
                return service.DeleteBuild(userName, buildId);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }

    }
}
