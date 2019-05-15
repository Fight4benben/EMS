using EMS.DAL.Services;
using EMS.DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class HistoryParamController:ApiController
    {
        HistoryParamService service = new HistoryParamService();

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

        public object Get(string buildId, string energyCode)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
       /// <summary>
       /// 获取单个支路包含的参数
       /// </summary>
       /// <param name="buildId"></param>
       /// <param name="energyCode"></param>
       /// <param name="circuitID"></param>
       /// <returns></returns>
        public object Get(string buildId, string energyCode, string circuitID)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode, circuitID);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// 获取单个各个参数的数据
        /// </summary>
        /// <param name="circuitID"></param>
        /// <param name="meterParamIDs"></param>
        /// <param name="startTime"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public object Get(string circuitID, string meterParamIDs, string startTime, int step)
        {
            try
            {
                string[] paramIDs= meterParamIDs.Split(',');
                //DateTime start = Util.ConvertString2DateTime(startTime, "yyyy-MM-dd HH:mm:ss");
                return service.GetViewModel(circuitID, paramIDs, startTime, step);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}