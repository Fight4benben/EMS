using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IRegionCompareDbContext
    {
        List<EMSValue> GetCompareValueList(string energyCode, string regionID, string date);
        List<TreeViewInfo> GetTreeViewInfoList(string buildId, string energyCode);
    }
}
