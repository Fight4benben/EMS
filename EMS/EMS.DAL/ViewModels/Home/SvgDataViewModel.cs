using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels.Home
{
    public class SvgParamValue
    {
        public string Code { get; set; }
        public decimal Value { get; set; }
  
    }

    public class SvgMeterParam
    {
        public string MeterID { get; set; }
        public List<SvgParamValue> ParamList { get; set; }
    }

    public class SvgDataViewModel
    {
        public List<SvgMeterParam> MeterValueList { get; set; }
    }
}
