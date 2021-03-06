﻿using EMS.DAL.Services;
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

        public ActionResult Compare()
        {
            return View();
        }

        public ActionResult Collect()
        {
            return View();
        }

        public ActionResult RingRatio()
        {
            return View();
        }

        public ActionResult Param()
        {
            return View();
        }

        public ActionResult Environment()
        {
            return View();
        }
        public ActionResult MultiRate()
        {
            return View();
        }

        public ActionResult NoWorkDay()
        {
            return View();
        }

        public ActionResult Toxic()
        {
            return View();
        }

        public ActionResult MultiCollect()
        {
            return View();
        }

        public ActionResult EPECollect()
        {
            return View();
        }

        public FileResult GetExcel(string buildId, string energyCode, string type, string circuits,string date)
        {
            string[] circuitArray = circuits.Split(',');
            List<string> circuitList = new List<string>();
            foreach (string circuit in circuitArray)
            {
                circuitList.Add(buildId + circuit);
            }
            string basePath = HttpContext.Server.MapPath("~/App_Data/");
            CircuitReportService service = new CircuitReportService();
            Excel excel = service.ExportCircuitReportToExcel(basePath,buildId,energyCode,circuitList.ToArray(),type,date);
            return File(excel.Data,"application/ms-excel",excel.Name);
        }
    }
}