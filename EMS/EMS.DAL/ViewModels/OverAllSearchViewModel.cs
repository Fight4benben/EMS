using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class OverAllSearchViewModel
    {
        public List<EMSValue> Last31Day { get; set; }
        public List<EMSValue> MonthDate { get; set; }
        public List<CompareData> MomData { get; set; }
        public List<CompareData> CompareData { get; set; }
    }
}
