using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class UserSetService
    {
        private UserSetDbContext context;

        public UserSetService()
        {
            context = new UserSetDbContext();
        }

        public UserSetViewModel GetAllUserViewModel()
        {
            //List<UserSet> Users = context.GetUserList();
            //List<UserBuild> UserBuilds = context.GetUserBuildList();

            UserSetViewModel viewModel = new UserSetViewModel();
            //viewModel.UserSets = Users;
            //viewModel.UserBuilds = UserBuilds;
            return viewModel;
        }
    }
}
