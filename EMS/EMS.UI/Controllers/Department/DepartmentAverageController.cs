﻿using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class DepartmentAverageController : ApiController
    {
        DepartmentEnergyAverageService service = new DepartmentEnergyAverageService();

        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModelByUserName(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string buildId, string energyCode, string type, string date)
        {
            try
            {
                return service.GetViewModel(buildId,energyCode,type,date);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
