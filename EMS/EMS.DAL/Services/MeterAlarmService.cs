using EMS.DAL.Entities;
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
    public class MeterAlarmService
    {
        private IMeterAlarmDbContext context;

        public MeterAlarmService()
        {
            context = new MeterAlarmDbContext();
        }

        /*
        public MeterAlarmViewModel GetViewModel(string userName)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.PageInfos = context.GetPageInfoList(userName);
            viewModel.Data = context.GetMeterAlarmingList(userName,1,100);

            return viewModel;
        }
        */

        public MeterAlarmViewModel GetViewModel(string userName, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.PageInfos = context.GetPageInfoList(userName, pageSize);
            viewModel.Data = context.GetMeterAlarmingList(userName, pageIndex, pageSize);

            return viewModel;
        }

    }
}
