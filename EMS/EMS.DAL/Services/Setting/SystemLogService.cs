using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.Utils;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class SystemLogService
    {
        private SystemLogDbContext context;

        public SystemLogService()
        {
            context = new SystemLogDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">DD:当日
        ///                     WW:本周
        ///                     MM：本月
        ///                     YY：本年
        ///                     LDD:昨天
        ///                     LWW:上周
        ///                     LMM：本月
        ///                     LYY：本年
        /// </param>
        /// <returns></returns>
        public SystemLogViewModel GetViewModel(string type)
        {
            string startDay;
            string endDay;

            switch (type)
            {
                case "DD":
                    startDay = DateTime.Now.ToString("yyyy-MM-dd");
                    endDay = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                    break;

                case "WW":
                    startDay = Util.GetWeekFirstDayMon(DateTime.Now).ToString("yyyy-MM-dd");
                    endDay = Util.GetWeekLastDaySun(DateTime.Now).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;

                case "MM":
                    startDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
                    endDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "YY":
                    startDay = DateTime.Now.ToString("yyyy") + "-01-01";
                    endDay = DateTime.Now.ToString("yyyy") + "-12-31 23:59:59";
                    break;

                case "LDD":
                    startDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    endDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;

                case "LWW":
                    startDay = Util.GetWeekFirstDayMon(DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd");
                    endDay = Util.GetWeekLastDaySun(DateTime.Now).AddDays(-7).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;

                case "LMM":
                    startDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1).ToString("yyyy-MM-dd");
                    endDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).AddMonths(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "LYY":
                    startDay = DateTime.Now.AddYears(-1).ToString("yyyy") + "-01-01";
                    endDay = DateTime.Now.AddYears(-1).ToString("yyyy") + "-12-31 23:59:59";
                    break;


                default:
                    startDay = DateTime.Now.ToString("yyyy-MM-dd");
                    endDay = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
            }


            List<LogInfo> logInfos = context.GetSystemLogList(startDay, endDay);

            SystemLogViewModel viewModel = new SystemLogViewModel();
            viewModel.LogInfos = logInfos;

            return viewModel;
        }

        public SystemLogViewModel GetViewModel(string startDay, string endDay)
        {

            List<LogInfo> logInfos = context.GetSystemLogList(startDay, endDay + " 23:59:59");

            SystemLogViewModel viewModel = new SystemLogViewModel();
            viewModel.LogInfos = logInfos;

            return viewModel;
        }

    }
}
