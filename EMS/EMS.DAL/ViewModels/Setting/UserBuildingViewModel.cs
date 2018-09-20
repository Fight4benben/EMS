using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class UserBuildingViewModel
    {
        public List<User> Users{ get; set; }
        public List<UserBuilding> UserBuildings { get; set; }
    }
}
