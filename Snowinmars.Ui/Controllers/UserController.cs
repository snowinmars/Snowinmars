using Snowinmars.Ui.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snowinmars.Common;
using Snowinmars.Ui.App_Start;
using Snowinmars.UserSlice.UserBll.Interfaces;
using Snowinmars.UserSlice.UserEntites;

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

            //this.shortcutJobName = nameof(ShortcutJob).ToLowerInvariant();
            //this.warningJobName = nameof(WarningJob).ToLowerInvariant();
            //this.emailServiceName = nameof(EmailService).ToLowerInvariant();
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        private bool Authenticate(string username, string password)
        {
            if (this.userLogic.Authenticate(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, createPersistentCookie: true);
	            return true;
            }

	        return false;
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

	        return ControllerHelper.GetSuccessJsonResult(user.Roles.ToString());
        }

        [HttpPost]
        [Route("demote")]
        public JsonResult Demote(string username)
        {
            var user = this.userLogic.Get(username);

            user.Roles = user.Roles.Demote();

            this.userLogic.Update(user);

            return ControllerHelper.GetSuccessJsonResult(user.Roles.ToString());
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

	        ApplicationUser user = ControllerHelper.Map(userModel);

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
        public JsonResult Enter(CreateUserModel userModel)
        {
            bool isUserAlreadyRegistred = string.IsNullOrWhiteSpace(userModel.PasswordConfirm);

            if (!isUserAlreadyRegistred)
            {
                this.Create(userModel);
            }

            var result = this.Authenticate(userModel.Username, userModel.Password);

	        if (result)
	        {
		        return ControllerHelper.GetSuccessJsonResult(new
		        {
			        Redirect = this.Url.Action("Index", "Home"),
		        });
	        }

			return ControllerHelper.GetFailJsonResult();
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            IEnumerable<ApplicationUser> users = this.userLogic.Get(u => true);

            IEnumerable<UpdateUserModel> userModels = ControllerHelper.Map(users);

            return this.View(userModels);
        }

        [HttpPost]
        [Route("isUsernameExist")]
        [AllowAnonymous]
        public JsonResult IsUsernameExist(string username)
        {
            return ControllerHelper.GetSuccessJsonResult(this.userLogic.IsUsernameExist(username));
        }

        [HttpGet]
        [Route("rootPage")]
        public ActionResult RootPage()
        {
            //var isShortcutJobSmtpReady = QuartzCron.ShortcutJob?.IsSmtpReady ?? false;
            //var isWarningJobSmtpReady = QuartzCron.WarningJob?.IsSmtpReady ?? false;

            GetSystemSettings systemSettings = new GetSystemSettings
            {
                IsShortcutJobSmtpServerReady = false,
                IsWarningJobSmtpServerReady = false,
            };

            return this.View(systemSettings);
        }

        //private readonly string shortcutJobName;
        //private readonly string emailServiceName;
        //private readonly string warningJobName;

        //[HttpPost]
        //[Route("setSmtpEntropies")]
        //public JsonResult SetSmtpEntropies(string jobName, string entropy)
        //{
            //jobName = jobName.ToLowerInvariant();

            //if (jobName == this.shortcutJobName)
            //{
            //    return ControllerHelper.GetSuccessJsonResult(this.Login(QuartzCron.ShortcutJob, entropy));
            //}

            //if (jobName == this.warningJobName)
            //{
            //    return ControllerHelper.GetSuccessJsonResult(this.Login(QuartzCron.WarningJob, entropy));
            //}

            //if (jobName == this.emailServiceName)
            //{
            //    EmailService.TryLogin(entropy);

            //    if (EmailService.IsSmtpReady)
            //    {
            //        return ControllerHelper.GetSuccessJsonResult(true);
            //    }
            //    else
            //    {
            //        return ControllerHelper.GetFailJsonResult();
            //    }
            //}

            //return ControllerHelper.GetFailJsonResult();
        //}

        //private bool Login<T>(Cron<T> cronService, string entropy)
        //{
        //    cronService.TryLogin(entropy);
        //    return cronService.IsSmtpReady;
        //}

        [HttpPost]
        [Route("update")]
        public ActionResult Update(UpdateUserModel model)
        {
            try
            {
	            ApplicationUser user = ControllerHelper.Map(model);
                this.userLogic.Update(user);

                var cookie = new HttpCookie("lang", user.Language.ToString());
                this.HttpContext.Response.SetCookie(cookie);
            }
            catch
            {
                this.Response.StatusCode = 500;
                return ControllerHelper.GetFailJsonResult();
            }

            return ControllerHelper.GetSuccessJsonResult();
        }
    }
}