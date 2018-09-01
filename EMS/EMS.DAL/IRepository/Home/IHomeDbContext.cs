using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Entities;
using EMS.DAL.ViewModels;

namespace EMS.DAL.IRepository
{
    public interface IHomeDbContext
    {
        IQueryable<BuildInfo> GetBuilds();

        BuildInfo GetBuildById(string buildId);

        EnergyItemDict GetEnergyItemByCode(string energyItemCode);

        List<BuildViewModel> GetBuildsByUserName(string userName);

        List<EnergyClassify> GetEnergyClassifyValues(string buildId,string date);

        List<EnergyItem> GetEnergyItemValues(string buildId,string date);

        List<HourValue> GetHourValues(string buildId,string date);
        string GetExetendFunc(string buildId);
    }
}
