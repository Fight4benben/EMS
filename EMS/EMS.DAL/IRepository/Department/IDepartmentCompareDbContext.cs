using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IDepartmentCompareDbContext
    {
        List<EMSValue> GetDepartmentCompareValueList(string buildId, string energyCode,string departmentID, string date);
    }
}
