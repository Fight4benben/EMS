using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class BuildSetViewModel
    {
        public List<BuildViewModel> Builds { get; set; }
        public List<BuildInfoSet> BuildInfo { get; set; }
        public ResultState ResultState { get; set; }

    }
}
