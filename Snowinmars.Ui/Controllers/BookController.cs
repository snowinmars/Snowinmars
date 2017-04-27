using Snowinmars.Bll.Interfaces;
using Snowinmars.Common;
using Snowinmars.Entities;
using Snowinmars.Ui.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;

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

		[HttpGet]
		[Route("create")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Route("create")]
		public ActionResult Create(ViewModels.Book.CreateViewModel bookModel)
		{
			Book book = new Book(bookModel.Title.Trim(), bookModel.PageCount)
			{
				Year = bookModel.Year,
			};

			if (bookModel.AuthorModels != null &&
				bookModel.AuthorModels.Any())
			{
				book.Authors.AddRange(bookModel.AuthorModels);
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

			ViewModels.Book.DetailsViewModel bookModel = ViewModels.Book.DetailsViewModel.Map(book);

			return View(bookModel);
		}

		[HttpGet]
		[Route("edit")]
		public ActionResult Edit(Guid id)
		{
			var book = this.bookLogic.Get(id);

			var bookModel = ViewModels.Book.UpdateViewModel.Map(book);

			return View(bookModel);
		}

		[HttpPost]
		[Route("edit")]
		public ActionResult Edit(ViewModels.Book.UpdateViewModel bookModel)
		{
			Book book = new Book(bookModel.Title.Trim(), bookModel.PageCount)
			{
				Id = bookModel.Id,
				Year = bookModel.Year,
			};

			book.Authors.AddRange(bookModel.AuthorModels);

			this.bookLogic.Update(book);

			return new RedirectResult(Url.Action("Details", "Book", new RouteValueDictionary() { { "id", book.Id } }));
		}

		[HttpPost]
		[Route("getAuthors")]
		public JsonResult GetAuthors(Guid id)
		{
			IEnumerable<Entities.Author> authorIds = this.bookLogic.GetAuthors(id);

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

			List<ViewModels.Book.DetailsViewModel> models = c.Select(book => new ViewModels.Book.DetailsViewModel()
			{
				Id = book.Id,
				Title = book.Title,
				Year = book.Year,
				PageCount = book.PageCount,
				AuthorModels = book.Authors.ToList(),
			}).ToList();

			return View(models);
		}
	}
}