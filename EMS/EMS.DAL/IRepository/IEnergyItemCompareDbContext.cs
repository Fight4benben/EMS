using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IEnergyItemCompareDbContext
    {
        List<EnergyItemValue> GetEnergyItemCompareValueList(string buildId, string formulaId, string date);
    }
}
