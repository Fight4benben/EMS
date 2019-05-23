using EMS.UI.Filters;
using System.Web;
using System.Web.Mvc;

namespace EMS.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NoCacheAttribute());
        }
    }
}
