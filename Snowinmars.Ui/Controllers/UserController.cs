using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;

namespace Snowinmars.Ui.Controllers
{
    public class UserController : Controller
    {
	    private readonly IUserLogic userLogic;

		public UserController(IUserLogic userLogic1)
		{
			this.userLogic = userLogic1;
		}

	    [HttpGet]
	    [Route("")]
	    public ActionResult Index()
	    {
		    var user = this.userLogic.Get(User.Identity.Name);
			UserModel userModel = UserModel.Map(user);

		    return View(userModel);
	    }

		[HttpPost]
		[Route("create")]
	    public RedirectResult Create(UserModel userModel)
	    {
		    if (userModel.Roles == UserRoles.None)
		    {
			    userModel.Roles = UserRoles.User;
		    }

		    User user = Map(userModel);

			this.userLogic.SetupCryptography(user);

			user.PasswordHash = this.userLogic.CalculateHash(userModel.Password, user.Salt);

			this.userLogic.Create(user);

			return new RedirectResult(Url.Action("Index", "Home"));
	    }

		[HttpPost]
		[Route("isUsernameExist")]
	    public JsonResult IsUsernameExist(string username)
	    {
		    return Json(this.userLogic.IsUsernameExist(username));
	    }

		[HttpPost]
		[Route("authenticate")]
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
				Email = userModel.Email,
				Roles = userModel.Roles,
			};

			if (userModel.Id != Guid.Empty)
			{
				user.Id = userModel.Id;
			}

			return user;
		}

		[HttpPost]
		[Route("enter")]
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
    }
}