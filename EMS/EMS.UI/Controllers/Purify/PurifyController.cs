using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class PurifyController : Controller
    {
        // GET: Purify
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gas()
        {
            return View();
        }

        public ActionResult LeakWater()
        {
            return View();
        }

        public ActionResult ThreeDModel()
        {
            return View();
        }
    }
}