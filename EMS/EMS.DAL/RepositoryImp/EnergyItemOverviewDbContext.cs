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
    public class EnergyItemOverviewDbContext:IEnergyItemOverviewDbContext
    {
        private EnergyDB _db = new EnergyDB();
        
        public List<EnergyItemValue> GetEnergyItemMomDayValueList(string buildId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EnergyItemValue>(EnergyItemOverviewResources.EnergyItemMomDaySQL, sqlParameters).ToList();
        }

        public List<EnergyItemValue> GetEnergyItemRankByMonthValueList(string buildId, string date)
        {
            throw new NotImplementedException();
        }

        public List<EnergyItemValue> GetEnergyItemMomLast31DayPieChartValueList(string buildId, string date)
        {
            throw new NotImplementedException();
        }

        public List<EnergyItemValue> GetEnergyItemMomLast31DayValueList(string buildId, string date)
        {
            throw new NotImplementedException();
        }
    }
}
