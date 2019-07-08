using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class MeterAlarmSetApiController : ApiController
    {
        MeterAlarmSetService service = new MeterAlarmSetService();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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

        [HttpGet]
        public object Get(string buildID)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetViewModel(userName, buildID);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        [HttpGet]
        public object Get(string buildID, string code)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetViewModel(userName, buildID, code);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public object Get(string buildID, string code,string circuitID)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetViewModel(userName, buildID, code, circuitID);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}