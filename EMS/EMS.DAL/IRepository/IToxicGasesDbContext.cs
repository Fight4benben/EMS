﻿using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IToxicGasesDbContext
    {
        List<MeterList> GetMeterList(string buildID);
        List<MeterValue> GetOneMeterValue(string meterID, string buildID);

    }
}
