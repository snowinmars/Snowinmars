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

		[HttpPost]
		[Route("create")]
	    public RedirectResult Create(UserModel userModel)
	    {
		    if (userModel.Roles == UserRoles.None)
		    {
			    userModel.Roles = UserRoles.User;
		    }

		    User user = Map(userModel);

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

		    if (this.userLogic.Authenticate(candidate))
		    {
				FormsAuthentication.SetAuthCookie(candidate.Username, createPersistentCookie: true);
			    return Redirect(Url.Action("Index", "Home"));
		    }

			throw new Exception("Can't login");
	    }

		private User Map(UserModel userModel)
	    {
		    return new User(userModel.Username)
		    {
			    Id = userModel.Id,
			    Email = userModel.Email,
			    PasswordHash = this.userLogic.CalculateHash(userModel.Password),
				Roles = userModel.Roles,
			};
	    }
    }
}