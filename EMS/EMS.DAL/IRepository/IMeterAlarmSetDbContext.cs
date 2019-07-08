using EMS.DAL.Entities.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IMeterAlarmSetDbContext
    {
        List<MeterAlarmSet> GetMeterParamList(string buildID, string circuitID);
    }
}
