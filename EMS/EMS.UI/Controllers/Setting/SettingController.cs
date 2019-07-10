using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        // GET: Setting
        //设置重点设备非工作时间及其值，供越线报警使用
        public ActionResult OutOfWork()
        {
            return View();
        }

        public ActionResult DepartmentOutOfWork()
        {
            return View();
        }

        public ActionResult UserBuilding()
        {
            return View();
        }

        public ActionResult UserMenu()
        {
            return View();
        }

        public ActionResult SvgSetting()
        {
            return View();
        }

        public ActionResult Build()
        {
            return View();
        }

        public ActionResult OverLimit()
        {
            return View();
        }
    }
}