using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI.Filters
{
    public class NoCacheAttribute:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetNoStore();
        }
    }
}