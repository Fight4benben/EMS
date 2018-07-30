using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class ItemStatisticController : ApiController
    {
        EnergyItemStatisticService service = new EnergyItemStatisticService();
        public object Get()
        {
            string userName = User.Identity.Name;

            try
            {
                return service.GetViewModelByUserName(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
