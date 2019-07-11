using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class MeterAlarmInfo
    {
        public int ID { get; set; }
        public string BuildID { get; set; }
        public string BuildName { get; set; }
        public string MeterID { get; set; }
        public string MeterName { get; set; }
        public string CollectionName { get; set; }
        public string MeterParamName { get; set; }
        public string ParamUnit { get; set; }
        public string NormalRange { get; set; }
        public int TypeCode { get; set; }
        public string TypeName { get; set; }
        public decimal SetValue { get; set; }
        public decimal AlarmValue { get; set; }
        public DateTime AlarmTime { get; set; }
        public int IsConfirm { get; set; }

    }
}
