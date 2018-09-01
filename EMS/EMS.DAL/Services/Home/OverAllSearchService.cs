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
    public class OverAllSearchService
    {
        private OverAllSearchDbContext context;

        public OverAllSearchService()
        {
            context = new OverAllSearchDbContext();
        }

        /// <summary>
        /// 获取搜索结果
        /// </summary>
        /// <param name="type">支路："Circuit"；部门："Dept";区域："Region"</param>
        /// <param name="keyWord">搜索内容</param>
        /// <param name="endDay">结束时间（"yyyy-MM-dd"）</param>
        public OverAllSearchViewModel GetViewModel(string type, string keyWord, string endDay)
        {
            DateTime startTime = Convert.ToDateTime(endDay);
             startTime = startTime.AddDays(-startTime.Day+1);
            string startDay = startTime.ToString("yyyy-MM-dd");

            OverAllSearchViewModel viewModel = new OverAllSearchViewModel();
            List<EMSValue> last31Day= context.GetLast31DayList(type, keyWord, endDay);
            List<EMSValue> monthData = new List<EMSValue>();
            if (last31Day != null && last31Day.Count > 0)
            {
                //var monthData = ( last31Day.Where(x=>x.Time > startTime));
                foreach (var item in last31Day)
                {
                    if (item.Time >= startTime)
                    {
                        monthData.Add(item);
                    }
                }
            }

            viewModel.Last31Day= context.GetLast31DayList(type, keyWord,endDay);
            viewModel.MonthDate = monthData;
            viewModel.MomData = context.GetMomMonthList(type,keyWord,startDay,endDay);
            viewModel.CompareData = context.GetCompareMonthList(type, keyWord, startDay, endDay);

            return viewModel;
        }
    }
}
