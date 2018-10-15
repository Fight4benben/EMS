using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EMS.UI.Filters
{
    public class ActionLogAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Console.WriteLine("当前操作：" + actionExecutedContext.ActionContext.ActionDescriptor.ActionName + ",用户名：" + actionExecutedContext.ActionContext.RequestContext.Principal.Identity.Name);
        }
    }
}