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
    [Authorize]
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
		    var author = new Author(authorModel.Shortcut)
		    {
		        GivenName = ControllerHelper.Convert(authorModel.GivenName),
		        FullMiddleName = ControllerHelper.Convert(authorModel.FullMiddleName),
		        FamilyName = ControllerHelper.Convert(authorModel.FamilyName),
                IsSynchronized = authorModel.IsSynchronized,
                MustInformAboutWarnings = authorModel.MustInformAboutWarnings,
                Pseudonym = MapPseudonym(authorModel),
            };

		    if (authorModel.Id != Guid.Empty)
		    {
		        author.Id = authorModel.Id;
		    }

		    return author;
		}

	    private static Pseudonym MapPseudonym(AuthorModel authorModel)
	    {
	        return new Pseudonym
	        {
	            GivenName = authorModel.PseudonymGivenName,
	            FullMiddleName = authorModel.PseudonymFullMiddleName,
	            FamilyName = authorModel.PseudonymFamilyName,
	        };
	    }


	    [HttpGet]
		[Route("create")]
		public ActionResult Create()
		{
			return View(AuthorModel.Emtpy);
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public JsonResult GetAll()
		{
			return Json(this.authorLogic.Get(null));
		}

		[HttpGet]
		[Route("")]
        [AllowAnonymous]
        public ActionResult Index()
		{
			var c = this.authorLogic.Get(null).Select(a => new AuthorModel() { Id = a.Id, GivenName = a.GivenName, FullMiddleName = a.FullMiddleName, Shortcut = a.Shortcut, FamilyName = a.FamilyName });

			return View(c);
		}
	}
}