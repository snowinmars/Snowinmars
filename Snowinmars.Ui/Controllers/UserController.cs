﻿using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;
using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Snowinmars.Ui.Controllers
{
    [Authorize]
    [Internationalization]
    public class UserController : Controller
    {
        private readonly IUserLogic userLogic;

        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public ActionResult Authenticate(UserModel userModel)
        {
            User candidate = ControllerHelper.Map(userModel);

            if (this.userLogic.Authenticate(candidate, userModel.Password))
            {
                FormsAuthentication.SetAuthCookie(candidate.Username, createPersistentCookie: true);
                return this.Redirect(this.Url.Action("Index", "Home"));
            }

            throw new Exception("Can't login");
        }

        [HttpGet]
        [Route("ban")]
        [ActionName("BanById")]
        public RedirectResult Ban(Guid id)
        {
            var user = this.userLogic.Get(id);

            user.Roles = UserRoles.Banned;

            this.userLogic.Update(user);

            return new RedirectResult(this.Url.Action("Index", "User"));
        }

        [HttpGet]
        [Route("ban")]
        [ActionName("BanByUsername")]
        public RedirectResult Ban(string username)
        {
            var user = this.userLogic.Get(username);
            user.Roles = UserRoles.Banned;
            this.userLogic.Update(user);

            return new RedirectResult(this.Url.Action("Index", "User"));
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public RedirectResult Create(UserModel userModel)
        {
            if (userModel.Roles == UserRoles.Banned)
            {
                userModel.Roles = UserRoles.User;
            }

            User user = ControllerHelper.Map(userModel);

            this.userLogic.SetupCryptography(user);

            user.PasswordHash = this.userLogic.CalculateHash(userModel.Password, user.Salt);

            this.userLogic.Create(user);

            return new RedirectResult(this.Url.Action("Index", "Home"));
        }

        [HttpGet]
        [Route("deauthenticate")]
        public RedirectResult Deauthenticate()
        {
            FormsAuthentication.SignOut();

            return new RedirectResult(this.Url.Action("Index", "Home"));
        }

        [HttpGet]
        [Route("delete")]
        [ActionName("DeleteById")]
        public RedirectResult Delete(Guid id)
        {
            this.userLogic.Remove(id);

            return new RedirectResult(this.Url.Action("Index", "User"));
        }

        [HttpGet]
        [Route("delete")]
        [ActionName("DeleteByUsername")]
        public RedirectResult Delete(string username)
        {
            this.userLogic.Remove(username);

            return new RedirectResult(this.Url.Action("Index", "User"));
        }

        [HttpGet]
        [Route("details")]
        [AllowAnonymous]
        public ActionResult Details()
        {
            var a = CultureInfo.CurrentCulture;
            var user = this.userLogic.Get(this.User.Identity.Name);
            UserModel userModel = ControllerHelper.Map(user);

            return this.View(userModel);
        }

        [HttpPost]
        [Route("enter")]
        [AllowAnonymous]
        public RedirectResult Enter(UserModel userModel)
        {
            if (string.IsNullOrWhiteSpace(userModel.PasswordConfirm))
            {
                this.Authenticate(userModel);
            }
            else
            {
                this.Create(userModel);
                this.Authenticate(userModel);
            }

            return new RedirectResult(this.Url.Action("Index", "Home"));
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var users = this.userLogic.Get(u => true);

            return this.View(users);
        }

        [HttpPost]
        [Route("isUsernameExist")]
        [AllowAnonymous]
        public JsonResult IsUsernameExist(string username)
        {
            return this.Json(this.userLogic.IsUsernameExist(username));
        }

        [HttpGet]
        [Route("rootPage")]
        public ActionResult RootPage()
        {
            return this.View();
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update(UserModel model)
        {
            try
            {
                User user = ControllerHelper.Map(model);
                this.userLogic.Update(user);

                var cookie = new HttpCookie("lang", user.Language.ToString());
                this.HttpContext.Response.SetCookie(cookie);
            }
            catch
            {
                this.Response.StatusCode = 500;
                return this.Json(new { success = false });
            }

            return this.Json(true);
        }
    }
}