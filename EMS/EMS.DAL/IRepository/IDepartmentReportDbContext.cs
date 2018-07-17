using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IDepartmentReportDbContext
    {
        List<ReportValue> GetReportValueList(string energyCode,string[] circuits, string date, string type);
    }
}
