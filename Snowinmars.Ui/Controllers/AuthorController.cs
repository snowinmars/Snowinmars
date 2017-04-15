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

	    public ActionResult Index()
	    {
		    var c = this.authorLogic.Get(null);

		    return View(c);
	    }

		[HttpPost]
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
	    public ActionResult Create()
		{
			return View();
		}

		public ActionResult Details(Guid id)
		{
			var author = this.authorLogic.Get(id);

			AuthorModel authorModel = AuthorModel.Map(author);

			return View(authorModel);
		}

	    public ActionResult Edit(string s)
	    {
		    throw new NotImplementedException();
	    }

		[Route("getAll")]
		[HttpPost]
	    public JsonResult GetAll()
	    {
		    return Json(this.authorLogic.Get(null));
	    }
    }
}