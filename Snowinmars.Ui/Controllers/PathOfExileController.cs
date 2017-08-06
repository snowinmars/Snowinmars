﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Snowinmars.Bll;

namespace Snowinmars.Ui.Controllers
{
	[Route("pathOfExile")]
	[AllowAnonymous]
	[Internationalization]
	public class PathOfExileController : Controller
	{
		private PathOfExileLogic pathOfExileLogic;

		public PathOfExileController(PathOfExileLogic pathOfExileLogic)
		{
			this.pathOfExileLogic = pathOfExileLogic;
		}

		[HttpGet]
		[Route("qualities")]
        public ActionResult Qualities()
        {
            return View();
        }

		[HttpPost]
		[Route("qualities")]
		public JsonResult Qualities(IList<int> qualities, int desiredValue)
		{
			IEnumerable<IList<int>> qualityCombination = this.pathOfExileLogic.PickQualityCombination(qualities, desiredValue);

			return new JsonResult()
			{
				Data = qualityCombination,
				ContentEncoding = Encoding.UTF8,
				ContentType = "application/json",
			};
		}
    }
}