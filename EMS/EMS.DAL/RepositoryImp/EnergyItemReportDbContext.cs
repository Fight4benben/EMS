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
    public class EnergyItemReportDbContext : IEnergyItemReportDbContext
    {
        private EnergyDB _db = new EnergyDB();

        
        
        
        /// <summary>
        /// 获取报表
        /// </summary>
        /// <param name="energyCodes">分项用能编码</param>
        /// <param name="date">传入的日期</param>
        /// <param name="type">年月日类型：（DD：日报表，MM：月报表，YY:年报表）</param>
        /// <returns>List<ReportValue></returns>
        public List<ReportValue> GetReportValueList(string[] energyCodes, string date, string type)
        {
            string sql;
            switch (type)
            {
                case "DD":
                    sql = string.Format(EnergyItemReportResources.DayReportSQL, "'" + string.Join("','", energyCodes) + "'");
                    break;
                case "MM":
                    sql = string.Format(EnergyItemReportResources.MonthReportSQL, "'" + string.Join("','", energyCodes) + "'");
                    break;
                case "YY":
                    sql = string.Format(EnergyItemReportResources.YearReportSQL, "'" + string.Join("','", energyCodes) + "'");
                    break;
                default:
                    sql = string.Format(EnergyItemReportResources.DayReportSQL, "'" + string.Join("','", energyCodes) + "'");
                    break;
            }

            return _db.Database.SqlQuery<ReportValue>(sql, new SqlParameter("@EndTime", date)).ToList();

        }
    }
}
