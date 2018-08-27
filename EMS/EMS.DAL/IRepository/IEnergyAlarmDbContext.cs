using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IEnergyAlarmDbContext
    {
        List<EnergyAlarm> GetEnergyOverLimitValueList(string buildId, string date);
    }
}
