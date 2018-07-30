using EMS.DAL.Services;
using EMS.DAL.ViewModels;
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

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult Compare()
        {
            return View();
        }

        public ActionResult Statistic()
        {
            return View();
        }

        public FileResult GetExcel(string buildId, string type, string formularIds, string date)
        {
            string[] formulars = formularIds.Split(',');
           
            string basePath = HttpContext.Server.MapPath("~/App_Data/");
            //CircuitReportService service = new CircuitReportService();
            EnergyItemReportService service = new EnergyItemReportService();
            Excel excel = service.ExportReportToExcel(basePath, buildId,formulars,date,type);
            return File(excel.Data, "application/ms-excel", excel.Name);
        }
    }
}