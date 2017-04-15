using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;

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

		[HttpGet]
		[Route("")]
	    public ActionResult Index()
	    {
		    var c = this.authorLogic.Get(null).Select(a => new AuthorModel() {Id = a.Id, FirstName = a.FirstName, LastName = a.LastName, Shortcut = a.Shortcut, Surname = a.Surname});

		    return View(c);
	    }

		[HttpPost]
		[Route("create")]
	    public ActionResult Create(AuthorModel authorModel)
		{
			Author author = new Author(authorModel.FirstName, authorModel.LastName, authorModel.Surname)
			{
				Shortcut = authorModel.Shortcut,
			};

			this.authorLogic.Create(author);

			return new EmptyResult();

		}

		[HttpGet]
		[Route("create")]
	    public ActionResult Create()
		{
			return View();
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
			Author author = new Author(authorModel.FirstName, authorModel.LastName, authorModel.Surname)
			{
				Id = authorModel.Id,
				Shortcut = authorModel.Shortcut,
			};

			this.authorLogic.Update(author);

			return new EmptyResult();
		}

		[HttpPost]
		[Route("getAll")]
	    public JsonResult GetAll()
	    {
		    return Json(this.authorLogic.Get(null));
	    }

		[HttpGet]
		[Route("delete")]
	    public ActionResult Delete(Guid id)
	    {
			this.authorLogic.Remove(id);

		    return new EmptyResult();
	    }
    }
}