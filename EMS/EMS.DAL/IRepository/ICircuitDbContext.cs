﻿using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface ICircuitDbContext
    {
        List<Circuit> GetCircuitListByBIdAndEItemCode(string buildId, string energyItemCode);

        List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId); 
    }
}
