using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Snowinmars.Ui.ViewModels.Author;

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
		public ActionResult Create(CreateViewModel authorModel)
		{
			Author author = new Author(authorModel.GivenName.Trim())
			{
				FullMiddleName = authorModel.FullMiddleName?.Trim() ?? string.Empty,
				FamilyName = authorModel.FamilyName?.Trim() ?? string.Empty,
				Shortcut = authorModel.Shortcut?.Trim() ?? string.Empty,
			};

			this.authorLogic.Create(author);

			return new RedirectResult(this.Url.Action("Index", "Author"));
		}

		[HttpGet]
		[Route("create")]
		public ActionResult Create()
		{
			return this.View();
		}

		[HttpGet]
		[Route("delete")]
		public ActionResult Delete(Guid id)
		{
			this.authorLogic.Remove(id);

			return new RedirectResult(this.Url.Action("Index", "Author"));
		}

		[HttpGet]
		[Route("details")]
		public ActionResult Details(Guid id)
		{
			var author = this.authorLogic.Get(id);

			DetailsViewModel authorModel = DetailsViewModel.Map(author);

			return this.View(authorModel);
		}

		[HttpGet]
		[Route("edit")]
		public ActionResult Edit(Guid id)
		{
			Author author = this.authorLogic.Get(id);

			UpdateViewModel authorModel = UpdateViewModel.Map(author);

			return this.View(authorModel);
		}

		[HttpPost]
		[Route("edit")]
		public ActionResult Edit(UpdateViewModel authorModel)
		{
			Pseudonym pseudonym = new Pseudonym()
			{
				GivenName = authorModel.PseudonymGivenName?.Trim() ?? string.Empty,
				FullMiddleName = authorModel.PseudonymFullMiddleName?.Trim() ?? string.Empty,
				FamilyName = authorModel.PseudonymFamilyName?.Trim() ?? string.Empty,
			};

			Author author = new Author(authorModel.GivenName.Trim())
			{
				Id = authorModel.Id,
				FullMiddleName = authorModel.FullMiddleName?.Trim() ?? string.Empty,
				FamilyName = authorModel.FamilyName?.Trim() ?? string.Empty,
				Shortcut = authorModel.Shortcut?.Trim() ?? string.Empty,
				Pseudonym = pseudonym,
			};

			this.authorLogic.Update(author);

			return new RedirectResult(this.Url.Action("Details", "Author", new RouteValueDictionary() { {"id", author.Id} }));
		}

		[HttpPost]
		[Route("getAll")]
		public JsonResult GetAll()
		{
			return this.Json(this.authorLogic.Get(null));
		}

		[HttpGet]
		[Route("")]
		public ActionResult Index()
		{
			IEnumerable<DetailsViewModel> authors = this.authorLogic.Get(null).Select(a => new DetailsViewModel()
			{
				Id = a.Id,
				GivenName = a.GivenName,
				FullMiddleName = a.FullMiddleName,
				Shortcut = a.Shortcut,
				FamilyName = a.FamilyName,
				PseudonymGivenName = a.Pseudonym.GivenName,
				PseudonymFullMiddleName = a.Pseudonym.FullMiddleName,
				PseudonymFamilyName = a.Pseudonym.FamilyName,
			});

			return this.View(authors);
		}
	}
}