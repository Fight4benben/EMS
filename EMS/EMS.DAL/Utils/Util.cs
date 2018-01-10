using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Utils
{
    public class Util
    {
        public static  DateTime ConvertString2DateTime(string date,string formatPattern)
        {
            DateTime dt;
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern= formatPattern;
            return dt = Convert.ToDateTime(date,dtFormat);
        }
    }
}
