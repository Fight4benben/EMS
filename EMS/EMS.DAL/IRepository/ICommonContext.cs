using EMS.DAL.Entities;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface ICommonContext
    {
        BuildExtendInfo GetExtendInfoByBuildId(string buildId);
        List<BuildViewModel> GetBuildsByUserName(string userName);
        List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId);
    }
}
