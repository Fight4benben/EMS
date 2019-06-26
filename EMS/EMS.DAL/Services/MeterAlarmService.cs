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
        private ITreeViewDbContext tvContext;

        public MeterAlarmService()
        {
            context = new MeterAlarmDbContext();
            tvContext = new TreeViewDbContext();
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

        public string GetIsAlarming(string userName)
        {
            string result = "";
            if (context.GetIsAlarming(userName) > 0)
            {
                result = "IsAlarming:true";
                return result;
            }
            else
            {
                result = "IsAlarming:false";
                return result;
            }
        }

        public MeterAlarmViewModel GetAlarmingViewModel(string userName, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.PageInfos = context.GetPageInfoList(userName, pageSize);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;
            viewModel.Data = context.GetMeterAlarmingList(userName, pageIndex, pageSize);

            return viewModel;
        }

        /// <summary>
        /// 确认选择的报警
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="describe"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 确认所有的报警
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="describe"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 根据用户名，获取报警记录，（默认查询当天的报警记录）
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MeterAlarmViewModel GetAlarmLogViewModel(string userName)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            int pageIndex = 1;
            int pageSize = 100;
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd 00:00");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd 23:59");
            string buildID = "";

            viewModel.Builds = tvContext.GetBuildsByUserName(userName);
            viewModel.AlarmType = context.GetAlarmType();

            if (viewModel.Builds.Count > 0)
            {
                buildID = viewModel.Builds.First().BuildID;
            }


            viewModel.PageInfos = context.GetAlarmLogPageInfo(userName, buildID, pageSize, beginDate, endDate);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogList(userName, buildID, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }

        /// <summary>
        /// 根据 用户、建筑ID ，时间，获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="buildID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MeterAlarmViewModel GetAlarmLogViewModel(string userName, string buildID, string beginDate, string endDate, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.PageInfos = context.GetAlarmLogPageInfo(userName, buildID, pageSize, beginDate, endDate);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogList(userName, buildID, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }

        /// <summary>
        /// 根据建筑ID，报警类型,获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="buildID"></param>
        /// <param name="alarmType"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MeterAlarmViewModel GetAlarmLogViewModel(string userName, string buildID, string alarmType, string beginDate, string endDate, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.PageInfos = context.GetAlarmLogByBuildIDAlarmTypePageInfo(userName, buildID, alarmType, pageSize, beginDate, endDate);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogByBuildIDAlarmTypeList(userName, buildID, alarmType, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }



        /// <summary>
        /// 根据报警类型，获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="alarmType"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MeterAlarmViewModel GetAlarmLogViewModelByAlarmType(string userName, string alarmType, string beginDate, string endDate, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.Builds = tvContext.GetBuildsByUserName(userName);
            viewModel.AlarmType = context.GetAlarmType();

            viewModel.PageInfos = context.GetAlarmLogByAlarmTypePageInfo(userName, alarmType, pageSize, beginDate, endDate);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogByAlarmTypeList(userName, alarmType, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }


        /// <summary>
        /// 根据建筑ID，报警类型,获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="buildID"></param>
        /// <param name="alarmType"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MeterAlarmViewModel GetAlarmLogViewModelByAlarmType(string userName, string buildID, string alarmType, string beginDate, string endDate, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.Builds = tvContext.GetBuildsByUserName(userName);
            viewModel.AlarmType = context.GetAlarmType();

            viewModel.PageInfos = context.GetAlarmLogByBuildIDAlarmTypePageInfo(userName, buildID, alarmType, pageSize, beginDate, endDate);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogByBuildIDAlarmTypeList(userName, buildID, alarmType, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }



        /// <summary>
        /// 根据用户，建筑ID，仪表ID，获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="buildID"></param>
        /// <param name="meterID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MeterAlarmViewModel GetAlarmLogViewModelByMeterID(string userName, string buildID, string meterID, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd 00:00");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd 23:59");

            viewModel.PageInfos = context.GetAlarmLogPageInfo(userName, buildID, meterID, pageSize, beginDate, endDate);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogList(userName, buildID, meterID, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }

        /// <summary>
        /// 根据用户，建筑ID，仪表ID，时间，获取报警记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="buildID"></param>
        /// <param name="meterID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MeterAlarmViewModel GetAlarmLogViewModelByMeterID(string userName, string buildID, string meterID, string beginDate, string endDate, int pageIndex = 1, int pageSize = 100)
        {
            MeterAlarmViewModel viewModel = new MeterAlarmViewModel();

            viewModel.PageInfos = context.GetAlarmLogPageInfo(userName, buildID, meterID, pageSize, beginDate, endDate);
            viewModel.PageInfos.CurrentPage = pageIndex;
            viewModel.PageInfos.PageSize = pageSize;

            viewModel.AlarmLogs = context.GetAlarmLogList(userName, buildID, meterID, pageIndex, pageSize, beginDate, endDate);

            return viewModel;
        }



    }
}
