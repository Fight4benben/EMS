using EMS.DAL.Entities.Setting;
using EMS.DAL.ViewModels.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository.Setting
{
    public interface ISvgSettingContext
    {
        List<SvgViewModel> GetSvgViewModels(string buildId);

        Svg GetSvgById(string svgId);

        SvgBinding GetSvgBindingBySvgId(string svgId);

        int Insert(Svg svg);

        int Update(Svg svg);

        int Delete(string svgId);

        List<Identify> GetMeters(string buildId);
        List<Identify> GetParams(string buildId);

        int InsertSvgBinding(SvgBinding binding);
        int UpdateSvgBinding(SvgBinding binding);
    }
}
