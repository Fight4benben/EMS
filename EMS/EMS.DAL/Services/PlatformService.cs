using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class PlatformService
    {
        private IPlatformDbContext context;
        private IMapDbContext MapContext;

        public PlatformService()
        {
            context = new PlatformDbContext();
            MapContext = new MapDbContext();
        }


        public PlatformViewModel GetViewModel(string userName)
        {
            PlatformViewModel viewModel = new PlatformViewModel();

            string endDate = DateTime.Now.ToString("yyyy-MM-dd");

            viewModel.Builds = MapContext.GetBuildsLocationByUserName(userName);
            viewModel.Device = context.GetDeviceCount(userName);
            viewModel.RunningDay = context.GetRunningDay();
            viewModel.Standardcoal = context.GetStandardcoalMonthList(userName, endDate);
            viewModel.DayDate = context.GetDayList(userName, endDate);
            viewModel.MonthDate = context.GetMonthList(userName, endDate);
            viewModel.YearDate = context.GetYearList(userName, endDate);

            return viewModel;
        }
    }
}
