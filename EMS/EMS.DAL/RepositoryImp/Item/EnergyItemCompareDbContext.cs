﻿using EMS.DAL.Entities;
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
    public class EnergyItemCompareDbContext:IEnergyItemCompareDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<EnergyItemValue> GetEnergyItemCompareValueList(string buildId, string formulaId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@FormulaID",formulaId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EnergyItemValue>(EnergyItemCompareResources.EnergyItemCompareSQL, sqlParameters).ToList();
        }

        public List<EnergyItemValue> GetRingRatioValueList(string buildId, string formulaId, string date)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@FormulaID",formulaId),
                new SqlParameter("@EndTime",date)
            };
            return _db.Database.SqlQuery<EnergyItemValue>(EnergyItemCompareResources.ItemDayRingRatioSQL, sqlParameters).ToList();
        }
    }
}
