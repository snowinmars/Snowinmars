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
    [Internationalization]
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
            Author author = ControllerHelper.Map(authorModel);

            this.authorLogic.Create(author);

            return new RedirectResult(this.Url.Action("Index", "Author"));
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return this.View(AuthorModel.Emtpy);
        }

        [HttpGet]
        [Route("delete")]
        public ActionResult Delete(Guid id)
        {
            this.authorLogic.Remove(id);

            return new RedirectResult(this.Url.Action("Index", "Author"));
        }

        [HttpGet]
        [Route("details")]
        [AllowAnonymous]
        public ActionResult Details(Guid id)
        {
            var author = this.authorLogic.Get(id);

            AuthorModel authorModel = ControllerHelper.Map(author);

            return this.View(authorModel);
        }

        [HttpGet]
        [Route("edit")]
        public ActionResult Edit(Guid id)
        {
            Author author = this.authorLogic.Get(id);

            AuthorModel authorModel = ControllerHelper.Map(author);

            return this.View(authorModel);
        }

        [HttpPost]
        [Route("edit")]
        public ActionResult Edit(AuthorModel authorModel)
        {
            Author author = ControllerHelper.Map(authorModel);

            this.authorLogic.Update(author);

            return new RedirectResult(this.Url.Action("Details", "Author", new { id = author.Id }));
        }

        [HttpPost]
        [Route("getAll")]
        [AllowAnonymous]
        public JsonResult GetAll()
        {
            return ControllerHelper.GetSuccessJsonResult(this.authorLogic.Get(null));
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var c = this.authorLogic.Get(null).Select(ControllerHelper.Map);

            return this.View(c);
        }
    }
}