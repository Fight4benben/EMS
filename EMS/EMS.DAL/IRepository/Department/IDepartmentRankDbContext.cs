using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IDepartmentRankDbContext
    {
        List<EMSValue> GetRankList(string buildId,string startDate,string endDate,string energyCode);
    }
}
