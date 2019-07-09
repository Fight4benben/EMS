using EMS.DAL.Entities.Setting;
using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        public object SetAlarmInfo([FromBody] JObject obj)
        {
            try
            {
                MeterAlarmSet setInfo = new MeterAlarmSet();

                setInfo.BuildID = obj["BuildID"].ToString();
                setInfo.MeterID = obj["MeterID"].ToString();
                setInfo.ParamID = obj["ParamID"].ToString();
                setInfo.ParamCode = obj["ParamCode"].ToString();
                setInfo.State = Convert.ToInt32( obj["State"].ToString());
                setInfo.Level = Convert.ToInt32( obj["Level"].ToString());
                setInfo.Delay = Convert.ToInt32(obj["Delay"].ToString());
                setInfo.Lowest = Convert.ToDecimal(obj["Lowest"].ToString());
                setInfo.Low = Convert.ToDecimal(obj["Low"].ToString());
                setInfo.High = Convert.ToDecimal(obj["High"].ToString());
                setInfo.Highest = Convert.ToDecimal(obj["Highest"].ToString());

                return service.SetAlarmInfo(setInfo);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpDelete]
        public object DeleteAlarmInfo([FromBody] JObject obj)
        {
            try
            {
                MeterAlarmSet setInfo = new MeterAlarmSet();

                setInfo.BuildID = obj["BuildID"].ToString();
                setInfo.MeterID = obj["MeterID"].ToString();
                setInfo.ParamID = obj["ParamID"].ToString();

                return service.DeleteParam(setInfo);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}