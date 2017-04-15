using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Common;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;

namespace Snowinmars.Ui.Controllers
{
    public class BookController : Controller
    {
	    private readonly IBookLogic bookLogic;

	    public BookController(IBookLogic bookLogic)
	    {
		    this.bookLogic = bookLogic;
	    }

        public ActionResult Index()
        {
	        var c = this.bookLogic.Get(book => book.Authors.Any(a => a.FirstName == "nnn"));
			
            return View(c);
        }

		[HttpGet]
	    public ActionResult Create()
	    {
            return View();
		}

	    [HttpPost]
	    public ActionResult Create(BookModel bookModel)
	    {
		    Book book = new Book(bookModel.Title, bookModel.PageCount)
		    {
			    Year = bookModel.Year,
		    };

		    if (bookModel.AuthorModels != null &&
		        bookModel.AuthorModels.Any())
		    {
			    book.Authors.AddRange(bookModel.AuthorModels.Select(am => new Author(am.FirstName, am.LastName, am.Surname) {Shortcut = am.Shortcut}));
			}

		    this.bookLogic.Create(book);

		    return new EmptyResult();
	    }

	    public ActionResult Edit(Guid id)
	    {
            return View();
		}

	    public ActionResult Details(Guid id)
	    {
		    var book = this.bookLogic.Get(id);

			BookModel bookModel = BookModel.Map(book);

            return View(bookModel);
		}

	    public ActionResult Delete(Guid id)
	    {
            return View();
	    }
    }
}