using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels.Home
{
    public class SvgInfo
    {
        public string SvgID { get; set; }
        public string SvgName { get; set; }
    }

    public class SvgBinding
    {
        public string SvgID { get; set; }
        public string Meters { get; set; }
        public string ParamStrings { get; set; }
    }

    public class SvgTempValue
    {
        public string MeterId { get; set; }
        public string ParamName { get; set; }
        public decimal Value { get; set; }
    }

    public class SvgViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<SvgInfo> Svgs { get; set; }
        public string SvgView { get; set; }
        public SvgDataViewModel Data { get; set; }
    }
}
