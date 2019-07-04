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
    public class UpdateUserPasswordController : ApiController
    {
        public UserSetService service = new UserSetService();


        [HttpPost]
        public object UpdataPassword([FromBody] JObject obj)
        {
            try
            {
                string userName = User.Identity.Name;
                string password = obj["password"].ToString();
                string oldPassword = obj["oldPassword"].ToString();

                return service.UpdateUserSelf(userName, password, oldPassword);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}