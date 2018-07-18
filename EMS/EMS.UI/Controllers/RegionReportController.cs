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

        public object Get(string energyCode, string RegionIDs, string date, string type)
        {
            try
            {
                string[] ids = RegionIDs.Split(',');
                return service.GetViewModel(energyCode, ids, date, type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}