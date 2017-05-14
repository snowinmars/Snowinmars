﻿using Snowinmars.Bll.Interfaces;
using Snowinmars.Common;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;

namespace Snowinmars.Ui.Controllers
{
	[Route("book")]
    [Authorize]
    public class BookController : Controller
	{
		private readonly IBookLogic bookLogic;

		public BookController(IBookLogic bookLogic)
		{
			this.bookLogic = bookLogic;
		}

		[HttpPost]
		[Route("startInformAboutWarnings")]
		public ActionResult StartInformAboutWarnings(Guid bookId)
		{
			this.bookLogic.StartInformAboutWarnings(bookId);

			return new RedirectResult(Url.Action("Index", "Book"));
		}

		[HttpPost]
		[Route("stopInformAboutWarnings")]
		public ActionResult StopInformAboutWarnings(Guid bookId)
		{
			this.bookLogic.StopInformAboutWarnings(bookId);

			return new RedirectResult(Url.Action("Index", "Book"));
		}

		[HttpPost]
		[Route("startInformAboutWarnings")]
		public ActionResult StartInformAboutWarnings()
		{
			this.bookLogic.StartInformAboutWarnings();

			return new RedirectResult(Url.Action("Index", "Book"));
		}

		[HttpPost]
		[Route("stopInformAboutWarnings")]
		public ActionResult StopInformAboutWarnings()
		{
			this.bookLogic.StopInformAboutWarnings();

			return new RedirectResult(Url.Action("Index", "Book"));
		}

		[HttpGet]
		[Route("create")]
		public ActionResult Create()
		{
			return View(BookModel.Empty);
		}

		[HttpPost]
		[Route("create")]
		public ActionResult Create(BookModel bookModel)
		{
			Book book = Map(bookModel);

			this.bookLogic.Create(book);

			return new RedirectResult(Url.Action("Index", "Book"));
		}

        private static Book Map(BookModel bookModel)
		{
			var book = new Book(bookModel.Title, bookModel.PageCount)
			{
				Year = bookModel.Year,
				AdditionalInfo = ControllerHelper.Convert(bookModel.AdditionalInfo),
				Bookshelf = ControllerHelper.Convert(bookModel.Bookshelf),
				FlibustaUrl = ControllerHelper.Convert(bookModel.FlibustaUrl),
				LibRusEcUrl = ControllerHelper.Convert(bookModel.LibRusEcUrl),
				LiveLibUrl =  ControllerHelper.Convert(bookModel.LiveLibUrl),
				MustInformAboutWarnings = bookModel.MustInformAboutWarnings,
                IsSynchronized = bookModel.IsSynchronized,
			};

		    string owner;

		    if (string.IsNullOrWhiteSpace(bookModel.Owner))
		    {
		        owner = System.Web.HttpContext.Current.User.Identity.Name;
            }
		    else
		    {
		        owner = ControllerHelper.Convert(bookModel.Owner);
		    }

		    book.Owner = owner;

			if (bookModel.Id != Guid.Empty)
			{
				book.Id = bookModel.Id;
			}

			if (bookModel.AuthorModelIds != null &&
				bookModel.AuthorModelIds.Any())
			{
				book.AuthorIds.AddRange(bookModel.AuthorModelIds);
			}

			if (book.AuthorShortcuts != null &&
				book.AuthorShortcuts.Any())
			{
				book.AuthorShortcuts.AddRange(bookModel.AuthorShortcuts);
			}

			return book;
		}

		[HttpGet]
		[Route("delete")]
		public ActionResult Delete(Guid id)
		{
			this.bookLogic.Remove(id);

			return new RedirectResult(Url.Action("Index", "Book"));
		}

		[HttpGet]
		[Route("details")]
        [AllowAnonymous]
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

			var bookModel = BookModel.Map(book);

			return View(bookModel);
		}

		[HttpGet]
		[Route("edit")]
		public ActionResult Edit(Guid id)
		{
			var book = this.bookLogic.Get(id);

			var bookModel = BookModel.Map(book);

			return View(bookModel);
		}

		[HttpPost]
		[Route("edit")]
		public ActionResult Edit(BookModel bookModel)
		{
			Book book = Map(bookModel);

			this.bookLogic.Update(book);

			return new RedirectResult(Url.Action("Details", "Book", new RouteValueDictionary() { { "id", book.Id } }));
		}

		[HttpPost]
		[Route("getAuthors")]
        [AllowAnonymous]
		public JsonResult GetAuthors(Guid id)
        {
			IEnumerable<Author> authorIds = this.bookLogic.GetAuthors(id);

			return Json(new
			{
				BookId = id,
				Authors = authorIds,
			});
		}

		[HttpGet]
		[Route("")]
        [AllowAnonymous]
        [OutputCache(Duration = 10, VaryByParam = "none", Location = OutputCacheLocation.Any)]
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

			List<BookModel> models = c.Select(BookModel.Map).ToList();

			return View(models);
		}
	}
}