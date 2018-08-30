using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class MapViewController : Controller
    {
        // GET: MapView
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
           
        }
    }
}