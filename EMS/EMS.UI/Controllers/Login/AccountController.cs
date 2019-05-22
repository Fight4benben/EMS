using EMS.DAL.RepositoryImp;
using EMS.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EMS.UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (model.Password == null)
                model.Password = "";

            if (!ModelState.IsValid)
            {
                return View(new LoginViewModel());
            }
            var result = false;

            result = new UserContext().MatchUser(model.UserName,model.Password);

            //var result = FormsAuthentication.Authenticate(model.UserName,model.Password);
            //var result = _authProvider.Auth(model.UserName, model.Password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "账号或用户名有误");
            return View(new LoginViewModel());
        }

        public ActionResult Logout()
        {
            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}