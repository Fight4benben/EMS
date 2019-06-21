using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IMeterAlarmDbContext
    {
        PageInfo GetPageInfoList(string userName, int pageSize);
        List<MeterAlarmInfo> GetMeterAlarmingList(string userName, int pageIndex, int pageSize);
        int SetConfirmMeterAlarm(string userName, string describe, string[] ids);
        int SetConfirmAllMeterAlarm(string userName, string describe);
    }
}
