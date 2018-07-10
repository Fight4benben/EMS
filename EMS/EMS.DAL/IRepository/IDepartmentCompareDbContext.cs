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
        List<DepartmentValue> GetDepartmentCompareValueList(string buildId, string energyItemCode, string date);
    }
}
