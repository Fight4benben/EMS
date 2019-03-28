using EMS.DAL.Services.Home;
using EMS.DAL.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace EMS.UI.Controllers.Home
{
    [Authorize]
    public class SvgController : ApiController
    {
        SvgService service = new SvgService();
        public object Get()
        {

            SvgViewModel svgViewModel = new SvgViewModel();
            string userName = User.Identity.Name;
            string path = HostingEnvironment.MapPath("~/App_Data/SvgFiles");
            try
            {
                svgViewModel = service.GetSvgViewModelByName(userName);

                if (svgViewModel.SvgView != null)
                {
                    string fileName = path + "\\" + svgViewModel.SvgView;

                    svgViewModel.SvgView = File.ReadAllText(fileName, Encoding.UTF8);
                }
                
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return svgViewModel;
        }

        public object Get(string buildId)
        {

            SvgViewModel svgViewModel = new SvgViewModel();
            string path = HostingEnvironment.MapPath("~/App_Data/SvgFiles");
            try
            {
                svgViewModel = service.GetSvgViewModel(buildId);

                if (svgViewModel.SvgView != "not exist")
                {
                    string fileName = path + "\\" + svgViewModel.SvgView;

                    svgViewModel.SvgView = File.ReadAllText(fileName, Encoding.UTF8);
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return svgViewModel;
        }

        public object Get(string buildId,string svgId)
        {

            SvgViewModel svgViewModel = new SvgViewModel();
            string path = HostingEnvironment.MapPath("~/App_Data/SvgFiles");
            try
            {
                svgViewModel = service.GetSvgViewModel(buildId,svgId);

                if (svgViewModel.SvgView != "not exist")
                {
                    string fileName = path + "\\" + svgViewModel.SvgView;

                    svgViewModel.SvgView = File.ReadAllText(fileName, Encoding.UTF8);
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return svgViewModel;
        }

        public object Get(string buildId,string svgId,string dataType)
        {
            SvgDataViewModel dataViewModel = new SvgDataViewModel();

            try
            {
                dataViewModel = service.GetSvgData(buildId,svgId);
            }
            catch (Exception e)
            {
                return e.Message;
            }


            return dataViewModel;
        }


    }
}
