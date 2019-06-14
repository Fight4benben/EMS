using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels.Setting
{
    public class SvgSettingViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<SvgViewModel> Svgs { get; set; }
    }

    public class SvgViewModel
    {
        public string BuildId { get; set; }
        public string SvgId { get; set; }
        public string SvgName { get; set; }
        public string Path { get; set; }
    }

    public class SvgBindingViewModel
    {
        public List<Identify> MeterList { get; set; }
        public List<Identify> ParamList { get; set; }
        public List<string> SelectedMeters { get; set; }
        public List<string> SelectedParams { get; set; }
    }

    public class Identify
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SvgBindingPost
    {
        public string SvgId { get; set; }
        public string PMeters { get; set; }
        public string PParams { get; set; }
    }
}
