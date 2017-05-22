﻿using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snowinmars.Ui.AppStartHelpers;

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

            shortcutJobName = nameof(ShortcutJob).ToLowerInvariant();
            warningJobName = nameof(WarningJob).ToLowerInvariant();
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        private ActionResult Authenticate(string username, string password)
        {
            if (this.userLogic.Authenticate(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, createPersistentCookie: true);
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

            user.Roles = user.Roles == UserRoles.Banned ? UserRoles.User : UserRoles.Banned;

            this.userLogic.Update(user);

            return new RedirectResult(this.Url.Action("Index", "User"));
        }

        [HttpPost]
        [Route("promote")]
        public JsonResult Promote(string username)
        {
            var user = this.userLogic.Get(username);

            user.Roles = user.Roles.Promote();

            this.userLogic.Update(user);

            return Json(user.Roles.ToString());
        }

        [HttpPost]
        [Route("demote")]
        public JsonResult Demote(string username)
        {
            var user = this.userLogic.Get(username);

            user.Roles = user.Roles.Demote();

            this.userLogic.Update(user);

            return Json(user.Roles.ToString());
        }

        [HttpGet]
        [Route("ban")]
        [ActionName("BanByUsername")]
        public RedirectResult Ban(string username)
        {
            var user = this.userLogic.Get(username);

            user.Roles = user.Roles == UserRoles.Banned ? UserRoles.User : UserRoles.Banned;

            this.userLogic.Update(user);

            return new RedirectResult(this.Url.Action("Index", "User"));
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        private RedirectResult Create(CreateUserModel userModel)
        {
            if (userModel.Roles == default(UserRoles))
            {
                userModel.Roles = UserRoles.User;
            }

            User user = ControllerHelper.Map(userModel);

            this.userLogic.WriteCryptographicData(userModel.Password, user);
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
            var user = this.userLogic.Get(this.User.Identity.Name);

            UpdateUserModel userModel = ControllerHelper.Map(user);

            return this.View(userModel);
        }

        [HttpPost]
        [Route("enter")]
        [AllowAnonymous]
        public RedirectResult Enter(CreateUserModel userModel)
        {
            bool isUserAlreadyRegistred = string.IsNullOrWhiteSpace(userModel.PasswordConfirm);

            if (!isUserAlreadyRegistred)
            {
                this.Create(userModel);
            }

            this.Authenticate(userModel.Username, userModel.Password);

            return new RedirectResult(this.Url.Action("Index", "Home"));
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            IEnumerable<User> users = this.userLogic.Get(u => true);

            IEnumerable<UpdateUserModel> userModels = ControllerHelper.Map(users);

            return this.View(userModels);
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
            var isShortcutJobSmtpReady = QuartzCron.ShortcutJob?.IsSmtpReady ?? false;
            var isWarningJobSmtpReady = QuartzCron.WarningJob?.IsSmtpReady ?? false;

            GetSystemSettings systemSettings = new GetSystemSettings
            {
                IsShortcutJobSmtpServerReady = isShortcutJobSmtpReady,
                IsWarningJobSmtpServerReady = isWarningJobSmtpReady,
            };

            return this.View(systemSettings);
        }

        private string shortcutJobName;
        private string warningJobName;

        [HttpPost]
        [Route("setSmtpEntropies")]
        public JsonResult SetSmtpEntropies(string jobName, string entropy)
        {
            jobName = jobName.ToLowerInvariant();

            if (jobName == shortcutJobName)
            {
                return ControllerHelper.GetSuccessJsonResult(Login(QuartzCron.ShortcutJob, entropy));
            }

            if (jobName == warningJobName)
            {
                return ControllerHelper.GetSuccessJsonResult(Login(QuartzCron.WarningJob, entropy));
            }

            return ControllerHelper.GetFailJsonResult();
        }

        private bool Login<T>(Cron<T> cronService, string entropy)
        {
            cronService.TryLogin(entropy);
            return cronService.IsSmtpReady;
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update(UpdateUserModel model)
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