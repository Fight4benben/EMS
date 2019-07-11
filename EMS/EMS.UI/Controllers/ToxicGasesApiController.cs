using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class ToxicGasesApiController : ApiController
    {
        ToxicGasesService service = new ToxicGasesService();


        /// <summary>
        /// 获取 建筑列表，设备列表，第一个设备的所有参数值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetViewModel()
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
        /// 根据建筑 获取设备列表，第一个设备的所有参数值
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetViewModel(string buildID)
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

        /// <summary>
        ///  根据 建筑列表，设备，获取该设备所有参数值
        /// </summary>
        /// <param name="buildID"></param>
        /// <param name="meterID"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetViewModel(string buildID, string meterID)
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModel(userName, buildID, meterID);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取 指定设备，指定日天，获取一天内的所有数据
        /// </summary>
        /// <param name="buildID"></param>
        /// <param name="meterID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetViewModel(string buildID, string meterID,string date)
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetHistoryDataViewModel(userName, buildID, meterID,date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}