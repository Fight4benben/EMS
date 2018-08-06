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
        List<HistoryBinarys> GetHistoryBinaryString(string[] meterIds, string[] meterParamIds, DateTime time);
        List<TreeViewInfo> GetTreeViewInfoList(string buildId, string energyCode);
    }
}
