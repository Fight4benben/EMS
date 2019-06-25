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
        int GetIsAlarming(string userName);
        PageInfo GetPageInfoList(string userName, int pageSize);
        List<MeterAlarmInfo> GetMeterAlarmingList(string userName, int pageIndex, int pageSize);

        PageInfo GetAlarmLogPageInfo(string userName, int pageSize, string beginDate, string endDate);
        PageInfo GetAlarmLogPageInfo(string userName, string buildID, int pageSize, string beginDate, string endDate);
        PageInfo GetAlarmLogPageInfo(string userName, string buildID, string meterID, int pageSize, string beginDate, string endDate);

        List<MeterAlarmLog> GetAlarmLogList(string userName, int pageIndex, int pageSize, string beginDate, string endDate);
        List<MeterAlarmLog> GetAlarmLogList(string userName, string buildID, int pageIndex, int pageSize, string beginDate, string endDate);
        List<MeterAlarmLog> GetAlarmLogList(string userName, string buildID, string meterID, int pageIndex, int pageSize, string beginDate, string endDate);


        int SetConfirmMeterAlarm(string userName, string describe, string[] ids);
        int SetConfirmAllMeterAlarm(string userName, string describe);
    }
}
