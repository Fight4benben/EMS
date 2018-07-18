using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class DepartmentController : Controller
    {
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

        public FileResult GetExcel(string buildId, string energyCode, string departmentIDs, string date, string type)
        {
            string[] formulars = departmentIDs.Split(',');

            string basePath = HttpContext.Server.MapPath("~/App_Data/");
            //CircuitReportService service = new CircuitReportService();
            DepartmentReportService service = new DepartmentReportService();
            Excel excel = service.ExportReportToExcel(basePath, buildId,energyCode, formulars, date, type);
            return File(excel.Data, "application/ms-excel", excel.Name);
        }
    }
}