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
    public class UserMenuService
    {
        private UserMenuDbContext context;

        public UserMenuService()
        {
            context = new UserMenuDbContext();
        }

        /// <summary>
        /// 获取管理员所关联菜单
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserMenuViewModel GetAdminMenuViewModel(string userName)
        {
            List<UserSet> users = context.GetAllUserList();
            List<UserMenus> adminMenu = context.GetAdminMenuByUserName(userName);
            List<MenuInfo> menuInfo = context.GetMenuInfo();

            //管理员所关联菜单
            UserMenus admin = adminMenu.First();
            List<User2Menu> admin2Menu = new List<User2Menu>();

            if (admin.Menus.Equals("all"))
            {
                foreach (var menuIetm in menuInfo)
                {
                    User2Menu user2m = new User2Menu();
                    user2m.MenuID = menuIetm.MenuID;
                    user2m.MenuName = menuIetm.MenuName;
                    user2m.isUsing = true;
                    admin2Menu.Add(user2m);
                }

            }
            else
            {
                string[] adminMenuArray = admin.Menus.Split('|');
                foreach (var menuId in adminMenuArray)
                {
                    User2Menu user2m = new User2Menu();
                    user2m.MenuName = menuInfo.Find(x => x.MenuID.Equals(menuId)).MenuName;
                    user2m.MenuID = menuId;
                    user2m.isUsing = true;
                    admin2Menu.Add(user2m);
                }
            }

            UserMenuViewModel viewModel = new UserMenuViewModel();
            viewModel.Users = users;
            viewModel.adminMenu = admin2Menu;

            return viewModel;
        }

        /// <summary>
        /// 获取其他用户关联的菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserMenuViewModel GetUserMenuViewModel(int userID)
        {
            List<UserMenus> userMenu = context.GetOneUserMenu(userID);

            //其他用户关联的菜单
            UserMenus userM = new UserMenus();
            List<User2Menu> user2Menu = new List<User2Menu>();
            string[] userMenuArray= { };
            if (userMenu.Count > 0)
            {
                userM = userMenu.First();

                if (userM.Menus.Equals("all"))
                {
                    userMenuArray[0] = "all" ;
                }
                else
                {
                    userMenuArray = userM.Menus.Split('|');
                }
            }

            UserMenuViewModel viewModel = new UserMenuViewModel();
            viewModel.userMenu = userMenuArray;

            return viewModel;
        }

        public UserMenuViewModel SetUserMenu(int userID, string menuIDs)
        {
            ResultState resultState = new ResultState();

            int result = context.SetUserMenu(userID, menuIDs);

            if (result > 0)
            {
                resultState.State = 0;
                resultState.Details = "成功";
            }
            else
            {
                resultState.State = 1;
                resultState.Details = "失败";
            }

            UserMenuViewModel viewModel = new UserMenuViewModel();
            viewModel.ResultState = resultState;

            return viewModel;
        }
    }
}
