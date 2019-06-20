using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class MeterAlarmController : ApiController
    {
        MeterAlarmService service = new MeterAlarmService();

        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Get(string pageIndex, string pageSize)
        {
            try
            {
                string userName = User.Identity.Name;

                return service.GetViewModel(userName, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}