using EMS.DAL.ViewModels;
using EMS.DAL.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository.Home
{
    public interface ISvgDbContext
    {
        List<BuildViewModel> GetBuildsByUserName(string userName);
        List<SvgInfo> GetSvgListByBuildId(string buildId);
        string GetSvgViewById(string svgId);
        SvgDataViewModel GetDataViewModel(string buildId,string svgId);
    }
}
