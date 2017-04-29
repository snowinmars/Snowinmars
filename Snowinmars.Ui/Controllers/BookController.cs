using Snowinmars.Bll.Interfaces;
using Snowinmars.Common;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Snowinmars.Ui.Controllers
{
	[Route("book")]
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
			return View();
		}

		[HttpPost]
		[Route("create")]
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

			return new RedirectResult(Url.Action("Index", "Book"));
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
			Book book = new Book(bookModel.Title, bookModel.PageCount)
			{
				Id = bookModel.Id,
				Year = bookModel.Year,
			};

			book.AuthorIds.AddRange(bookModel.AuthorModelIds);

			this.bookLogic.Update(book);

			return new RedirectResult(Url.Action("Details", "Book", new RouteValueDictionary() { { "id", book.Id } }));
		}

		[HttpPost]
		[Route("getAuthors")]
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