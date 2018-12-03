using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    public class UserMenuApiController : ApiController
    {
        public UserMenuService service = new UserMenuService();

        /// <summary>
        /// 默认或者管理员关联的菜单
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            try
            {
                return service.GetUserMenuViewModel();
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
        /// <returns></returns>
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
        /// <returns></returns>
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