using EMS.DAL.Entities.Setting;
using EMS.DAL.Services.Setting;
using EMS.DAL.ViewModels.Setting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers.Setting
{
    [Authorize]
    public class SvgSettingController : ApiController
    {
        SvgSettingService service = new SvgSettingService();
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetByName(userName);
            }
            catch (Exception e)
            {
                return new { Error = e.Message };
            }
        }

        public object Get(string buildId)
        {

            try
            {
                return service.GetByBuildId(buildId);
            }
            catch (Exception e)
            {
                return new { Error = e.Message };
            }
        }

        public object Get(string buildId, string svgId)
        {
            return service.GetBindingViewModel(buildId, svgId);
        }

        [HttpPost]
        public object Post([FromBody] JObject obj)
        {

            string buildId = obj["buildid"].ToString().ToUpper();
            string svgName = obj["svgname"].ToString();

            return service.Insert(buildId, svgName);
        }

        [HttpPut]
        public object Put([FromBody] JObject obj)
        {
            string svgId = obj["svgid"].ToString().ToUpper();
            string svgName = obj["svgname"].ToString();

            return service.Update(svgId, svgName);
        }

        [HttpDelete]
        public object Delete([FromBody]JObject obj)
        {
            string svgId = obj["svgid"].ToString();
            return service.Delete(svgId);
        }


    }

    [Authorize]
    public class SvgBindingController:ApiController
    {
        SvgSettingService service = new SvgSettingService();
        [HttpPost]
        public object Post([FromBody]SvgBindingPost obj)
        {
            string svgId = obj.SvgId.ToUpper();
            string postMeters = obj.PMeters.ToUpper();
            string postParams = obj.PParams;
            
            return service.UpdateSvgBinding(svgId,postMeters,postParams);
        }
    }
}
