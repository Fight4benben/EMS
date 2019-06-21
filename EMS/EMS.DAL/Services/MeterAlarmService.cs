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
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;
            viewModel.Data = context.GetMeterAlarmingList(userName, pageIndex, pageSize);

            return viewModel;
        }

        public object SetConfirmMeterAlarm(string userName, string describe, string ids)
        {
            ResultState resultState = new ResultState();

            string[] idsArry = ids.Split(',');

            int result = context.SetConfirmMeterAlarm(userName, describe, idsArry);

            if (result > 0)
            {
                resultState.State = 0;
                resultState.Details = "OK ：操作成功";
            }
            else
            {
                resultState.State = 1;
                resultState.Details = "NG ：确认失败！！！";
            }

            return resultState;
        }

        public object SetConfirmAllMeterAlarm(string userName, string describe)
        {
            ResultState resultState = new ResultState();

            int result = context.SetConfirmAllMeterAlarm(userName, describe);

            if (result > 0)
            {
                resultState.State = 0;
                resultState.Details = "OK ：操作成功";
            }
            else
            {
                resultState.State = 1;
                resultState.Details = "NG ：确认失败！！！";
            }

            return resultState;
        }


        public MeterAlarmViewModel GetAlarmLogViewModel(string userName, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            string beginDate = DateTime.Now.ToString("yyyy-MM-dd 00:00");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd 23:59");

            viewModel.PageInfos = context.GetPageInfoList(userName, pageSize);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogList(userName, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }

    }
}
