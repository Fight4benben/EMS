using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class OverAllSearchDbContext : IOverAllSearchDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EMSValue> GetLast31DayList(string type, string keyWord, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitLast31DaySQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionLast31DaySQL;
                    break;

                default:
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<EMSValue> GetMonthList(string type, string keyWord, string startDay, string endDay)
        {
            string sql;

            switch (type)
            {
                case "Circuit":
                    sql = OverAllSearchResources.CircuitLast31DaySQL;
                    break;

                case "Dept":
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;

                case "Region":
                    sql = OverAllSearchResources.RegionLast31DaySQL;
                    break;

                default:
                    sql = OverAllSearchResources.DeptLast31DaySQL;
                    break;
            }

            SqlParameter[] sqlParameters ={
                new SqlParameter("@KeyWord",keyWord),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<EMSValue>(sql, sqlParameters).ToList();
        }

        public List<CompareData> GetMomMonthList(string type, string keyWord, string startDay, string endDay)
        {
           
        }

        public List<CompareData> GetCompareMonthList(string type, string keyWord, string startDay, string endDay)
        {
            throw new NotImplementedException();
        }
       
    }
}
