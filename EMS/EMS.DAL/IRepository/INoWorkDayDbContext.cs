using EMS.DAL.Entities;
using System.Collections.Generic;

namespace EMS.DAL.IRepository
{
    public interface INoWorkDayDbContext
    {
        List<NoWorkDay> GetCircuitData(string buildID, string code, string beginDate, string endDate);
        List<NoWorkDay> GetCircuitData(string buildID, string code, string[] ids, string beginDate, string endDate);
    }
}
