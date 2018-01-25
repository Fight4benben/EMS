using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Entities;

namespace EMS.DAL.IRepository
{
    public interface ICircuitOverviewDbContext
    {
        List<CircuitValue> GetCircuitLoadValueList(string buildId, string circuitId, string date);
        List<CircuitValue> GetCircuitMomValueList(string buildId, string circuitId, string date);
        List<CircuitValue> GetCircuit48HoursValueList(string buildId, string circuitId, string date);
        List<CircuitValue> GetCircuit31DaysValueList(string buildId, string circuitId, string date);
        List<CircuitValue> GetCircuit12MonthValueList(string buildId, string circuitId, string date);
        List<CircuitValue> GetCircuit3YearValueList(string buildId, string circuitId, string date);
    }
}
