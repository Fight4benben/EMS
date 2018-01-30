using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class CircuitOverviewController : ApiController
    {
        CircuitOverviewService service = new CircuitOverviewService();


        public object GetCircuitOverview()
        {
            string userName = User.Identity.Name;

            return service.GetCircuitOverviewViewModel(userName);
        }

        
       
    }
}