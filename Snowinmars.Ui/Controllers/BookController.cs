using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace Snowinmars.Ui.Controllers
{
    [Route("book")]
    [Authorize]
    [Internationalization]
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
            return this.View(BookModel.Empty);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult Create(BookModel bookModel)
        {
            Book book = ControllerHelper.Map(bookModel);

            this.bookLogic.Create(book);

            return new RedirectResult(this.Url.Action("Index", "Book"));
        }

        [HttpGet]
        [Route("delete")]
        public ActionResult Delete(Guid id)
        {
            this.bookLogic.Remove(id);

            return new RedirectResult(this.Url.Action("Index", "Book"));
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
                return this.View("BrokenDetails");
            }

            var bookModel = ControllerHelper.Map(book);

            return this.View(bookModel);
        }

        [HttpPost]
        [Route("details")]
        [ActionName("Details")]
        [AllowAnonymous]
        public JsonResult DetailsPost(Guid id)
        {
            var book = this.bookLogic.Get(id);
            var bookModel = ControllerHelper.Map(book);

            return this.Json(bookModel);
        }

        [HttpGet]
        [Route("edit")]
        public ActionResult Edit(Guid id)
        {
            var book = this.bookLogic.Get(id);

            var bookModel = ControllerHelper.Map(book);

            return this.View(bookModel);
        }

        [HttpPost]
        [Route("edit")]
        public ActionResult Edit(BookModel bookModel)
        {
            Book book = ControllerHelper.Map(bookModel);

            this.bookLogic.Update(book);

            return new RedirectResult(this.Url.Action("Details", "Book", new RouteValueDictionary() { { "id", book.Id } }));
        }

        [HttpPost]
        [Route("getAuthors")]
        [AllowAnonymous]
        public JsonResult GetAuthors(Guid id)
        {
            IEnumerable<Author> authorIds = this.bookLogic.GetAuthors(id);

            return this.Json(new
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
                return this.View("BrokenDetails");
            }

            List<BookModel> models = c.Select(ControllerHelper.Map).ToList();

            return this.View(models);
        }

        [HttpPost]
        [Route("startInformAboutWarnings")]
        public ActionResult StartInformAboutWarnings(Guid bookId)
        {
            this.bookLogic.StartInformAboutWarnings(bookId);

            return new RedirectResult(this.Url.Action("Index", "Book"));
        }

        [HttpPost]
        [Route("startInformAboutWarnings")]
        public ActionResult StartInformAboutWarnings()
        {
            this.bookLogic.StartInformAboutWarnings();

            return new RedirectResult(this.Url.Action("Index", "Book"));
        }

        [HttpPost]
        [Route("stopInformAboutWarnings")]
        public ActionResult StopInformAboutWarnings(Guid bookId)
        {
            this.bookLogic.StopInformAboutWarnings(bookId);

            return new RedirectResult(this.Url.Action("Index", "Book"));
        }

        [HttpPost]
        [Route("stopInformAboutWarnings")]
        public ActionResult StopInformAboutWarnings()
        {
            this.bookLogic.StopInformAboutWarnings();

            return new RedirectResult(this.Url.Action("Index", "Book"));
        }
    }
}