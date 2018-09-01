using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface ICircuitCompareDbContext
    {
        List<CircuitValue> GetCircuitCompareValueList(string buildId, string circuitId, string date);
    }
}
