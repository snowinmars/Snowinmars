using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;

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

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            if (!User.IsInRole(UserRoles.Root.ToString()))
            {
                throw new UnauthorizedAccessException("You are not root");
            }

            var users = this.userLogic.Get(u => true);

            return View(users);
        }

	    [HttpGet]
	    [Route("details")]
        [AllowAnonymous]
	    public ActionResult Details()
	    {
	        var a = CultureInfo.CurrentCulture;
            var user = this.userLogic.Get(User.Identity.Name);
			UserModel userModel = UserModel.Map(user);

		    return View(userModel);
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

		    User user = Map(userModel);

			this.userLogic.SetupCryptography(user);

			user.PasswordHash = this.userLogic.CalculateHash(userModel.Password, user.Salt);

			this.userLogic.Create(user);

			return new RedirectResult(Url.Action("Index", "Home"));
	    }

	  //  [HttpPost]
	  //  [Route("setEmail")]
	  //  public JsonResult SetEmail(string email)
	  //  {
		 //   var user = this.userLogic.Get(User.Identity.Name);

		 //   user.Email = email;

			//this.userLogic.Update(user);

		 //   return Json(true);
	  //  }

        [HttpPost]
        [Route("isUsernameExist")]
        [AllowAnonymous]
        public JsonResult IsUsernameExist(string username)
        {
            return Json(this.userLogic.IsUsernameExist(username));
        }

        [HttpGet]
        [Route("rootPage")]
        public ActionResult RootPage()
        {
            return View();
        }

        [HttpPost]
		[Route("authenticate")]
        [AllowAnonymous]
	    public ActionResult Authenticate(UserModel userModel)
        {
		    User candidate = Map(userModel);
			
		    if (this.userLogic.Authenticate(candidate, userModel.Password))
		    {
				FormsAuthentication.SetAuthCookie(candidate.Username, createPersistentCookie: true);
			    return Redirect(Url.Action("Index", "Home"));
		    }

			throw new Exception("Can't login");
	    }

	    [HttpGet]
	    [Route("deauthenticate")]
	    public RedirectResult Deauthenticate()
	    {
		    FormsAuthentication.SignOut();

			return new RedirectResult(Url.Action("Index", "Home"));
	    }

        private User Map(UserModel userModel)
		{
			var user = new User(userModel.Username)
			{
				Email = ControllerHelper.Convert(userModel.Email),
				Roles = userModel.Roles,
                Language = userModel.Language,
			};

			if (userModel.Id != Guid.Empty)
			{
				user.Id = userModel.Id;
			}

			return user;
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

			return new RedirectResult(Url.Action("Index", "Home"));
	    }

        [HttpGet]
        [Route("delete")]
        [ActionName("DeleteById")]
        public RedirectResult Delete(Guid id)
        {
            this.userLogic.Remove(id);

			return new RedirectResult(Url.Action("Index", "User"));
        }

        [HttpGet]
        [Route("delete")]
        [ActionName("DeleteByUsername")]
        public RedirectResult Delete(string username)
        {
            this.userLogic.Remove(username);

            return new RedirectResult(Url.Action("Index", "User"));
        }

        [HttpGet]
        [Route("ban")]
        [ActionName("BanById")]
        public RedirectResult Ban(Guid id)
        {
            var user = this.userLogic.Get(id);
            user.Roles = UserRoles.Banned;
            this.userLogic.Update(user);

            return new RedirectResult(Url.Action("Index", "User"));
        }

        [HttpGet]
        [Route("ban")]
        [ActionName("BanByUsername")]
        public RedirectResult Ban(string username)
        {
            var user = this.userLogic.Get(username);
            user.Roles = UserRoles.Banned;
            this.userLogic.Update(user);

            return new RedirectResult(Url.Action("Index", "User"));
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update(UserModel model)
        {
            try
            {
                User user = Map(model);
                this.userLogic.Update(user);

                var cookie = new HttpCookie("lang", user.Language.ToString());
                HttpContext.Response.SetCookie(cookie);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { success = false });
            }

            return Json(true);
        }
    }
}