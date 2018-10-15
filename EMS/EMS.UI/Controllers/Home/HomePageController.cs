using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using EMS.UI.Filters;
using System;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class HomePageController : ApiController
    {
        HomeServices service = new HomeServices();

        /// <summary>
        /// 默认请求
        /// </summary>
        /// <returns></returns>
        public object GetHome()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            string userName = User.Identity.Name;

            try
            {
                homeViewModel = service.GetHomeViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return homeViewModel; 
        }

        public object GetHome(string buildId, string date)
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            try
            {
                homeViewModel = service.GetHomeViewModel(buildId,date);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return homeViewModel;
        }
    }
}
