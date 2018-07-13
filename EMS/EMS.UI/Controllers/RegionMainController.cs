using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    public class RegionMainController : ApiController
    {
        RegionMainService service = new RegionMainService();

        public object Get()
        {
            string userName = User.Identity.Name;
            return service.GetViewModelByUserName(userName);
        }

        public object Get(string buildId)
        {
            return service.GetViewModel(buildId);
        }

        public object Get(string buildId, string energyCode)
        {
            return service.GetViewModel(buildId,energyCode);
        }
    }
}
