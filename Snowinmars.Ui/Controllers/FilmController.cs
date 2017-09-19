using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;

namespace Snowinmars.Ui.Controllers
{
	[Route("film")]
	[Internationalization]
	public class FilmController : Controller
	{
		private readonly IFilmLogic filmLogic;

		public FilmController(IFilmLogic filmLogic)
		{
			this.filmLogic = filmLogic;
		}

		[HttpGet]
		[Route("create")]
		public ActionResult Create()
		{
			return this.View(FilmModel.Empty);
		}

		[HttpPost]
		[Route("create")]
		public ActionResult Create(FilmModel filmModel)
		{
			Film film = ControllerHelper.Map(filmModel);

			this.filmLogic.Create(film);

			return new RedirectResult(this.Url.Action("Index", "Film"));
		}

		[HttpGet]
		[Route("delete")]
		public ActionResult Delete(Guid id)
		{
			this.filmLogic.Remove(id);

			return new RedirectResult(this.Url.Action("Index", "Film"));
		}

		[HttpGet]
		[Route("details")]
		public ActionResult Details(Guid id)
		{
			Film film;

			try
			{
				film = this.filmLogic.Get(id);
			}
			catch (ObjectNotFoundException e)
			{
				return this.View("BrokenDetails");
			}

			FilmModel filmModel = ControllerHelper.Map(film);

			return this.View(filmModel);
		}

		[HttpPost]
		[Route("details")]
		[ActionName("Details")]
		public JsonResult DetailsPost(Guid id)
		{
			var film = this.filmLogic.Get(id);
			var filmModel = ControllerHelper.Map(film);

			return ControllerHelper.GetSuccessJsonResult(filmModel);
		}

		[HttpGet]
		[Route("edit")]
		public ActionResult Edit(Guid id)
		{
			var film = this.filmLogic.Get(id);
			var filmModel = ControllerHelper.Map(film);

			return this.View(filmModel);
		}

		[HttpPost]
		[Route("edit")]
		public ActionResult Edit(FilmModel filmModel)
		{
			var film = ControllerHelper.Map(filmModel);

			this.filmLogic.Update(film);

			return new RedirectResult(this.Url.Action("Details", "Film", new RouteValueDictionary() { {"id", film.Id } }));
		}

		[HttpGet]
		[Route("index")]
        public ActionResult Index()
		{
			IEnumerable<Film> f;

			try
			{
				f = this.filmLogic.Get(null);
			}
			catch (ObjectNotFoundException e)
			{
				return this.View("BrokenDetails");
			}

			List<FilmModel> models = f.Select(ControllerHelper.Map).ToList();

			return this.View(models);
		}
	}
}