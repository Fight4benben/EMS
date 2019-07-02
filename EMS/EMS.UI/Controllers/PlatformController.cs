using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class PlatformController : ApiController
    {
        PlatformService service = new PlatformService();

        [HttpGet]
        public object GetReport()
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
    }
}