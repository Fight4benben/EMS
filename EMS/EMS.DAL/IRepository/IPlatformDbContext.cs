using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IPlatformDbContext
    {
        int GetRunningDay();
        DeviceCount GetDeviceCount(string userName);
        List<PlatformItemValue> GetStandardcoalMonthList(string userName, string endDate);
        List<PlatformItemValue> GetDayList(string userName, string endDate);
        List<PlatformItemValue> GetMonthList(string userName, string endDate);
        List<PlatformItemValue> GetYearList(string userName, string endDate);
    }
}
