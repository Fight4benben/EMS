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
    public class CircuitController : Controller
    {
        // GET: Circuit
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public FileResult GetExcel(string buildId, string energyCode, string type, string circuits,string date)
        {
            string basePath = HttpContext.Server.MapPath("~/App_Data/");
            CircuitReportService service = new CircuitReportService();
            Excel excel = service.ExportCircuitReportToExcel(basePath,buildId,energyCode,circuits,type,date);
            return File(excel.Data,"application/ms-excel",excel.Name);
        }
    }
}