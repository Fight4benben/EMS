using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class OverAllSearchDbContext : IOverAllSearchDbContext
    {
       

        public List<CompareData> GetLast31DayList(string type, string keyWord, string startDay, string endDay)
        {
            throw new NotImplementedException();
        }

        public List<CompareData> GetMonthList(string type, string keyWord, string startDay, string endDay)
        {
            throw new NotImplementedException();
        }

        public List<CompareData> GetMomMonthList(string type, string keyWord, string startDay, string endDay)
        {
            throw new NotImplementedException();
        }

        public List<CompareData> GetCompareMonthList(string type, string keyWord, string startDay, string endDay)
        {
            throw new NotImplementedException();
        }
       
    }
}
