using System.Web.Mvc;

namespace Snowinmars.Ui.Controllers
{
	[Route("Home")]
	public class HomeController : Controller
	{
		[HttpGet]
		[Route("")]
		public ActionResult Index()
		{
			return View();
		}
	}
}