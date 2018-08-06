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

        public object Get(string buildId, string energyCode, string circuitIDs)
        {
            try
            {
                string[] ids = circuitIDs.Split(',');
                return service.GetViewModel(buildId, energyCode, ids);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string circuitIDs, string meterParamIDs, string startTime, int step)
        {
            try
            {
                string[] ids = circuitIDs.Split(',');
                string[] paramIDs= meterParamIDs.Split(',');
                DateTime start = Util.ConvertString2DateTime(startTime, "yyyy-MM-dd HH:mm:ss");
                return service.GetViewModel(ids,paramIDs, start,step);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}