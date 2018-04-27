using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class HistoryDbContext
    {
        private HistoryDB _db = new HistoryDB();

        public List<HistoryValue> GetHistoryValues(string[] meterIds,string[] meterParamIds,DateTime time)
        {
            string month = time.Month.ToString("00");
            int day = time.Day;
            int hour = time.Hour;
            int minute = time.Minute;

            string meters ="('"+ string.Join("','",meterIds)+"')";
            string meterparams = "('" + string.Join("','", meterParamIds) + "')";

            string sql = @"SELECT F_MeterID MeterID,F_MeterParamID MeterParamID, dbo.SelectBinarysToDoubleByDateOfFive(F_Month"+month+
                @","+day+","+hour+","+minute+ @") Value FROM HistoryData WITH(NOLOCK) WHERE F_Year = "+time.Year+" AND F_MeterID in"+meters+""+
                " AND F_MeterParamID in"+ meterparams + "";

            return _db.Database.SqlQuery<HistoryValue>(sql).ToList();
        }
    }
}
