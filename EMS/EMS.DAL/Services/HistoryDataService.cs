using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class HistoryDataService
    {
        HistoryDbContext context;

        public HistoryDataService()
        {
            context = new HistoryDbContext();
        }


        public List<CollectValue> GetCollectValues(string meters,string meterparams,string startTime,string endTime)
        {
            DateTime start = Util.ConvertString2DateTime(startTime,"yyyy-MM-dd HH:mm:ss");
            DateTime end = Util.ConvertString2DateTime(endTime, "yyyy-MM-dd HH:mm:ss");

            string[] meterArray = meters.Split(';');
            string[] paramsArray = meterparams.Split(';');

            List<HistoryValue> startValues = context.GetHistoryValues(meterArray,paramsArray,start);
            List<HistoryValue> endValues = context.GetHistoryValues(meterArray, paramsArray, end);


            return null;
        }
    }
}
