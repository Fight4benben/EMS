using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IRegionMainDbContext
    {
        List<EMSValue> GetRegionMainCompareValueList(string buildId,string date,string energyCode,string filterType);
        List<RankValue> GetRegionMainRankValueList(string buildId,string date,string energyCode,string filterType);
        List<EMSValue> GetRegionPieValueList(string buildId,string date ,string energyCode,string filterType);
        List<EMSValue> GetRegionStackValueList(string buildId,string date,string energyCode,string filterType);
    }
}
