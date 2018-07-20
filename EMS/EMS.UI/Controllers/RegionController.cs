using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class RegionController : Controller
    {
        // GET: Region
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

        public ActionResult Cost()
        {
            return View();
        }

        public FileResult GetExcel(string buildId, string energyCode, string type, string regionIDs, string date)
        {
            string[] regions = regionIDs.Split(',');
            
            string basePath = HttpContext.Server.MapPath("~/App_Data/");
            RegionReportService service = new RegionReportService();
            Excel excel = service.ExportReportToExcel(basePath, buildId, energyCode, regions,date,type);
            return File(excel.Data, "application/ms-excel", excel.Name);
        }
    }
}