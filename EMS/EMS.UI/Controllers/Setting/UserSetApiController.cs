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
    public class UserSetApiController : ApiController
    {
        public UserSetService service = new UserSetService();


        public object Get()
        {
            try
            {
                return service.GetAllUserViewModel();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        [HttpPost]
        public object AddUser([FromBody] JObject obj)
        {
            try
            {
                string userName = obj["userName"].ToString();
                string password = obj["password"].ToString();
                string userGroupID = obj["userGroupID"].ToString();
                return service.AddUser(userName, password, userGroupID);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }

        [HttpPut]
        public object UpdataUser([FromBody] JObject obj)
        {
            try
            {
                string userID = obj["userID"].ToString();
                string userName = obj["userName"].ToString();
                string password = obj["password"].ToString();
                string repassword = obj["password"].ToString();
                string oldPassword = obj["oldPassword"].ToString();
                string userGroupID = obj["userGroupID"].ToString();
                return service.UpdateUser(userID, userName, password, repassword, oldPassword, userGroupID);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }

        [HttpDelete]
        public object DeleteUser([FromBody] JObject obj)
        {
            try
            {
                string userName = obj["userName"].ToString();
                return service.DeleteUser(userName);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }
    }
}