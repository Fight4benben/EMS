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


        public UserMenuViewModel GetUserMenuViewModel()
        {
            List<UserSet> users = context.GetAllUserList();
            List<UserMenus> adminMenu = context.GetAdminMenu();
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
                    user2m.MenuID = menuId;
                    user2m.MenuName = menuInfo.Find(x => x.MenuID.Equals(menuId)).MenuName;
                    user2m.isUsing = true;
                    admin2Menu.Add(user2m);
                }
            }

            //其他用户关联的菜单

            UserMenuViewModel viewModel = new UserMenuViewModel();
            viewModel.Users = users;
            viewModel.adminMenu = admin2Menu;

            return viewModel;
        }

        public UserMenuViewModel GetUserMenuViewModel(int userID)
        {
            List<UserSet> users = context.GetAllUserList();
            List<UserMenus> adminMenu = context.GetAdminMenu();
            List<UserMenus> userMenu = context.GetOneUserMenu(userID);
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
                    user2m.MenuID = menuId;
                    user2m.MenuName = menuInfo.Find(x => x.MenuID.Equals(menuId)).MenuName;
                    user2m.isUsing = true;
                    admin2Menu.Add(user2m);
                }
            }

            //其他用户关联的菜单
            UserMenus userM = new UserMenus();
            if (userMenu.Count > 0)
            {
                userM = userMenu.First();
            }

            List<User2Menu> user2Menu = new List<User2Menu>();

            if (userM.Menus.Equals("all"))
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
                string[] userMenuArray = userM.Menus.Split('|');
                foreach (string menuId in userMenuArray)
                {
                    User2Menu user2m = new User2Menu();
                    user2m.MenuID = menuId;
                    user2m.MenuName = menuInfo.Find(x => x.MenuID.Equals(menuId)).MenuName;
                    user2m.isUsing = true;
                    user2Menu.Add(user2m);
                }
            }


            UserMenuViewModel viewModel = new UserMenuViewModel();
            viewModel.Users = users;
            viewModel.adminMenu = admin2Menu;
            viewModel.userMenu = user2Menu;

            return viewModel;
        }

        public UserMenuViewModel SetUserMenu(int userID, string menuIDs)
        {
            ResultState resultState = new ResultState();

            int result = context.SetUserMenu(userID, menuIDs);

            if (result > 0)
            {
                resultState.State = 0;
            }
            else
            {
                resultState.State = 1;
                resultState.Details = "SetUserMenu 失败";
            }

            UserMenuViewModel viewModel = new UserMenuViewModel();
            viewModel.ResultState = resultState;

            return viewModel;
        }
    }
}
