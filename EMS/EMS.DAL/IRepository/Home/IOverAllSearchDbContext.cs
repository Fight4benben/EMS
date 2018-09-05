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
        List<EMSValue> GetLast31DayList(string type, string keyWord, string buildID, string endDay);
        List<EMSValue> GetMonthList(string type, string keyWord, string buildID, string startDay, string endDay);
        List<CompareData> GetMomMonthList(string type, string keyWord, string buildID, string startDay, string endDay);
        List<CompareData> GetCompareMonthList(string type, string keyWord, string buildID, string startDay, string endDay);
    }
}
