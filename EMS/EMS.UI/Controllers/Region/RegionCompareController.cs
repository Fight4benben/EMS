using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class RegionCompareController : ApiController
    {
        RegionCompareService service = new RegionCompareService();
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

        public object Get(string buildId,string date)
        {
            try
            {
                return service.GetViewModel(buildId, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string energyCode,string date)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode,date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId,string energyCode, string regionID, string date)
        {
            try
            {
                return service.GetViewModel(buildId,energyCode, regionID, date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}