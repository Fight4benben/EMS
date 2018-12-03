using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class UserMenuViewModel
    {
        public List<UserSet> Users { get; set; }
        public List<User2Menu> adminMenu { get; set; }
        public List<User2Menu> userMenu { get; set; }
        public ResultState ResultState { get; set; }

    }
}
