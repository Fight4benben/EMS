using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    public class DepartmentRankController : ApiController
    {
        DepartmentRankService service = new DepartmentRankService();
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModelByName(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string date)
        {
            try
            {
                
                return service.GetViewModel(buildId,date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string date,string energyCode)
        {
            try
            {
                return service.GetViewModel(buildId, date,energyCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
