using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IDepartmentEnergyAverageDbContext
    {
        List<EnergyAverage> GetDeptMonthEnergyAverageList(string buildId, string energyCode, string departmentID, string startDay, string endDay);
        List<EnergyAverage> GetDeptQuarterEnergyAverageList(string buildId, string energyCode, string departmentID, string startDay, string endDay);
        List<EnergyAverage> GetDeptYearEnergyAverageList(string buildId, string energyCode, string departmentID, string startDay, string endDay);
    }
}
