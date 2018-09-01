using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class MapService
    {
        private MapDbContext context;

        public MapService()
        {
            context = new MapDbContext();
        }

        public MapViewModel GetViewModelByUserName(string userName)
        {
            List<BuildMap> buildInfos = context.GetBuildsLocationByUserName(userName);
            MapViewModel viewModel = new MapViewModel();
            viewModel.Builds = buildInfos;

            return viewModel;
        }
    }
}
