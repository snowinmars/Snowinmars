using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Snowinmars.Ui.Controllers
{
	[Route("author")]
	public class AuthorController : Controller
	{
		private readonly IAuthorLogic authorLogic;

		public AuthorController(IAuthorLogic authorLogic)
		{
			this.authorLogic = authorLogic;
		}

		[HttpPost]
		[Route("create")]
		public ActionResult Create(AuthorModel authorModel)
		{
			Author author = Map(authorModel);

			this.authorLogic.Create(author);

			return new RedirectResult(Url.Action("Index", "Author"));
		}

		private static Author Map(AuthorModel authorModel)
		{
			return new Author(authorModel.Shortcut)
			{
				GivenName = authorModel.GivenName,
				FullMiddleName = authorModel.FullMiddleName,
				FamilyName = authorModel.FamilyName,
				Id = authorModel.Id,
			};
		}

		[HttpGet]
		[Route("create")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpGet]
		[Route("delete")]
		public ActionResult Delete(Guid id)
		{
			this.authorLogic.Remove(id);

			return new RedirectResult(Url.Action("Index", "Author"));
		}

		[HttpGet]
		[Route("details")]
		public ActionResult Details(Guid id)
		{
			var author = this.authorLogic.Get(id);

			AuthorModel authorModel = AuthorModel.Map(author);

			return View(authorModel);
		}

		[HttpGet]
		[Route("edit")]
		public ActionResult Edit(Guid id)
		{
			Author author = this.authorLogic.Get(id);

			AuthorModel authorModel = AuthorModel.Map(author);

			return View(authorModel);
		}

		[HttpPost]
		[Route("edit")]
		public ActionResult Edit(AuthorModel authorModel)
		{
			Author author = Map(authorModel);

			this.authorLogic.Update(author);

			return new RedirectResult(Url.Action("Details", "Author", new RouteValueDictionary() { { "id", author.Id } }));
		}

		[HttpPost]
		[Route("getAll")]
		public JsonResult GetAll()
		{
			return Json(this.authorLogic.Get(null));
		}

		[HttpGet]
		[Route("")]
		public ActionResult Index()
		{
			var c = this.authorLogic.Get(null).Select(a => new AuthorModel() { Id = a.Id, GivenName = a.GivenName, FullMiddleName = a.FullMiddleName, Shortcut = a.Shortcut, FamilyName = a.FamilyName });

			return View(c);
		}
	}
}