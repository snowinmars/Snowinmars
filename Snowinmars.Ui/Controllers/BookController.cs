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
	        var c = this.bookLogic.Get(null);
			
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

		    if (bookModel.AuthorModelIds != null &&
		        bookModel.AuthorModelIds.Any())
		    {
			    book.AuthorIds.AddRange(bookModel.AuthorModelIds);
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

		[HttpPost]
	    public JsonResult GetAuthorIds(Guid id)
	    {
		    IEnumerable<Guid> authorIds = this.bookLogic.GetAuthorIds(id);

		    return Json(authorIds);
	    }
	}
}