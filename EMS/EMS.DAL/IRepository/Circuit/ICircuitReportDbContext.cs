﻿using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface ICircuitReportDbContext
    {
        List<EMS.DAL.Entities.CircuitList> GetCircuitListByBIdAndEItemCode(string buildId, string energyItemCode);

        List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId);

        List<ReportValue> GetReportValueList(string[] circuits,string date, string type);

        EnergyItemDict GetUnitByEnergyCode(string energyCode);
    }
}
