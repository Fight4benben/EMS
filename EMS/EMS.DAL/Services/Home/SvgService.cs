using EMS.DAL.IRepository.Home;
using EMS.DAL.RepositoryImp.Home;
using EMS.DAL.ViewModels;
using EMS.DAL.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services.Home
{
    public class SvgService
    {
        ISvgDbContext context;
        public SvgService()
        {
            context = new SvgDbContext();
        }

        public SvgViewModel GetSvgViewModelByName(string userName)
        {
            SvgViewModel viewModel = new SvgViewModel();
            List<BuildViewModel> builds = context.GetBuildsByUserName(userName);

            if (builds.Count == 0)
                return viewModel;

            string buildId = builds.First().BuildID;

            viewModel.Builds = builds;

            List<SvgInfo> svgs = context.GetSvgListByBuildId(buildId);

            if (svgs.Count == 0)
                return viewModel;

            viewModel.Svgs = svgs;

            string path = context.GetSvgViewById(svgs.First().SvgID);

            viewModel.SvgView = path;

            return viewModel;
        }

        public SvgViewModel GetSvgViewModel(string buildId)
        {
            SvgViewModel viewModel = new SvgViewModel();

            List<SvgInfo> svgs = context.GetSvgListByBuildId(buildId);

            if (svgs.Count == 0)
                return viewModel;

            viewModel.Svgs = svgs;

            string path = context.GetSvgViewById(svgs.First().SvgID);

            viewModel.SvgView = path;

            return viewModel;
        }

        public SvgViewModel GetSvgViewModel(string buildId,string svgId)
        {
            SvgViewModel viewModel = new SvgViewModel();

            string path = context.GetSvgViewById(svgId);

            viewModel.SvgView = path;

            return viewModel;
        }

        public SvgDataViewModel GetSvgData(string buildId, string svgId)
        {
            return context.GetDataViewModel(buildId,svgId);
        }
    }
}
