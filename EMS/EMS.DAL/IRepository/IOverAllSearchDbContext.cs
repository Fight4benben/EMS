using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IOverAllSearchDbContext
    {
        List<CompareData> GetLast31DayList(string type, string keyWord, string startDay, string endDay);
        List<CompareData> GetMonthList(string type, string keyWord, string startDay, string endDay);
        List<CompareData> GetMomMonthList(string type, string keyWord, string startDay, string endDay);
        List<CompareData> GetCompareMonthList(string type, string keyWord, string startDay, string endDay);
    }
}
