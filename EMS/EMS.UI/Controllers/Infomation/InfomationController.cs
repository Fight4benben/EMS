using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class InfomationController : Controller
    {
        // GET: Infomation
        public ActionResult MeterStatus()
        {
            return View();
        }
    }
}