using EMS.DAL.Utils;
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
    public class AntiSqlInjectAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var actionParams = actionContext.ActionDescriptor.GetParameters();
            var actionArguments = actionContext.ActionArguments;

            foreach (var p in actionParams)
            {
                var value = actionContext.ActionArguments[p.ParameterName];

                var pType = p.ParameterType;

                if (value == null)
                    continue;

                if (!pType.IsClass)
                    continue;

                if (value is string)
                {
                    //string result = (string)value;
                    //if (result.Length > 20 || result.Contains("'") || result.Contains("%") ||
                    //result.ToLower().Contains("and") || result.ToLower().Contains("or") ||
                    //result.Contains("+") || result.Contains("-") || result.ToLower().Contains("select") || result.ToLower().Contains("delete") ||
                    //result.ToLower().Contains("update") || result.ToLower().Contains("insert"))
                    //    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK,new { Error="检测到SQL注入攻击！"});
                    actionContext.ActionArguments[p.ParameterName] = 
                    Util.FilterSql(value.ToString());
                }


            }
        }
    }
}