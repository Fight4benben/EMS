using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IHistoryParamDbContext
    {
        List<HistoryParameterValue> GetParamValue(string circuitID, string[] meterParamIds, string dateTime, int step);
        List<HistoryParameterValue> GetParamByMeterIDValue(string meterID, string dateTime, int step);
        List<HistoryBinarys> GetHistoryBinaryString(string circuitID, string[] meterParamIds, DateTime time);
        List<TreeViewInfo> GetTreeViewInfoList(string buildId, string energyCode);
    }
}
