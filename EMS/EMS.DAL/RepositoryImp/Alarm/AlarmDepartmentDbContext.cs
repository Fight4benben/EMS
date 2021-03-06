﻿using EMS.DAL.Entities;
using EMS.DAL.StaticResources;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp
{
    public class AlarmDepartmentDbContext
    {
        private EnergyDB _db = new EnergyDB();

        public List<BuildAlarmLevel> GetBuildAlarmLevelValueList(string buildId)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId)
            };
            return _db.Database.SqlQuery<BuildAlarmLevel>(AlarmDepartmentResources.GetAlarmDeptLevelValueSQL, sqlParameters).ToList();
        }

        public int SetBuildAlarmLevel(string buildId, string energyCode, decimal level1, decimal level2)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyCode",energyCode),
                new SqlParameter("@Level1",level1),
                new SqlParameter("@Level2",level2)
            };
            return _db.Database.ExecuteSqlCommand(AlarmDepartmentResources.SetAlarmDeptLevelValueSQL, sqlParameters);
        }

        public List<CompareData> GetMomDayValueList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentResources.DeptMomDayOverLevel1ValueSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetCompareMonthValueList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentResources.DeptCompareMonthOverLevel1ValueSQL, sqlParameters).ToList();
        }

        public List<CompareData> GetCompareQuarterValueList(string buildId, string energyCode, string startDay, string endDay)
        {
            SqlParameter[] sqlParameters ={
                new SqlParameter("@BuildID",buildId),
                new SqlParameter("@EnergyItemCode",energyCode),
                new SqlParameter("@StartDay",startDay),
                new SqlParameter("@EndDay",endDay)
            };
            return _db.Database.SqlQuery<CompareData>(AlarmDepartmentResources.DeptCompareQuarterOverLevel1ValueSQL, sqlParameters).ToList();
        }

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        public List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId)
        {
            return _db.Database.SqlQuery<EnergyItemDict>(SharedResources.EnergyItemDictSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }
    }
}
