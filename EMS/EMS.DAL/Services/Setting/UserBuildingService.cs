using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services
{
    public class UserBuildingService
    {
        private IUserBuildingDbContext context;

        public UserBuildingService()
        {
            context = new UserBuildingDbContext();
        }

        public UserBuildingViewModel GetViewModel()
        {
            List<User> users = context.GetUsers();
            string userName;
            if (users.Count > 0)
                userName = users[0].UserName;
            else
                userName = "";
            UserBuildingViewModel userBuildingViewModel = new UserBuildingViewModel();
            userBuildingViewModel.Users = users;
            userBuildingViewModel.UserBuildings = context.GetUserBuildings(userName);

            return userBuildingViewModel;
        }

        public UserBuildingViewModel GetViewModel(string userName)
        {
            
            UserBuildingViewModel userBuildingViewModel = new UserBuildingViewModel();
            userBuildingViewModel.UserBuildings = context.GetUserBuildings(userName);

            return userBuildingViewModel;
        }

        public int AddBuild(string userName,string buildId)
        {
            return context.AddBuild(userName,buildId);
        }

        public int DeleteBuild(string userName, string buildId)
        {
            return context.DeleteBuild(userName,buildId);
        }


    }
}
