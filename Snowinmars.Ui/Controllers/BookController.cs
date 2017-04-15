﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
			IEnumerable<Book> c;
			try
			{
		        c = this.bookLogic.Get(null);
			}
			catch (ObjectNotFoundException e)
			{
				return View("BrokenDetails");
			}

	        List<BookModel> models = c.Select(book => new BookModel()
			{
				Id = book.Id, Title = book.Title, Year = book.Year, PageCount = book.PageCount, AuthorModelIds = book.AuthorIds.ToList(),
			}).ToList();

	        return View(models);
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
			Book book;

			try
		    {
			    book = this.bookLogic.Get(id);
		    }
			catch (ObjectNotFoundException e)
			{
				return View("BrokenDetails");
			}

			BookModel bookModel = BookModel.Map(book);

            return View(bookModel);
		}

	    public ActionResult Delete(Guid id)
	    {
			this.bookLogic.Remove(id);

			return new EmptyResult();
	    }

		[HttpPost]
	    public JsonResult GetAuthors(Guid id)
	    {
		    IEnumerable<Author> authorIds = this.bookLogic.GetAuthors(id);

		    return Json(new
		    {
			    BookId = id,
				Authors = authorIds,
			});
	    }
	}
}