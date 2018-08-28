﻿using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IEnergyAlarmDbContext
    {
        List<EnergyAlarm> GetEnergyOverLimitValueList(string buildId, string date);
        List<CompareData> GetDayCompareValueList(string buildId, string startDay);
        List<CompareData> GetMonthCompareValueList(string buildId, string startDay, string endDay);
        List<CompareData> GetDeptCompareValueList(string buildId, string startDay, string endDay);
    }
}
