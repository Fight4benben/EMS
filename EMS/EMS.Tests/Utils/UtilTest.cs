using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EMS.Tests.Utils
{
    public class UtilTest
    {
        public static string GetJson(object o)
        {
            //格式化json
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(o, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
    }
}
