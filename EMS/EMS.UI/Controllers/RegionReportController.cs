using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class RegionReportController : ApiController
    {
        RegionReportService service = new RegionReportService();

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

        public object Get(string buildId,string date,string type)
        {
            try
            {
                return service.GetViewModel(buildId, date, type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string energyCode,string date,string type)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode,date,type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId,string energyCode, string regionIDs, string date, string type)
        {
            try
            {
                string[] ids = regionIDs.Split(',');
                return service.GetViewModel(energyCode, ids, date, type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}