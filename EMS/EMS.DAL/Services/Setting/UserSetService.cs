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
            List<UserSet> users = context.GetAllUserList();

            UserSetViewModel viewModel = new UserSetViewModel();
            viewModel.UserSets = users;

            return viewModel;
        }

        public UserSetViewModel AddUser(string userName, string password, string userGroupID)
        {
            UserSetViewModel viewModel = new UserSetViewModel();
            viewModel.ResultState = new ResultState();

            List<UserSet> user = context.GetUserByUserName(userName);
            if (user.Count > 0)
            {
                viewModel.ResultState.State = 1;
                viewModel.ResultState.Details = "NG : 用户名：" + userName + " 已存在";
                return viewModel;
            }

            //当密码为空的情况下，默认转化为空密码对应的MD5
            if (string.IsNullOrEmpty(password))
                password = "d41d8cd98f00b204e9800998ecf8427e";

            int result = context.AddUser(userName, password, userGroupID);
            if (result == 1)
            {
                viewModel.ResultState.State = 0;
                viewModel.ResultState.Details = "OK";
            }
            else
            {
                viewModel.ResultState.State = 1;
                viewModel.ResultState.Details = "NG";
            }

            return viewModel;
        }

        public UserSetViewModel UpdateUser(string userName, string password, string oldPassword, string userGroupID)
        {
            UserSetViewModel viewModel = new UserSetViewModel();
            viewModel.ResultState = new ResultState();
            //当密码为空的情况下，默认转化为空字符串对应的MD5
            if (string.IsNullOrEmpty(password))
                password = "d41d8cd98f00b204e9800998ecf8427e";

            int result = context.UpdataUser(userName, password, oldPassword, userGroupID);
            if (result == 1)
            {
                viewModel.ResultState.State = 0;
            }
            else
            {
                viewModel.ResultState.State = 1;
                viewModel.ResultState.Details = "NG ：用户名或密码错误";
            }

            return viewModel;
        }

        public UserSetViewModel DeleteUser(string userName)
        {
            UserSetViewModel viewModel = new UserSetViewModel();
            viewModel.ResultState = new ResultState();
            int result = context.DeleteUser(userName);
            if (result == 1)
            {
                viewModel.ResultState.State = 0;
            }
            else
            {
                viewModel.ResultState.State = 1;
                viewModel.ResultState.Details = "NG ：该用不存在";
            }

            return viewModel;
        }


    }
}
