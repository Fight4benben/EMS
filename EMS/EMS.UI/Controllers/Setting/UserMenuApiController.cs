using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class UserMenuApiController : ApiController
    {
        public UserMenuService service = new UserMenuService();

        /// <summary>
        /// 默认或者管理员关联的菜单
        /// </summary>
        /// <returns>返回：系统中所以用户；当前登录管理员关联菜单</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetAdminMenuViewModel(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据用户ID获取用户的菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>返回：传入用户的菜单ID数组</returns>
        public object Get(string userID)
        {
            try
            {
                return service.GetUserMenuViewModel(Convert.ToInt32(userID));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 设置用户关联的菜单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>返回：State数值，0代表操作成功；1代表操作失败</returns>
        [HttpPost]
        public object SetUserMenu([FromBody] JObject obj)
        {
            try
            {
                string userID = obj["userID"].ToString();
                string menuIDs = obj["menuIDs"].ToString();

                return service.SetUserMenu(Convert.ToInt32(userID), menuIDs);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }

    }
}