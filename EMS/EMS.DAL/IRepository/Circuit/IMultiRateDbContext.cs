using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository.Circuit
{
    public interface IMultiRateDbContext
    {
        List<MultiRateData> GetReportValueList(string buildID, string code, string type, string date);

        List<MultiRateData> GetReportValueList(string buildID, string code, string type, string date, string[] circuitIds);

        List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId);
        List<Entities.CircuitList> GetCircuitListByBIdAndEItemCode(string buildId, string energyCode);
    }
}
