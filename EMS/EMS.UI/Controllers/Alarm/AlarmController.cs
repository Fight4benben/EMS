using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class AlarmController : Controller
    {
        // GET: Alarm
        public ActionResult EquipAlarm()
        {
            return View();
        }

        public ActionResult OutOfWorkAlarm()
        {
            return View();
        }

        public ActionResult DepartmentAlarm()
        {
            return View();
        }

        public ActionResult DepartmentOutOfAlarm()
        {
            return View();
        }

        public ActionResult AlarmRecord()
        {
            return View();
        }
    }
}