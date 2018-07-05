using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Main()
        {
            return View();
        }
    }
}