using System.Web.Mvc;

namespace Snowinmars.Ui.Controllers
{
	[Route("Home")]
    [Authorize]
    public class HomeController : Controller
	{
		[HttpGet]
		[Route("")]
        [AllowAnonymous]
		public ActionResult Index()
        {
			return View();
		}

	    [HttpGet]
	    [Route("banned")]
	    [AllowAnonymous]
	    public ActionResult Banned()
	    {
	        return View();
	    }
	}
}