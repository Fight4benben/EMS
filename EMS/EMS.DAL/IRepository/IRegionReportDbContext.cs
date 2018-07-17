using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IRegionReportDbContext
    {
        List<ReportValue> GetReportValueList(string[] RegionIDs, string energyCode, string date, string type);
        List<TreeViewInfo> GetTreeViewInfoList(string buildId);
    }
}
