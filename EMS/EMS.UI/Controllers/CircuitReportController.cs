using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class CircuitReportController : ApiController
    {
        CircuitReportService service = new CircuitReportService();


        public object GetReport()
        {
            string userName = User.Identity.Name;

            return service.GetViewModel(userName);
        }

        public object GetReport(string buildId,string type,string date)
        {
            return service.GetViewModel(buildId,type,date);
        }

        public object GetReport(string buildId, string energyCode,string type, string date)
        {
            return service.GetViewModel(buildId, energyCode, type, date);
        }

        public object GetReport(string buildId, string energyCode,string circuits, string type, string date)
        {
            return service.GetViewModel(buildId, energyCode, circuits,type, date);
        }
    }
}
